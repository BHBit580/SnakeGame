using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class SnakeData
{
    public string name;
    public int score;
}
//This code is not great and can be improved.

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private IntVariableSO playerScore;

    [SerializeField] private Color playerColor;
    [SerializeField] private GameObject names, scores , rank;
    [SerializeField] private GameObject enemySpawner;
    public int playerRank { get;private set;}
    private List<SnakeData> leaderBoardDataList = new List<SnakeData>();
    private int numberToDisplay = 5;

    private void Update()
    {
        UpdateEnemyData();
        UpdateUIData();
    }

    private void UpdateEnemyData()
    {
        leaderBoardDataList.Clear();

        for (int i = 0; i < enemySpawner.transform.childCount; i++)
        {
            Transform enemyTransform = enemySpawner.transform.GetChild(i);
            SnakeHead enemyHead = enemyTransform.GetComponentInChildren<SnakeHead>();
            SnakeData snakeData = new SnakeData
            {
                name = enemyTransform.name,
                score = enemyHead.scoreValue
            };

            leaderBoardDataList.Add(snakeData);
        }

        if (player != null)
        {
            SnakeData playerData = new SnakeData
            {
                name = "You",
                score = playerScore.data
            };

            leaderBoardDataList.Add(playerData);
        }
    }

    private void UpdateUIData()
    {
        List<SnakeData> topScorers = GetTopScorers();
        for (int i = 0; i < topScorers.Count; i++)
        {
            names.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = topScorers[i].name;
            scores.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = topScorers[i].score.ToString();

            if (names.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text == "You")
            {
                names.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = playerColor;
                rank.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = playerColor;
                scores.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = playerColor;
            }
            else
            {
                rank.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = Color.white;
                scores.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = Color.white;
                names.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = Color.white;
            }
        }

        if(player ==null) return;

        var sortedList = leaderBoardDataList.OrderByDescending(e => e.score).ToList();
        playerRank = sortedList.FindIndex(e => e.name == "You") + 1;
        int playerIndex = names.transform.childCount - 1;


        bool isActive = playerRank > numberToDisplay;
        names.transform.GetChild(playerIndex).gameObject.SetActive(isActive);
        scores.transform.GetChild(playerIndex).gameObject.SetActive(isActive);
        rank.transform.GetChild(playerIndex).gameObject.SetActive(isActive);

        if (isActive)
        {
            scores.transform.GetChild(playerIndex).GetComponent<TextMeshProUGUI>().text = playerScore.data.ToString();
            rank.transform.GetChild(playerIndex).GetComponent<TextMeshProUGUI>().text = playerRank + ".";
        }
    }

    private List<SnakeData> GetTopScorers()
    {
        return leaderBoardDataList.OrderByDescending(e => e.score).Take(numberToDisplay).ToList();
    }
}
