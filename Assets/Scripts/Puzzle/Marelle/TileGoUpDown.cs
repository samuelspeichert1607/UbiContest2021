using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGoUpDown : MonoBehaviour
{
    //'a modifier quand j,aurai un prefab;
    public bool CanGoUp = false;
    public bool CanGoDown = false;

    private float initialY;
    private int speed = 5;
    private float minY = -1;

    private void Start()
    {
        initialY = transform.position.y;
    }

    void Update()
    {
        if (CanGoUp)
        {
            transform.position += new Vector3(0, Time.deltaTime * speed, 0);
            if (transform.position.y >= initialY)
            {
                CanGoUp = false;
            }
        }
        else if (CanGoDown)
        {

            transform.position -= new Vector3(0, Time.deltaTime * speed, 0);
            if (transform.position.y <= minY) //initialY-minY
            {
                CanGoDown = false;
            }
        }
    }
}
