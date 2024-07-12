using TMPro;
using UnityEngine;

public class PlayerScoreUI : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO playerScoreEvent;
    [SerializeField] private VoidEventChannelSO playerDecreaseScore;
    [SerializeField] private IntVariableSO playerScore;
    private TextMeshProUGUI textMeshProUGUI;
    private void Start()
    {
        playerScore.data = 0;
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = playerScore.data.ToString();
        playerScoreEvent.RegisterListener(IncreasePlayerScore);
        playerDecreaseScore.RegisterListener(DecreasePlayerScore);
    }

    private void IncreasePlayerScore()
    {
        playerScore.data++;
        textMeshProUGUI.text = playerScore.data.ToString();
    }
    
    private void DecreasePlayerScore()
    {
        playerScore.data--;
        textMeshProUGUI.text = playerScore.data.ToString();
    }

    private void OnDestroy()
    {
        playerScoreEvent.UnregisterListener(IncreasePlayerScore);
        playerDecreaseScore.UnregisterListener(DecreasePlayerScore);
    }
}
