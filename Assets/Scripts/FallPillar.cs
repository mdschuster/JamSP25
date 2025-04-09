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
