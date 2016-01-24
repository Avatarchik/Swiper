using System;
using UnityEngine;
using System.Collections;
using TMPro;

public class FontController : Controller<FontController>
{
    public TextMeshProFont CurrentFont;

    public static bool TextSuppoted(string text)
    {
        if (Instance != null && Instance.CurrentFont != null)
        {
            foreach (var s in text)
            {
                int code = s;
                if (Instance.CurrentFont.characterDictionary.ContainsKey(code))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        else
        {
            Debug.LogError("FontsController or Font missed!");
            return false;
        }
    }
}
