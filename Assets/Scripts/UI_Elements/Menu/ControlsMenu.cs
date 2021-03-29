using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    private ControllerManager _controllerManager;

    [SerializeField] private GameObject xboxControls;
    [SerializeField] private GameObject ps4Controls;
    // Start is called before the first frame update
    void Awake()
    {
        _controllerManager = GetComponent<ControllerManager>();
    }
    
    private void OnEnable()
    {
        switch (_controllerManager.GetControllerType())
        {
            case ControllerType.XboxController:
            default:
            {
                xboxControls.SetActive(true);
                break;
            }
            case ControllerType.PS4Controller:
            {
                ps4Controls.SetActive(true);
                break;
            }
        }
    }

    private void OnDisable()
    {
        xboxControls.SetActive(false);
        ps4Controls.SetActive(false);
    }
}
