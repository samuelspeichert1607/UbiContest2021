using UnityEngine;

public class LightPlateController : MonoBehaviour
{
    [SerializeField] float timerTime = 2;
    private float timer;
    private bool timerStart = false;

    private GameObject sourcePlate = null;
    private Light lightSpot;

    private LightPlate plate1Temp;
    private LightPlate plate2Temp;
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

                plate1Temp.ChangeColor(Color.red);
                if (plate2Temp != null)
                {
                    plate2Temp.ChangeColor(Color.red);
                }

            }
        }
    }

    public void CollisionDetected(GameObject source)
    {

        LightPlate sourceScript = source.GetComponent<LightPlate>();
        if (sourcePlate == null)
        {
            sourceScript.ChangeColor(Color.yellow);
            plate1Temp = sourceScript;
            sourcePlate = source;
            timer = timerTime;
            timerStart = true;
        }
        else if (sourcePlate != source)
        {

            plate2Temp = sourcePlate.GetComponent<LightPlate>();

            timerStart = false;

            if (timer > 0)
            {
                sourceScript.ChangeColor(Color.green);
                sourcePlate.GetComponent<LightPlate>().ChangeColor(Color.green);

                lightSpot.enabled = true;
            }
            sourcePlate = null;

        }
    }
}