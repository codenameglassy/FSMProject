using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;


    [Header("Canvas - Combo")]
    public GameObject comboPrefab;
    public RectTransform panel;         // Assign your Canvas in the Inspector
    public RectTransform comboSpawnReferencePoint; // Reference a RectTransform in the Canvas

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    int currentScore;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString("0");
    }

    public void SubmitScore()
    {
        Leaderboard leaderboard = FindObjectOfType<Leaderboard>();
        leaderboard.SubmitScore(currentScore);
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        scoreText.text = currentScore.ToString("0");
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void SpawnComboPrefab(string string_)
    {
        if (comboPrefab == null || comboSpawnReferencePoint == null || panel == null)
        {
            Debug.LogError("Please assign all references in the Inspector.");
            return;
        }

        // Instantiate the text prefab
        GameObject spawnedText = Instantiate(comboPrefab, panel);

        // Get the RectTransform of the spawned text
        RectTransform rectTransform = spawnedText.GetComponent<RectTransform>();

        // Set its position to match the reference RectTransform
        rectTransform.anchoredPosition = comboSpawnReferencePoint.anchoredPosition;

        // Set Text
        spawnedText.GetComponent<TextMeshProUGUI>().text = string_;
    }
}
