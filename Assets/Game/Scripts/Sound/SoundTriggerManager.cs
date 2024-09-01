using System;
using UnityEngine;
using System.Collections;

//This class will trigger all the sounds , all the sounds in this game is being played from this class

public class SoundTriggerManager : MonoBehaviour
{
    [SerializeField] private float musicVolume = 1f;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip playerEatSfx;
    [SerializeField] private VoidEventChannelSO playerScoreEvent;

    private void Start()
    {
        playerScoreEvent.RegisterListener(PlayPlayerEatSfx);
        SoundManager.instance.PlayMusicSequence(musicClips , musicVolume);
    }

    private void PlayPlayerEatSfx()
    {
        SoundManager.instance.PlayEffectSoundOneShot(playerEatSfx);
    }

    private void OnDestroy()
    {
        playerScoreEvent.UnregisterListener(PlayPlayerEatSfx);
    }
}
