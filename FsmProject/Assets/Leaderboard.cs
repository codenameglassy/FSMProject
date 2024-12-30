using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance { get; private set; }
    string leaderboardId = "25931";

    public TextMeshProUGUI playerNamesTxt;
    public TextMeshProUGUI playerScoresTxt;
    public TextMeshProUGUI personalNameAndScoreTxt;

    private void Awake()
    {
        instance = this;
    }

    public void SubmitScore(int scoreToUpload)
    {
        StartCoroutine(SubmitScoreRoutine(scoreToUpload));
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerId = PlayerPrefs.GetString("PlayerId");
        LootLockerSDKManager.SubmitScore(playerId, scoreToUpload, leaderboardId, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Sucessfully upload the score");
                done = true;
            }
            else
            {
                Debug.Log("Failed " + response.errorData);
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator FetechLeaderboardRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardId, 10, 0, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Sucessfully feteched leaderboard");

                string tempPlayerNames = "Ranks\n <br>";
                string tempPlayerScores = "Highscores\n <br>";

                LootLockerLeaderboardMember[] member = response.items;

                for (int i = 0; i < member.Length; i++)
                {
                    tempPlayerNames += member[i].rank + ". ";
                  /*  if (member[i].player.name != "")
                    {
                        tempPlayerNames += member[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += member[i].player.id;
                    }*/
                    tempPlayerScores += member[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNamesTxt.text = tempPlayerNames;
                playerScoresTxt.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed fetching leaderboard" + response.errorData);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public void FetechPersonalScore()
    {
        StartCoroutine(FetechPersonalScoreRoutine());
    }

    public IEnumerator FetechPersonalScoreRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetMemberRank(leaderboardId, PlayerManager.Instance.GetPlayerID(), (response) =>
        {
            if (response.success)
            {
                Debug.Log("Sucessfully feteched player's perosnal score");
                Debug.Log("Player's score: " + response.score);
                Debug.Log("Player's rank: " + response.rank + response.player.id);
                //personalNameAndScoreTxt.text = "Personal Score: " + response.rank + ". " + response.player.id + response.score;
                personalNameAndScoreTxt.text = "Your rank: " + response.rank + "\n Your Score: " + response.score;
                done = true;
            }
            else
            {
                Debug.Log("Failed to get player's score");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
