using UnityEngine;

public class Moon : MonoBehaviour
{
    private float currentAngle;
    private float currentTemperature;

    public float angleSpeed;
    private float temperatureSpeed;

    private bool win = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAngle=transform.eulerAngles.x;
        currentTemperature = GetComponent<Light>().colorTemperature;
        temperatureSpeed=1000f*angleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (win || !GameManager.Instance().alive) return;
        if (!GameManager.Instance().playing) return;
        float angleStep= angleSpeed*Time.deltaTime;
        currentAngle -= angleStep;
        transform.eulerAngles = new Vector3(currentAngle,transform.eulerAngles.y,transform.eulerAngles.z);
        float tempStep = temperatureSpeed*Time.deltaTime;
        currentTemperature -= tempStep;
        if (currentTemperature < 1500) currentTemperature = 1500;
        GetComponent<Light>().colorTemperature = currentTemperature;
        if (currentAngle <= -2.5f)
        {
            GameManager.Instance().win();
            win = true;
        }
    }
}
