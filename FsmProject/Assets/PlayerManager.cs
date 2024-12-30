using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    private string playerID;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        StartCoroutine(SetupRoutine());
    }
    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine(); //login
        yield return Leaderboard.instance.FetechLeaderboardRoutine(); // get leaderboard
        //yield return SetPlayerNameRoutine(); //set name
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player Logged In");
                PlayerPrefs.SetString("PlayerId", response.player_id.ToString());
                playerID = response.player_id.ToString();
                done = true;
            }
            else
            {
                Debug.Log("Couldnot start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    public string GetPlayerID()
    {
        return playerID;
    }
}
