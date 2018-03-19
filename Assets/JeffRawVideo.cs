using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;


public class JeffRawVideo : MonoBehaviour
{

    RenderTexture text;
    private VideoPlayer player;
    private RawImage image;

    public void VideoEnable()
    {
        image.enabled = true;
    }
    public void Play()
    {
        player.Play();
    }


    void Awake()
    {
        player = GetComponent<VideoPlayer>();
        image = GetComponent<RawImage>();
        text = new RenderTexture((int)player.clip.width, (int)player.clip.height, 0);

        player.targetTexture = text;
        image.texture = text;

        Vector3 scale = image.transform.localScale;

        scale.y = player.clip.height / (float)player.clip.width * scale.y;

        image.transform.localScale = scale;
        player.loopPointReached += EndVideo;
    }

    void EndVideo(VideoPlayer vp)
    {
        image.enabled = false;
        player.frame = 0;
    }

}
