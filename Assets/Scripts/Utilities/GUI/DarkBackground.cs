using UnityEngine;
using System.Collections;

public class DarkBackground : Singleton<DarkBackground>
{
    public GameObject BackgroundObject;

    public static void Enable()
    {
        if (Instance != null && Instance.BackgroundObject != null)
        {
            Instance.BackgroundObject.SetActive(true);
        }
    }

    public static void Disable()
    {
        if (Instance != null && Instance.BackgroundObject != null)
        {
            Instance.BackgroundObject.SetActive(false);
            Debug.Log("Disable");
        }
    }
}
