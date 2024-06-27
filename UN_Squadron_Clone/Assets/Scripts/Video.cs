using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine;

public class Video : MonoBehaviour
{
    private VideoPlayer video;
 
    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;
    }
    
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("Level2");//the scene that you want to load after the video has ended.
    }
}
