using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightController : MonoBehaviour
{
    [SerializeField] private int movementSpeed;
    private CharacterController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move(transform.forward * (-Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed));
        controller.Move(transform.right * (Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed));
    }
}
