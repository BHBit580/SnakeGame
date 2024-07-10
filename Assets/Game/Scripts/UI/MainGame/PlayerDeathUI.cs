using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathUI : MonoBehaviour
{
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
