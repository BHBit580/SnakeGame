using UnityEngine;
using System.Collections;

public class SoundTriggerManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private float fadeTime = 1f;

    private int currentClipIndex = 0;

    private void Start()
    {
        StartCoroutine(PlayMusicSequence());
    }

    private IEnumerator PlayMusicSequence()
    {
        while (true)
        {
            if (musicClips.Length > 0)
            {
                AudioClip currentClip = musicClips[currentClipIndex];

                // Fade in
                SoundManager.instance.FadeInMusic(currentClip, fadeTime, 1f,  SoundManager.instance.GetMusicSource());

                // Wait for clip to finish
                yield return new WaitForSeconds(currentClip.length - fadeTime);

                // Fade out
                SoundManager.instance.FadeOutMusic(fadeTime, SoundManager.instance.GetMusicSource());
                yield return new WaitForSeconds(fadeTime);

                // Move to next clip
                currentClipIndex = (currentClipIndex + 1) % musicClips.Length;
            }
            else
            {
                Debug.LogWarning("No music clips assigned to SoundTriggerManager.");
                yield break;
            }
        }
    }
}
