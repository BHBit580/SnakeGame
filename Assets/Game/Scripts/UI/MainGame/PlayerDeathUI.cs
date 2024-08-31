using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private TextMeshProUGUI playerRank;
    [SerializeField] private IntVariableSO playerScoreDataSO;
    [SerializeField] private LeaderBoardManager leaderBoardManager;
    [SerializeField] private VoidEventChannelSO playerDiedEvent;

    private void OnEnable()
    {
        playerDiedEvent.RegisterListener(ShowDeathUI);
    }

    private void Start()
    {
        SetActiveAllChildren(false);
    }

    private void ShowDeathUI()
    {
        SetActiveAllChildren(true);
        playerScore.text = "SCORE - " + playerScoreDataSO.data.ToString();
        playerRank.text = "RANK - " +  leaderBoardManager.playerRank.ToString();
    }

    private void SetActiveAllChildren(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToSceneMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        playerDiedEvent.UnregisterListener(ShowDeathUI);
    }
}
