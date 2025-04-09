using Unity.Mathematics.Geometry;
using UnityEngine;

public class FallPillar : MonoBehaviour
{
    public bool falling;
    public float fallingSpeed;
    public float rotationAmount;
    private float totalRotation;
    public GameObject fallPillar;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        falling = false;
        totalRotation = 0;
        int number = Random.Range(0, 2);
        if(number == 0)
            fallPillar.transform.rotation = Quaternion.Euler(-90, 90, 0);
        else
            fallPillar.transform.rotation = Quaternion.Euler(-90, 270, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!falling) return;
        fallPillar.transform.Rotate(-fallingSpeed * Time.deltaTime*Vector3.right );
        totalRotation += fallingSpeed * Time.deltaTime;
        if(totalRotation>=rotationAmount) falling = false;
    }

    public void Fall()
    {
        falling = true;
    }
}
