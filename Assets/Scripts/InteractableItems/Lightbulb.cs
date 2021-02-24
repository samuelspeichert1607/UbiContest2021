using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LightbulbState 
{
    Closed,
    Failure,
    Success,
}

public class Lightbulb : MonoBehaviour
{
    [SerializeField]
    private Material closedMaterial;
    [SerializeField]
    private Material failureMaterial;
    [SerializeField]
    private Material successMaterial;

    private LightbulbState state = LightbulbState.Closed;
    private MeshRenderer _renderer;

    public void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void Close()
    {
        state = LightbulbState.Closed;
        _renderer.material = closedMaterial;
    }

    public void Fail()
    {
        state = LightbulbState.Failure;
        _renderer.material = failureMaterial;
    }
    
    public void Success()
    {
        state = LightbulbState.Success;
        _renderer.material = successMaterial;
    }

    public bool IsInSuccessState()
    {
        return state == LightbulbState.Success;
    }
}
