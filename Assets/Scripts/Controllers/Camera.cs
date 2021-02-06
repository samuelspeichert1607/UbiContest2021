using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public int rotationSpeed;
    private float angleY = 0;
    private float angleX = 0;
    public float r;
    private float h;
    private float k;

    private float rotationY;
    private float rotationX;

    // Start is called before the first frame update
    void Start()
    {
        h = transform.position.x;
        k = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {


        rotationY = Input.GetAxis("RotateY");
        rotationX = Input.GetAxis("RotateX");



        angleY += rotationY;
        angleX += rotationX;

        if (Mathf.Abs(rotationY) > Mathf.Abs(rotationX))
        {
            transform.position = new Vector3(h, Mathf.Cos((Mathf.Deg2Rad * angleY)) * r + h, Mathf.Sin((Mathf.Deg2Rad * angleY)) * r + k);
        }
        else if (rotationX != 0)
        {
            transform.position = new Vector3(Mathf.Sin((Mathf.Deg2Rad * angleX)) * r + k, transform.position.y, Mathf.Cos((Mathf.Deg2Rad * angleX)) * r + h);
        }

    }
}
