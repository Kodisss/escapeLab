using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    // helps with the isometric camera settings so that every movement are offseted by 45° on the Y-axis
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0)); // calculates the offset
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input); // apply the offset

    private static Matrix4x4 _lookMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 30, 0)); // calculates the offset
    public static Vector3 ToLook(this Vector3 input) => _lookMatrix.MultiplyPoint3x4(input); // apply the offset

    private static Matrix4x4 _invLookMatrix = _lookMatrix.inverse;
    public static Vector3 ToInvLook(this Vector3 input) => _invLookMatrix.MultiplyPoint3x4(input); // apply the offset
}