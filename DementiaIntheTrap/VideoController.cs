using System.Collections;

using UnityEngine;

using UnityEngine.UI;

using UnityEngine.Video;



public class VideoController : MonoBehaviour

{

    RawImage Image;

    VideoPlayer vidio;

    AudioSource audio;

    void Awake()

    {

        vidio = gameObject.AddComponent<VideoPlayer>();

        audio = gameObject.AddComponent<AudioSource>();

        vidio.playOnAwake = false;

        audio.playOnAwake = false;

        audio.Pause();

        PlayVideo();

    }

    public void PlayVideo()

    {

        StartCoroutine(playVideo());

    }

    IEnumerator playVideo()

    {


        WaitForSeconds waitTime = new WaitForSeconds(44.0f);

        while (!vidio.isPrepared)

        {

            Debug.Log("동영상 준비중...");

            yield return waitTime;

        }

        Debug.Log("동영상이 준비가 끝났습니다.");

        Image.texture = vidio.texture;

        vidio.Play();

        audio.Play();

        Debug.Log("동영상이 재생됩니다.");

        while (vidio.isPlaying)

        {

            Debug.Log("동영상 재생 시간 : " + Mathf.FloorToInt((float)vidio.time));

            yield return null;

        }

        Debug.Log("영상이 끝났습니다.");

    }

}

