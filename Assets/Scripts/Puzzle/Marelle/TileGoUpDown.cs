using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGoUpDown : MonoBehaviour
{
    public bool CanGoUp = false;
    public bool CanGoDown = false;

    private float initialY;
    private int speed = 25;
    private float minY = -12;

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
            if (transform.position.y <= minY)
            {
                CanGoDown = false;
            }
        }
    }
}
