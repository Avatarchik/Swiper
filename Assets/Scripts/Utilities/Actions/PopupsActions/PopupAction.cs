using System.Collections;
using AraxisTools;
using UnityEngine;

public class PopupAction<T> : Action where T : Popup<T>
{
    public override IEnumerable NextFrame()
    {
        OnStart();

        DarkBackground.Enable();

        Popup<T>.Instance = ResourcesController.Instance.LoadPopup(typeof(T)).GetComponent<T>();
        Popup<T>.Instance.gameObject.transform.SetParent(Object.FindObjectOfType<Canvas>().transform);
        var rectTransform = Popup<T>.Instance.gameObject.GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one;
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);
        Popup<T>.Instance.Hide += EndEvent;
        Popup<T>.Instance.IsActive = true;
        Popup<T>.Instance.ShowPopup();
        SoundController.PlayPopup();

        while (Popup<T>.Instance.IsActive)
        {
            yield return null;
        }

        ResourcesController.Instance.UnloadPopup();
        Popup<T>.Instance.Destroy();

        OnFinish();
    }

    public void EndEvent(T popup)
    {
        IsActive = false;
        DarkBackground.Disable();
    }

}