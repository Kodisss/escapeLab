using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// This script defines a custom rendering feature for adding a pixelized effect to the scene.
public class PixelizeFeature : ScriptableRendererFeature
{
    // This nested class contains settings for the custom pass, including the target height of the pixelized effect.
    [System.Serializable]
    public class CustomPassSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing; // The point in the rendering pipeline at which the custom pass will execute.
        public int screenHeight = 144; // The target height of the pixelized effect.
    }

    [SerializeField] private CustomPassSettings settings; // The settings for the custom pass.
    private PixelizePass customPass; // The custom rendering pass for applying the pixelized effect.

    // This method creates a new instance of PixelizePass and assigns it to customPass.
    public override void Create()
    {
        customPass = new PixelizePass(settings);
    }

    // This method adds the custom pass to the renderer's rendering queue.
    // It skips the custom pass in the Unity editor's scene view to improve performance.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
#if UNITY_EDITOR
        if (renderingData.cameraData.isSceneViewCamera) return;
#endif
        renderer.EnqueuePass(customPass);
    }
}
