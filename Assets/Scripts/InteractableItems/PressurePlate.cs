using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PressurePlate : MonoBehaviour
{
    private float speed=5;
    private float heightDifferenceWhenDown = 2;
    public bool goUp = false;
    public bool goDown = false;
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

    public abstract void CollisionDetected();

    public void CollisionExited()
    {

        goUp = true;


    }
}
