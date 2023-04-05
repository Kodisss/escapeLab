using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    // Gravity Scale editable on the inspector
    // providing a gravity scale per object

    [SerializeField] private float gravityScale = 100.0f;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    private static float globalGravity = -9.81f;

    Rigidbody m_rb;

    void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale / 100 * Vector3.up;
        m_rb.AddForce(gravity, ForceMode.Acceleration);
    }
}