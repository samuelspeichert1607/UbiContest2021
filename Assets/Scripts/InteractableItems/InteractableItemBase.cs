using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemBase : MonoBehaviour
{
    public string Name;

    public string InteractText;

    public virtual void onInteract()
    {
        
    }
}
