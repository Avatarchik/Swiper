using UnityEngine;
using System.Collections;

public class PrefabSpawner : MonoBehaviour
{
    public SpriteRenderer Sprite;

    public GameObject[] Prefabs;

    public PrefabType PrefabType;

    public bool UseDefaultPosition;

    public Vector3 Position = new Vector3(0, 0, 0);

    public bool UseDefaultScale;

    public Vector3 Scale = new Vector3(1.0f, 1.0f, 1.0f);

    public bool UseDefaultRotation;

    public Vector3 Rotation = new Vector3(0, 0, 0);

    private void Start()
    {
        Sprite.gameObject.SetActive(false);
        var obj = CreateGameObject();
        obj.transform.SetParent(gameObject.transform);

        if (!UseDefaultPosition)
        {
            obj.transform.localPosition = Position;
        }
        if (!UseDefaultScale)
        {
            obj.transform.localScale = Scale;
        }
        if (!UseDefaultRotation)
        {
            obj.transform.localEulerAngles = Rotation;
        }
    }

    public GameObject CreateGameObject()
    {
        switch (PrefabType)
        {
            case PrefabType.Player:
                return Instantiate(Prefabs[0]);
            case PrefabType.RedVertical:
                return Instantiate(Prefabs[1]);
            case PrefabType.RedHorizontal:
                return Instantiate(Prefabs[2]);
            case PrefabType.Pusher:
                return Instantiate(Prefabs[3]);
            case PrefabType.Coin:
                return Instantiate(Prefabs[4]);
        }
        return null;
    }

}

public enum PrefabType
{
    Player, RedVertical, RedHorizontal, Pusher, Coin
}
