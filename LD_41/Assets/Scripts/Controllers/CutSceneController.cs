using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour {

    public Texture[] frames;
    public float frameTime = 3.0f;

    private int frame = 0;
    private float nextFrameTime = 0;

    void OnGui()
    {
        if (frame < frames.Length)
        {
            if (Time.time >= nextFrameTime)
            {
                frame++;
                nextFrameTime += frameTime;
            }
            GUI.DrawTexture(Rect(0, 0, Screen.width, Screen.height), frames[frame]);
        }
    }
}
