using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Valve.VR.InteractionSystem;

public class HeadCountController : MonoBehaviour
{
    public Image scoreRing;
    public TextMeshProUGUI scoreText;
    public Canvas canvas;
    public int maximumScore = 10;
    public int score = 0;
    private string currentSceneName;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
        CheckScene(currentSceneName);

        scoreText.text = $"0 / {maximumScore}";
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != currentSceneName)
        {
            currentSceneName = currentScene.name;
            CheckScene(currentSceneName);
        }
    }

    void UpdateScoreUI()
    {
        scoreRing.fillAmount = score / maximumScore;
        scoreText.text = ($"{score} / {maximumScore}");
        if (score >= maximumScore)
            scoreText.color = new Color32(101, 36, 129, 255);
    }

    void CheckScene(string sceneName)
    {
        if (sceneName == "KidRoom" )
        {
            canvas.enabled = true;
            score = 0;
            UpdateScoreUI();

        }
        else
        {
            canvas.enabled = false;
        }
    }

    public void AddPoints(int points)
    {
        score += 1;
        UpdateScoreUI();

        if (score >= maximumScore)
        {
            GameObject exitDoor = GameObject.Find("ExitDoor");
            Interactable interactable = exitDoor.GetComponent<Interactable>();
            interactable.enabled = true;

            GameObject.Find("ExitSign").GetComponent<Image>().enabled = true;
            GameObject.Find("ExitArrow").GetComponent<Image>().enabled = true;
        }
    }

    public void Finish()
    {
        SceneController.SwitchScene("Hall");
    }
}
