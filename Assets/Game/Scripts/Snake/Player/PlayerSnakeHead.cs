using UnityEngine;

public class PlayerSnakeHead : SnakeHead
{
    [SerializeField] private VoidEventChannelSO playerDied;
    [SerializeField] private VoidEventChannelSO playerScoreEvent;
    
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Food"))
        {
            playerScoreEvent?.RaiseEvent();
        }
    }

    private void OnDestroy()
    {
        playerDied?.RaiseEvent();
    }
}
