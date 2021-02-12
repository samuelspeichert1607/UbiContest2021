using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarelleTile : MonoBehaviour
{
    //private GameObject player1;
    //private GameObject player2;

    [SerializeField] private float timerTime;
    [SerializeField] private bool firstTile = false;


    private float timer =0;

    private GameObject playerEntered=null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
        }


    }


    public void CollisionDetected(GameObject collision)
    {
        bool check = transform.parent.GetComponent<EndTile>().check;
        if ((!firstTile&& check) || (firstTile && !check))
        {
            Debug.Log(playerEntered+" "+ collision);
            if (playerEntered == null)
            {
                playerEntered = collision;
                timer = timerTime;
            }
            else if (playerEntered != collision)
            {
                playerEntered = null;
                if (timer > 0)
                {
                    Debug.Log("oui");
                    transform.parent.GetComponent<EndTile>().check = true;
                }
                else
                {
                    Debug.Log("non");
                    transform.parent.GetComponent<EndTile>().check = false;
                    
                }

            }
        }


    }


}
