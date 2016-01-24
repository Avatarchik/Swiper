using UnityEngine;

public interface ITriggerable
{
    void OnTriggerEnter2D(Collider2D obj);

    void OnTriggerExit2D(Collider2D obj);

    void OnTriggerStay2D(Collider2D obj);
}
