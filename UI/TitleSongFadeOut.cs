using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSongFadeOut : MonoBehaviour
{
    private AudioSource titleSong;

    [SerializeField] float fadeOutDuration = 10f;

    void Start()
    {
        titleSong = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float currentTime = 0;

        float startVolume = titleSong.volume;
        while (currentTime < fadeOutDuration)
        {
            titleSong.volume = Mathf.Lerp(startVolume, 0, currentTime / fadeOutDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
        yield break;
    }
}
