using UnityEngine;
using System.Collections;

public class MusicController : Controller<MusicController>
{
    public AudioSource AudioSource;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (MainCameraController.Instance == null)
        {
            gameObject.transform.position = Vector3.zero;
        }
        else
        {
            gameObject.transform.position = MainCameraController.Instance.gameObject.transform.position;
        }
    }

    public static void EnableMusic()
    {
        if (Instance != null)
        {
            Instance.AudioSource.volume = 0.2f;
        }
    }

    public static void DisableMusic()
    {
        if (Instance != null)
        {
            Instance.AudioSource.volume = 0.0f;
        }        
    }
}
