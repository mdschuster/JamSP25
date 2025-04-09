/*
Copyright (c) 2025, Micah Schuster
All rights reserved.
Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
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
