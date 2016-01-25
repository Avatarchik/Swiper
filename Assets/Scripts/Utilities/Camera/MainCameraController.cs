using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MainCameraController : Singleton<MainCameraController>
{
    public static Camera GameCamera
    {
        get { return Instance._gameCamera; }
    }

    private Camera _gameCamera;

    public float TargetAspect;

    public float TargetOrtographicSize;

    public Transform Transform;

    public override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _gameCamera = GetComponent<Camera>();
            Transform = gameObject.transform;
            if (SingletonType == SingletonType.Indestructible)
            {
                DontDestroyOnLoad(gameObject);
            }
            CalculateOrtographicSize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CalculateOrtographicSize()
    {
        float windowAspect = (float) Screen.width/Screen.height;
        float scaleHeight = windowAspect / TargetAspect;

        _gameCamera.orthographicSize = TargetOrtographicSize/scaleHeight;
    }

    public override void Update()
    {
		CalculateOrtographicSize();
    }
}
