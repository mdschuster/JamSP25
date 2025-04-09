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
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    //Singleton
    private static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static GameManager Instance()
    {
        return instance;
    }
    
    [Header("World")]
    public Player player;

    [Header("UI")] 
    public GameObject[] TutorialMessages;
    public GameObject[] SpeedPips;
    public GameObject returnToMenu;
    public GameObject winText;
    public GameObject winRestartText;
    private int currentPip;
    
    [Header("Camera")]
    public GameObject CinemachineCameraObject;
    
    [Header("Sound")]
    public GameObject TutorialSound;
    
    
    public bool playing;
    public bool alive;

    private void Start()
    {
        reset();
    }

    private void Update()
    {
        if (!alive || player.getWin())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Menu");
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("MainGame");
            }
        }
    }

    public void showTutorialMessage(int tutorialIndex)
    {
        if (tutorialIndex == 2) playing = true;
        GameObject go = TutorialMessages[tutorialIndex];
        StartCoroutine(FadeMessage(go));

    }

    IEnumerator FadeMessage(GameObject tutorialMessage)
    {
        tutorialMessage.SetActive(true);
        Instantiate(TutorialSound, tutorialMessage.transform.position, tutorialMessage.transform.rotation);
        StartCoroutine(FadeAlpha(tutorialMessage.GetComponent<TextMeshProUGUI>(),0f, 1f, 2f));
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeAlpha(tutorialMessage.GetComponent<TextMeshProUGUI>(),1f, 0f, 2f));
        yield return new WaitForSeconds(2f);
        tutorialMessage.SetActive(false);
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

    void reset()
    {
        playing = false;
        alive = true;
        player.reset();
        currentPip = 1;
        for (int i = 1; i < SpeedPips.Length; i++)
        {
            SpeedPips[i].SetActive(false);
        }
        returnToMenu.SetActive(false);
        winText.SetActive(false);
        winRestartText.SetActive(false);
    }

    public void UpdateMultiplier()
    {
        SpeedPips[currentPip].SetActive(true);
        currentPip++;
        player.UpdateMultiplier();
    }

    public void playerDeath()
    {
        alive = false;
        returnToMenu.SetActive(true);
        StartCoroutine(displayDeathText());
    }

    private IEnumerator displayDeathText()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeAlpha(returnToMenu.GetComponent<TextMeshProUGUI>(), 0f, 1f, 2f));
    }

    public void win()
    {
        player.win();
        CinemachineCameraObject.GetComponent<CameraScript>().win();
        StartCoroutine(DisplayWinText());
    }

    public IEnumerator DisplayWinText()
    {
        yield return new WaitForSeconds(1f);
        winText.SetActive(true);
        Instantiate(TutorialSound,this.transform.position, Quaternion.identity);
        winText.GetComponent<TMP_Text>().text = "Traveled "+(int)player.transform.position.z+" Units";
        StartCoroutine(FadeAlpha(winText.GetComponent<TextMeshProUGUI>(), 0f, 1f, 2f));
        yield return new WaitForSeconds(2f);
        winRestartText.SetActive(true);
        StartCoroutine(FadeAlpha(winRestartText.GetComponent<TextMeshProUGUI>(), 0f, 1f, 2f));
    }



}
