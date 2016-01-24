using UnityEngine;
using System.Collections;

public interface IMousable
{
    void OnMouseDown();

    void OnMouseUp();

    void OnMouseEnter();

    void OnMouseOver();

    void OnMouseExit();

    void OnMouseDrag();

    void OnMouseUpAsButton();
}