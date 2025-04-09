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
