using UnityEngine;

public class LightPlateController : MonoBehaviour
{
    [SerializeField] float timerTime = 2;
    private float timer;
    private bool timerStart = false;

    private GameObject sourcePlate = null;
    private Light lightSpot;
    private bool unlock = true;

    private void Start()
    {
        timer = timerTime;
        lightSpot = transform.GetChild(0).GetComponent<Light>();
    }

    private void Update()
    {
        if (timerStart)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timerStart = false;
                timer = 0;
                ChangeColor(Color.red);

            }
        }
    }

    public void CollisionDetected(GameObject source)
    {
        if (unlock)
        {

            if (sourcePlate == null || sourcePlate == source)
            {
                ChangeColor(Color.yellow);

                sourcePlate = source;
                timer = timerTime;
                timerStart = true;
            }
            else if (sourcePlate != source)
            {



                timerStart = false;

                if (timer > 0)
                {
                    ChangeColor(Color.green);

                    lightSpot.enabled = true;
                }
                sourcePlate = null;

            }
        }


    }

    private void ChangeColor(Color color)
    {
        if (color == Color.green)
        {
            unlock = false;
        }
        foreach (Transform child in transform)
        {
            if (child.name.Contains("pressure"))
            {
                child.GetComponentInChildren<Renderer>().material.SetColor("_Color", color);
            }
            
        }
    }
}