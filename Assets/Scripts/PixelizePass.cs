using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// This script defines a custom rendering pass for applying a pixelized effect to the scene.
public class PixelizePass : ScriptableRenderPass
{
    private PixelizeFeature.CustomPassSettings settings; // The settings for the pixelize pass.

    private RenderTargetIdentifier colorBuffer, pixelBuffer; // The render target identifiers for the color and pixel buffers.
    private int pixelBufferID = Shader.PropertyToID("_PixelBuffer"); // The ID for the pixel buffer, used by the shader.

    private Material material; // The material for the pixelize effect.
    private int pixelScreenHeight, pixelScreenWidth; // The pixel dimensions of the screen after the pixelize effect is applied.

    // This constructor initializes the pixelize pass and sets up the material for the pixelize effect.
    public PixelizePass(PixelizeFeature.CustomPassSettings settings)
    {
        this.settings = settings;
        this.renderPassEvent = settings.renderPassEvent;
        if (material == null) material = CoreUtils.CreateEngineMaterial("Hidden/Pixelize"); // Create the material for the pixelize effect.
    }

    // This method sets up the pixel buffer and updates the material with the block count and block size for the pixelize effect.
    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        colorBuffer = renderingData.cameraData.renderer.cameraColorTarget; // Get the color buffer for the camera.
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor; // Get the descriptor for the camera.

        pixelScreenHeight = settings.screenHeight; // Get the target height for the pixelize effect.
        pixelScreenWidth = (int)(pixelScreenHeight * renderingData.cameraData.camera.aspect + 0.5f); // Calculate the target width for the pixelize effect.

        // Update the material with the block count and block size for the pixelize effect.
        material.SetVector("_BlockCount", new Vector2(pixelScreenWidth, pixelScreenHeight));
        material.SetVector("_BlockSize", new Vector2(1.0f / pixelScreenWidth, 1.0f / pixelScreenHeight));
        material.SetVector("_HalfBlockSize", new Vector2(0.5f / pixelScreenWidth, 0.5f / pixelScreenHeight));

        descriptor.height = pixelScreenHeight; // Update the descriptor with the target height for the pixelize effect.
        descriptor.width = pixelScreenWidth; // Update the descriptor with the target width for the pixelize effect.

        cmd.GetTemporaryRT(pixelBufferID, descriptor, FilterMode.Point); // Get a temporary render texture for the pixel buffer.
        pixelBuffer = new RenderTargetIdentifier(pixelBufferID); // Set the pixel buffer as the render target identifier.
    }

    // This method applies the pixelize effect to the scene by blitting the color buffer to the pixel buffer with the pixelize material, 
    // and then blitting the pixel buffer back to the color buffer.
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get(); // Get a command buffer.
        using (new ProfilingScope(cmd, new ProfilingSampler("Pixelize Pass")))
        {
            Blit(cmd, colorBuffer, pixelBuffer, material); // Blit the color buffer to the pixel buffer with the pixelize material.
            Blit(cmd, pixelBuffer, colorBuffer); // Blit the pixel buffer back to the color buffer.
        }

        context.ExecuteCommandBuffer(cmd); // Execute the command buffer.
        CommandBufferPool.Release(cmd); // Release the command buffer.
    }

    // This method releases the temporary render texture used by the pixelize effect.
    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        if (cmd == null) throw new System.ArgumentNullException("cmd");

        // Release the temporary render target
        cmd.ReleaseTemporaryRT(pixelBufferID);
    }
}