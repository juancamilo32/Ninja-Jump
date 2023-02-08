using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("UI Manager is null.");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI bestScoreText;
    [SerializeField]
    GameObject deathScreen;

    private void Start()
    {
        scoreText.text = "Score: 0";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateBestScore(int score)
    {
        bestScoreText.text = "Best: " + score.ToString();
    }

    public void EnableDeathScreen(int score)
    {
        deathScreen.gameObject.SetActive(true);
        deathScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString(); ;
    }

}
