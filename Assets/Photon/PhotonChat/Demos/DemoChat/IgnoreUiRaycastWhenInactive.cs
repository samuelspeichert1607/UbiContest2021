using UnityEngine;


public class IgnoreUiRaycastWhenInactive : MonoBehaviour, ICanvasRaycastFilter
{
    public bool IsRaycastLocationValid(Vector2 sp, UnityEngine.Camera eventCamera)
    {
        return gameObject.activeInHierarchy;
    }
}