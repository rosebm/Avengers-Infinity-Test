using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {

    //Raw Image to Show Video Images [Assign from the Editor]
    public RawImage image;
    //Video To Play [Assign from the Editor]
    public VideoClip videoToPlay;
    public GameObject playButton;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    //Audio
    private AudioSource audiosource;
    private bool first = true;
    private bool pause = false;

    // Update is called once per frame
    //void Update()
    //{
    //    if (videoPlayer.isPlaying)
    //    {
    //        ShowPlayButton(false);
    //    }
    //    else
    //    {
    //        ShowPlayButton(true);
    //    }

    //}

    //private void OnApplicationPause(bool pause)
    //{
    //    if (pause)
    //        Pause();
    //}

    public void ShowPlayButton(bool visible)
    {
        //playButton.enabled = visible;
        //audioSource.Stop();
    }

    public void Play()
    {
        if (!first && !pause){
            videoPlayer.Pause();
            audiosource.Pause();
            playButton.SetActive(true);
            pause = true;
        }else if (!first && pause){
            videoPlayer.Play();
            audiosource.Play();
            playButton.SetActive(false);
            pause = false;
        }else{
            StartCoroutine(playVideo());
        }
    }

    public void Pause()
    {
        PauseAudio(true);
        videoPlayer.Pause();
    }

    public void PauseAudio(bool pause)
    {
        if (pause)
            audiosource.Pause();
        else
            audiosource.Play();
    }

    IEnumerator playVideo()
    {
        first = false;
        playButton.SetActive(false);
        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        //Add AudioSource
        audiosource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audiosource.playOnAwake = false;

        //We want to play from video clip not from url
        videoPlayer.source = VideoSource.VideoClip;

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.controlledAudioTrackCount = 1;             // <-- We have added this line. It tells video player that you will have one audio track playing in Unity AudioSource.
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audiosource);

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return null;
        }

        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;

        //Play Video
        Play();
        //videoPlayer.Play();

        //Play Sound
        //audio.Play();

        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }

        Debug.Log("Done Playing Video");
    }
}
