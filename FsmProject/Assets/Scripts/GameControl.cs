using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameControl : MonoBehaviour
{

    public static GameControl instance;


    [Header("Runaway-points")]
    public List<Transform> runawayPoints = new List<Transform>();

    [Header("Enemy")]
    public List<Transform> enemyEntityList = new List<Transform>();

    //[Header("Game State")]
    public bool isGameOver { get; private set; }
    public bool isGameStart { get; private set; }

    [Header("Canvas")]
    public GameObject gameOverCanvas;
    public GameObject instructionText;
    public CanvasGroup fadeCanvas;
    [SerializeField] private float fadeDelay;
    [SerializeField] private float gameStartDelay;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeCanvas.alpha = 1.0f;
        isGameOver = false;
        isGameStart = false;
        AudioManagerCS.instance.Stop("titleMusic");
        AudioManagerCS.instance.Play("music");
        StartCoroutine(Enum_Fade());
    }

    IEnumerator Enum_Fade()
    {
        yield return new WaitForSeconds(fadeDelay);
        fadeCanvas.DOFade(0, 2f);

        yield return new WaitForSeconds(gameStartDelay);
        isGameStart = true;
        instructionText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Enemy
    public void AddEnemy(Transform enemy)
    {
        enemyEntityList.Add(enemy);
    }
    public void RemoveEnemy(Transform enemy)
    {
        enemyEntityList.Remove(enemy);
    }

    #endregion

    #region HitStop
    private bool isHitStopping = false;
    public void TriggerHitstop(float duration)
    {
        StartCoroutine(HitstopCoroutine(duration));
    }

    private IEnumerator HitstopCoroutine(float duration)
    {
        if (isHitStopping)
        {
            yield break;
        }
        isHitStopping = true;

        // Store original time scale
        float originalTimeScale = Time.timeScale;

        // Set time scale to zero (pause)
        Time.timeScale = 0.2f;

        // Wait for the duration in real-time
        yield return new WaitForSecondsRealtime(duration);

        // Restore time scale
        Time.timeScale = originalTimeScale;
        isHitStopping = false;
    }
    #endregion


    public void GameOver()
    {
      
        if (isGameOver)
        {
            return;
        }
        isGameOver = true;

        StartCoroutine(Enum_Gameover());
    }
    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneLoader.instance.LoadScene("SampleScene");
        }
    }
    IEnumerator Enum_Gameover()
    {
        yield return new WaitForSeconds(2f);
     
        yield return StartCoroutine(Leaderboard.instance.SubmitScoreRoutine(ScoreManager.instance.GetCurrentScore()));
        yield return StartCoroutine(Leaderboard.instance.FetechPersonalScoreRoutine());
        yield return StartCoroutine(Leaderboard.instance.FetechLeaderboardRoutine());

        gameOverCanvas.SetActive(true);
    }

    void Test()
    {
        for (int i = 0; i < enemyEntityList.Count; i++)
        {
            enemyEntityList[i].gameObject.SetActive(false);
        }
    }
}
