using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private float speed=5;
    private float heightDifferenceWhenDown = 2;
    private bool goUp = false;
    private bool goDown = false;
    // Update is called once per frame
    void Update()
    {
        if (goUp)
        {
            transform.localPosition += new Vector3(0, Time.deltaTime * speed, 0);
            if (transform.localPosition.y >= 0)
            {

                goUp = false;
            }
        }
        else if (goDown)
        {
            transform.localPosition -= new Vector3(0, Time.deltaTime * speed, 0);
            if (transform.localPosition.y <= -heightDifferenceWhenDown)
            {
                goDown = false;
            }
        }
    }

    public void CollisionDetected()
    {
        goDown = true;
        CollisionEntered();
    }

    public void CollisionExited()
    {

        goUp = true;


    }

    public virtual void CollisionEntered()
    {

    }
}
