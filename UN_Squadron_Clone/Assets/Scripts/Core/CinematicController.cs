using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CinematicController : MonoBehaviour
{
    [SerializeField] private VideoPlayer video1;
    [SerializeField] private GameObject video1Renderer;
    [SerializeField] private VideoPlayer video2;
    [SerializeField] private GameObject video2Renderer;

    private void OnEnable()
    {
        video1.loopPointReached += TransitionToVideo;
        video2.loopPointReached += (VideoPlayer source) => SceneManager.LoadScene(0);
    }

    private void TransitionToVideo(VideoPlayer source)
    {
        ActivateVideo(video1, video1Renderer, false);
        ActivateVideo(video2, video2Renderer, true);
    }

    private void ActivateVideo(VideoPlayer video, GameObject videoRenderer, bool activated)
    {
        video.gameObject.SetActive(activated);
        videoRenderer.SetActive(activated);
    }
} 