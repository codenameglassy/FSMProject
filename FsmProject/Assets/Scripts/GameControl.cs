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

    public void AddEnemy(Transform enemy)
    {
        enemyEntityList.Add(enemy);
    }
    public void RemoveEnemy(Transform enemy)
    {
        enemyEntityList.Remove(enemy);
    }

}
