using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	public GameObject collectEffect;
	public GameObject collectSound;
	public float rotationSpeed;
	private float currentRotation = 0f;
	

    // Update is called once per frame
    void Update()
    {
	    float step = rotationSpeed * Time.deltaTime;
	    float newRotation = currentRotation + step;
	    transform.rotation = Quaternion.Euler(0, newRotation, 0);
	    currentRotation = newRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
	    Instantiate(collectSound, transform.position, transform.rotation);
	    GameObject go = Instantiate(collectEffect, this.transform.position, Quaternion.identity);
	    ParticleSystem ps = go.GetComponent<ParticleSystem>();
	    float speed = other.gameObject.GetComponent<Player>().forwardSpeed;
	    ParticleSystem.MainModule main = ps.main;
	    main.startSpeed = new ParticleSystem.MinMaxCurve(speed-0.1f*speed, speed+0.1f*speed);
	    GameManager.Instance().UpdateMultiplier();
	    Destroy(this.gameObject);
    }
}
