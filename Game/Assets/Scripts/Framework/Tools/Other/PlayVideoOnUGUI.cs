using System;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

//[AddComponentMenu("Video/PlayVideoOnUGUI")]
public class PlayVideoOnUGUI : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private RawImage rawImage;
    public TextMeshProUGUI timeTxt;
    public Slider VideoTimeSlider;
    public Slider AudioVolumeSlider;
    private bool videoTimeChanged;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        rawImage = GetComponent<RawImage>();
        VideoTimeSlider.onValueChanged.AddListener(SetVideoTime);
        AudioVolumeSlider.onValueChanged.AddListener(SetAudioVolume);
        HandleEvent();
    }

    private void Update()
    {
        if (videoPlayer == null
            || videoPlayer.clip == null
            || videoPlayer.texture == null)
        {
            return;
        }

        rawImage.texture = videoPlayer.texture;
        var clip = videoPlayer.clip;
        var time = videoPlayer.time;
        var clipHour = (int) clip.length / 3600;
        var clipMinute = (int) (clip.length - clipHour * 3600) / 60;
        var clipSecond = (int) (clip.length - clipHour * 3600 - clipMinute * 60);
        var currentHour = (int) time / 3600;
        var currentMinute = (int) (time - currentHour * 3600) / 60;
        var currentSecond = (int) (time - currentHour * 3600 - currentMinute * 60);
        timeTxt.text = $"{currentHour:D2}:{currentMinute:D2}:{currentSecond:D2} / {clipHour:D2}:{clipMinute:D2}:{clipSecond:D2}";
        if (!videoTimeChanged && VideoTimeSlider != null)
        {
            VideoTimeSlider.value = (float) (videoPlayer.time / videoPlayer.clip.length);
        }
    }

    private void HandleEvent()
    {
        if (videoPlayer == null)
        {
            return;
        }
        
        videoPlayer.prepareCompleted -= OnVideoPrepare;
        videoPlayer.started -= OnStarted;
        videoPlayer.errorReceived -= OnErrorReceived;
        
        videoPlayer.prepareCompleted += OnVideoPrepare;
        videoPlayer.started += OnStarted;
        videoPlayer.errorReceived += OnErrorReceived;
    }

    public void Play(VideoClip videoClip)
    {
        if (videoPlayer == null
            || rawImage == null
            || videoClip == null)
        {
            return;
        }

        if (VideoTimeSlider != null)
        {
            VideoTimeSlider.value = 0;
        }

        if (AudioVolumeSlider != null)
        {
            AudioVolumeSlider.value = 1;
        }

        videoPlayer.clip = videoClip;
        videoPlayer.Prepare();
    }

    public void Play()
    {
        if (videoPlayer == null
            || videoPlayer.clip == null)
        {
            return;
        }

        videoPlayer.Play();
    }

    public void Stop()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
    }

    public void Pause()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
        }
    }

    protected void OnVideoPrepare(VideoPlayer source)
    {
        if (source != null && source.clip != null)
        {
            if (source.isPrepared)
            {
                source.Play();
            }
        }
    }
    
    protected void OnStarted(VideoPlayer source)
    {
        Debug.LogError("OnStarted");
    }

    protected void OnErrorReceived(VideoPlayer source, string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
        {
            Debug.LogError(message);
        }
    }

    public void SetAudioVolume(float volume)
    {
        if (videoPlayer == null
            || videoPlayer.clip == null
            || Math.Abs(videoPlayer.GetDirectAudioVolume(0) - volume) < 0.001f)
        {
            return;
        }

        videoPlayer.SetDirectAudioVolume(0, volume);
    }

    public void SetVideoTime(float volume)
    {
        if (videoPlayer == null
            || videoPlayer.clip == null
            || Math.Abs(videoPlayer.time - volume * videoPlayer.clip.length) < 0.001f
            || !videoTimeChanged)
        {
            return;
        }

        videoPlayer.time = volume * videoPlayer.clip.length;
    }

    public void OnBeginDrag()
    {
        videoTimeChanged = true;
    }

    public void OnEndDrag()
    {
        videoTimeChanged = false;
    }
}