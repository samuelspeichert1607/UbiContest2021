using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public bool CanMove = false;
    [SerializeField] private float speed;
    [SerializeField] private float finalYPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            transform.position += new Vector3(0, Time.deltaTime * speed,0);
            if (transform.position.y>= finalYPos)
            {
                CanMove = false;
            }
        }
    }
}
