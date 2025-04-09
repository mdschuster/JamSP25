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
using UnityEditor;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 movement;
    private bool death = false;
    private bool didWin = false;

    [Header("Movement Properties")]
    public float forwardSpeed;
    public float sideSpeed;
    public float rotationSpeed;
    public float maxRotationAngle = 45f; // Max rotation angle in degrees
    private float currentRotation = 0f;
    public float speedMultiplier;
    public float multiplierAmount;
    public bool disableControls;
    
    [Header("Effects")]
    public GameObject deathEffect;
    public GameObject deathSound;
    public Color engineColor;
    public float engineIntensity;

    [Header("Camera Information")] 
    public CameraScript playerCamera;

    [Header("Graphic")]
    public GameObject graphic;
    public GameObject winPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (death || disableControls) return;
        float sideInput = Input.GetAxisRaw("Horizontal");
        movement.x=sideInput*sideSpeed*speedMultiplier;
        movement.y = 0.0f;
        movement.z = forwardSpeed*speedMultiplier;
        
        if (sideInput < 0) RotateTowardsMax(1);
        else if (sideInput > 0) RotateTowardsMax(-1);
        else  RotateBackToZero();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement;
    }
    
    void RotateTowardsMax(int direction)
    {
        float step = rotationSpeed * Time.deltaTime * direction;
        float newRotation = Mathf.Clamp(currentRotation + step, -maxRotationAngle, maxRotationAngle);
        transform.rotation = Quaternion.Euler(0, 0, newRotation);
        currentRotation = newRotation;
    }

    void RotateBackToZero()
    {
        float step = rotationSpeed * Time.deltaTime * Mathf.Sign(currentRotation);
        if (Mathf.Abs(currentRotation) < step)
        {
            currentRotation = 0;
        }
        else
        {
            currentRotation -= step;
        }
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            Instantiate(deathSound, transform.position, Quaternion.identity);
            GameObject go = Instantiate(deathEffect, transform.position, Quaternion.identity);
            ParticleSystem.MainModule main = go.GetComponent<ParticleSystem>().main;
            main.startSpeed = new ParticleSystem.MinMaxCurve(forwardSpeed - 0.1f * forwardSpeed, forwardSpeed + 0.1f * forwardSpeed);
            playerCamera.activateDeath();
            forwardSpeed = 0f;
            death = true;
            movement = Vector3.zero;
            this.graphic.SetActive(false);
            this.GetComponent<Collider>().enabled = false;
            GameManager.Instance().playerDeath();
        }
    }

    public void UpdateMultiplier()
    {
        speedMultiplier += multiplierAmount;
        engineIntensity++;
        graphic.GetComponent<MeshRenderer>().materials[2].SetColor("_Color", engineColor*Mathf.Pow(2,engineIntensity));    }

    public void reset()
    {
        death = false;
        forwardSpeed = 50f;
        this.graphic.SetActive(true);
        this.GetComponent<Collider>().enabled = true;
        speedMultiplier = 1f;
        engineIntensity = 1f;
        disableControls = false;
        didWin = false;
        graphic.GetComponent<MeshRenderer>().materials[2].SetColor("_Color", engineColor*Mathf.Pow(2,engineIntensity));
        
    }

    public void win()
    {
        disableControls = true;
        didWin = true;
        winPosition.transform.SetParent(null);
        foreach(Collider c in GetComponents<Collider> ())
        {
            c.enabled = false;
        }
    }

    public bool getWin()
    {
        return didWin;
    }
}
