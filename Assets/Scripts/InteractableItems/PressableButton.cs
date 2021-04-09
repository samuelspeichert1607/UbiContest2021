using System.Collections;
using System.Collections.Generic;
using System.Timers;
using InteractableItems;
using UnityEngine;

public class PressableButton : InteractableItem
{
    // Start is called before the first frame update
    [SerializeField] private float pressingTime;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pressingSound;
    [SerializeField] private Vector3 pressingDirection = new Vector3(-1,0, 0);
    
    // private ControllerManager controllerManager;
    private readonly float movingDistance = 0.15f;
    private Vector3 restPosition;

    private new void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        restPosition = transform.position;
        // controllerManager = GetComponent<ControllerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfAPlayerIsInRange();
        if (hasPlayerEnteredRange())
        {
            OnPlayerEnterRange();
        }
        else if (hasPlayerLeftRange())
        {
            OnPlayerExitRange();
        }
        else if (HasPlayerInRange)
        {
            OnPlayerInRange();
        }
    }
    
    public override void OnInteractStart()
    {
        IsInteractedWith = true;
        audioSource.PlayOneShot(pressingSound);
        StartCoroutine(PressingDown(pressingTime / 2f));
        Invoke(nameof(StartUnpressing), pressingTime / 2f);
        Invoke(nameof(OnInteractEnd), pressingTime);
        TextRenderer.CloseInfoText();
    }

    IEnumerator PressingDown(float pressingDownTime)
    {
        float startTime = Time.time;
        Vector3 initialPosition = transform.position;
        Vector3 displacement =  pressingDirection * movingDistance;
        while (Time.time - startTime < pressingDownTime)
        {
            float fractionOfTransition = (Time.time - startTime) / pressingDownTime;
            transform.position = initialPosition + displacement * fractionOfTransition;
            Debug.Log(transform.position.z);
            yield return null;
        }
    }

    private void StartUnpressing()
    {
        StartCoroutine(Unpressing(pressingTime/ 2f));
    }
    
    IEnumerator Unpressing(float unpressingTime)
    {
        float startTime = Time.time;
        Vector3 initialPosition = transform.position;
        Vector3 displacement =  - pressingDirection * movingDistance;
        while (Time.time - startTime < unpressingTime)
        {
            float fractionOfTransition = (Time.time - startTime) / unpressingTime;
            transform.position = initialPosition + displacement * fractionOfTransition;
            Debug.Log(transform.position.z);
            yield return null;
        }

        transform.position = restPosition;
    }
    
    public override void OnInteractEnd()
    {
        IsInteractedWith = false;
        TextRenderer.ShowInfoText(ToStartInteractText);
    }

    public override void OnPlayerEnterRange()
    {
        FindTextRendererOfPlayerInRange();
        TextRenderer.ShowInfoText(ToStartInteractText);   
    }

    public override void OnPlayerExitRange()
    {
        TextRenderer.CloseInfoText();
    }
    
    public bool IsPressed()
    {
        return IsInteractedWith;
    }

    public override void OnPlayerInRange()
    {
        
        if (ControllerManager.GetButtonDown(interactButtonName) && !IsInteractedWith)
        {
            OnInteractStart();
        }
    }
}
