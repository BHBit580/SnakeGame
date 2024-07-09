using UnityEngine;

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

    private void OnDestroy()
    {
        playerDiedEvent.UnregisterListener(ShowDeathUI);
    }
}
