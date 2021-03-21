using UnityEngine;
using UnityEngine.Serialization;

public class LightPlateController : MonoBehaviour
{
    [SerializeField] private GameObject gameLight;
    [SerializeField] private float timerTime = 2;
    private Light lightSpot;
    private float timer;
    private bool timerStart = false;

    private GameObject sourcePlate = null;
    private bool unlock = true;

    private bool testBool =true;
    private void Start()
    {
        timer = timerTime;
        lightSpot =gameLight.GetComponent<Light>();
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

        //cheat code
        if (Input.GetButtonDown("Fire2") && testBool)
        {
            CollisionDetected(transform.GetChild(1).gameObject);
            testBool = false;


        }
    }

    public void CollisionDetected(GameObject source)
    {
        if (unlock)
        {
            testBool = true;

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