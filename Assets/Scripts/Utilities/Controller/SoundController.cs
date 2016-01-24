using UnityEngine;
using System.Collections;

public class SoundController : Controller<SoundController>
{
    public AudioSource AudioSource;

    public AudioClip ButtonClickSound;

    public AudioClip GamePlayerDeathSound;

    public AudioClip GamePlayerGluedSound;

    public AudioClip GamePlayerJumpSound;

    public AudioClip GetCoinSound;

    public AudioClip ItemBoughtSound;

    public AudioClip PopupSound;

    public AudioClip StartLevelSound;

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

    public static void EnableSound()
    {
        if (Instance != null)
        {
            Instance.AudioSource.volume = 1.0f;
        }
    }

    public static void DisableSound()
    {
        if (Instance != null)
        {
            Instance.AudioSource.volume = 0.0f;
        }
    }

    public static void PlayButtonClick()
    {
        if (Instance != null)
        {
            Instance.AudioSource.PlayOneShot(Instance.ButtonClickSound);
        }
    }

    public static void PlayGamePlayerDeath()
    {
        if (Instance != null)
        {
            Instance.AudioSource.PlayOneShot(Instance.GamePlayerDeathSound);
        }
    }

    public static void PlayGamePlayerGlued()
    {
        if (Instance != null)
        {
            Instance.AudioSource.PlayOneShot(Instance.GamePlayerGluedSound, 4.0f);
        }
    }

    public static void PlayGamePlayerJump()
    {
        if (Instance != null)
        {
            Instance.AudioSource.PlayOneShot(Instance.GamePlayerJumpSound, 1.0f);
        }
    }

    public static void PlayGetCoin()
    {
        if (Instance != null)
        {
            Instance.AudioSource.PlayOneShot(Instance.GetCoinSound);
        }
    }

    public static void PlayItemBought()
    {
        if (Instance != null)
        {
            Instance.AudioSource.PlayOneShot(Instance.ItemBoughtSound);
        }
    }

    public static void PlayPopup()
    {
        if (Instance != null)
        {
            Instance.AudioSource.PlayOneShot(Instance.PopupSound);
        }
    }

    public static void PlayStartLevel()
    {
        if (Instance != null)
        {
            Instance.AudioSource.PlayOneShot(Instance.StartLevelSound);
        }
    }

}
