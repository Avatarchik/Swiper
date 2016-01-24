using UnityEngine;

public interface ICollisiable
{
    void OnCollisionEnter2D(Collision2D obj);

    void OnCollisionExit2D(Collision2D obj);

    void OnCollisionStay2D(Collision2D obj);
}
