using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource musicSource , effectSource;
    private int currentClipIndex = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public AudioSource GetMusicSource()
    {
        return musicSource;
    }

    public void PlayEffectSoundOneShot(AudioClip clip , float volume = 1f)
    {
        effectSource.volume = volume;
        effectSource.PlayOneShot(clip);
    }

    public void PlayMusicOneShot(AudioClip clip , float volume = 1f)
    {
        musicSource.volume = volume;
        musicSource.PlayOneShot(clip);
    }

    public void PlayMusicLoop(AudioClip clip, float volume = 1f)
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void PlayEffectSoundLoop(AudioClip clip, float volume = 1f)
    {
        effectSource.clip = clip;
        effectSource.volume = volume;
        effectSource.Play();
        effectSource.loop = true;
    }

    public void FadeOutMusic(float fadeTime , AudioSource audioSource)
    {
        StartCoroutine(FadeOut(audioSource, fadeTime));
    }

    private IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void FadeInMusic(AudioClip clip , float fadeTime , float volume, AudioSource audioSource)
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
        StartCoroutine(FadeIn(audioSource, fadeTime));
        musicSource.loop = true;
    }

    private IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float startVolume = 0.2f;

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.volume = 1f;
    }


    public void PlayMusicSequence(AudioClip[] musicClips , float musicVolume = 1)
    {
        StartCoroutine(PlayMusicSequenceCoroutine(musicClips , musicVolume));
    }

    private IEnumerator PlayMusicSequenceCoroutine(AudioClip[] musicClips , float musicVolume)
    {
        while (true)
        {
            if (musicClips.Length > 0)
            {
                AudioClip currentClip = musicClips[currentClipIndex];
                PlayMusicOneShot(currentClip , musicVolume);

                // Wait for clip to finish
                yield return new WaitForSeconds(currentClip.length);

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

    public void StopMusic()
    {
        musicSource.Stop();
        musicSource.loop = false;
    }

    public void StopEffect()
    {
        effectSource.Stop();
        effectSource.loop = false;
    }


}
