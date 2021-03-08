using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    private static float _rotationSpeed = 110;

    public static float RotationSpeed
    {
        get => _rotationSpeed;
        set => _rotationSpeed = value;
    }
}
