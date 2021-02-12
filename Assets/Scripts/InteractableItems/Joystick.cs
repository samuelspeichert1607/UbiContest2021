using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractableItemBase;
using static CustomController;

public class Joystick : InteractableItemBase
{
    

    [SerializeField] private CustomController target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target.Move(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), Time.deltaTime);
    }

    public override void onInteract()
    {
        base.onInteract();
    }
}
