using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 movingDirection;
    [SerializeField] private float speed;
    [SerializeField] private float movingDistance;
    private Transform player;
    private Vector3 startingPos;
    private Vector3 offset;

    private bool playerIn = false;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if(Vector3.Distance(startingPos,transform.position)>=movingDistance)
        {
            speed = -speed;
        }

        transform.position += speed * movingDirection*Time.deltaTime;
        if (playerIn)
        {
            
            //player.position = transform.position+offset;
            player.GetComponent<CharacterController>().Move(speed *Time.deltaTime*movingDirection);

        }
    }
    

    public void PlayerEntered(Transform playerE)
    {
        player = playerE;
        offset = player.position - transform.position;
        playerIn = true;
    }
    
    public void PlayerExited()
    {
        playerIn = false;
    }
}
