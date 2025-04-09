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
using UnityEngine;

public class Pillar : MonoBehaviour
{

    public Player player;
    public float midDistance;
    public float maxDistance;
    private MeshRenderer mr;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player=GameManager.Instance().player;
        mr = GetComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Shader Graphs/PillarOfLight"));
        mr.material.SetColor("_Color", new Color(191f/255,19f/255,182f/255)*60f);
        mr.material.SetFloat("_Alpha", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        player=GameManager.Instance().player;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance > maxDistance)
        {
            mr.material.SetFloat("_Alpha", 0f);
            return;
        }

        float newAlpha = 0f;
        if (distance < maxDistance)
        {
            newAlpha = Mathf.Lerp(0f, 0.2f, 1-(distance / maxDistance));
        }
        
        if (distance < midDistance)
        {
            newAlpha = Mathf.Lerp(0.2f, 0f, 1-((distance-150) / midDistance));
        }
        mr.material.SetFloat("_Alpha",newAlpha);
    }
}
