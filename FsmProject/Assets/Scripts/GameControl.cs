using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{

    public static GameControl instance;


    [Header("Runaway-points")]
    public List<Transform> runawayPoints = new List<Transform>();

    [Header("Enemy")]
    public List<Transform> enemyEntityList = new List<Transform>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

}
