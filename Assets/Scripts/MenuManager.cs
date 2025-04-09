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

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject GameName;
    public GameObject Instructions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameName.SetActive(false);
        Instructions.SetActive(false);
        StartCoroutine(FadeMenu());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainGame");
        }
    }
    
    IEnumerator FadeMenu()
    {
        yield return new WaitForSeconds(1.5f);
        GameName.SetActive(true);
        StartCoroutine(FadeAlpha(GameName.GetComponent<TextMeshProUGUI>(),0f, 1f, 2f));
        yield return new WaitForSeconds(2f);
        Instructions.SetActive(true);
        StartCoroutine(FadeAlpha(Instructions.GetComponent<TextMeshProUGUI>(),0f, 1f, 2f));

    }

    private IEnumerator FadeAlpha(TextMeshProUGUI tmpText, float from, float to, float duration)
    {
        float elapsed = 0f;
        Color color = tmpText.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            tmpText.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Ensure final alpha is set exactly
        tmpText.color = new Color(color.r, color.g, color.b, to);
    }
}
