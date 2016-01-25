using UnityEngine;
using UnityEngine.Events;
using System;

public class GameSwipeDetector : MonoBehaviour
{
    [Serializable]
    public class VectorEvent : UnityEvent<Vector2> { }

    public bool _isStretchMode = false;

    public float SwipeTime = 0.1f;

    private float _swipeTimer;

    private bool _skipTimer;

    [Header("Swipe Events")]
    public UnityEvent OnSwipeLeft;

    public UnityEvent OnSwipeRight;

    public UnityEvent OnSwipeUp;

    public UnityEvent OnSwipeDown;

    public UnityEvent OnSwipeLeftComplete;

    public UnityEvent OnSwipeRightComplete;

    public UnityEvent OnSwipeUpComplete;

    public UnityEvent OnSwipeDownComplete;

    [Header("Stretch Events")]
    public VectorEvent OnStretchUpdate;

    public VectorEvent OnStretchComplete;

    private Action _onUpdate;

    private Action<bool> _onCheckInput;

    private Vector2 _startFingerPosition;

    private Vector2 _finishFingerPosition;

    private int _startFingerId;

    private float _swipeMinLength;

    public Vector2 CurrentVector
    {
        get
        {
            return _finishFingerPosition - _startFingerPosition;
        }
    }


    public void Init()
    {
        Input.multiTouchEnabled = false;

        float swipeProcent = 0.03f; 

        if (Application.isMobilePlatform)
        {
            _swipeMinLength = Screen.height * swipeProcent;
            _onUpdate = UpdateMobile;
        }
        else
        {
            _swipeMinLength = 15;
            _onUpdate = UpdateStandalone;
        }

        _onCheckInput = null;
        if (_isStretchMode)
        {
            _onCheckInput = StretchUpdate;
        }
        else
        {
            _onCheckInput = CheckInput;
        }

    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _onUpdate();
    }

    private void UpdateMobile()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _swipeTimer = 0;
                _startFingerId = touch.fingerId;
                _startFingerPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && _startFingerId == touch.fingerId)
            {
                _swipeTimer += Time.deltaTime;
                _finishFingerPosition = touch.position;
                _onCheckInput.Invoke(false);
            }
            else if (touch.phase == TouchPhase.Ended && _startFingerId == touch.fingerId)
            {
                _swipeTimer += Time.deltaTime;
                _finishFingerPosition = touch.position;
                _onCheckInput.Invoke(true);
            }
        }
    }

    private void UpdateStandalone()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _swipeTimer = 0;
            _skipTimer = false;
            _startFingerPosition = Input.mousePosition;
            _finishFingerPosition = Input.mousePosition;
            _onCheckInput.Invoke(false);
        }
        else if (Input.GetMouseButton(0))
        {
            _swipeTimer += Time.deltaTime;
            _finishFingerPosition = Input.mousePosition;
            _onCheckInput.Invoke(false);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _finishFingerPosition = Input.mousePosition;
            _onCheckInput.Invoke(true);
        }
    }

    private void CheckInput(bool touchEnd = false)
    {
        Vector2 delta = _startFingerPosition - _finishFingerPosition;
        Vector2 deltaSz = new Vector2(Mathf.Abs(delta.x), Mathf.Abs(delta.y));

        if ((deltaSz.y > deltaSz.x) && deltaSz.y > _swipeMinLength)
        {
            if (delta.y > 0f)
            {
                if (touchEnd)
                {
                    OnSwipeDownComplete.Invoke();
                }

                if (_swipeTimer >= SwipeTime && !_skipTimer)
                {
                    _skipTimer = true;
                    OnSwipeDown.Invoke();
                }
            }
            else
            {
                if (touchEnd)
                {
                    OnSwipeUpComplete.Invoke();
                }

                if (_swipeTimer >= SwipeTime && !_skipTimer)
                {
                    _skipTimer = true;
                    OnSwipeUp.Invoke();
                }
            }
        }
        else
        {
            if ((deltaSz.x > deltaSz.y) && deltaSz.x > _swipeMinLength)
            {
                if (delta.x > 0f)
                {
                    if (touchEnd)
                    {
                        OnSwipeLeftComplete.Invoke();
                    }

                    if (_swipeTimer >= SwipeTime && !_skipTimer)
                    {
                        _skipTimer = true;
                        OnSwipeLeft.Invoke();
                    }
                }
                else
                {
                    if (touchEnd)
                    {
                        OnSwipeRightComplete.Invoke();
                    }

                    if (_swipeTimer >= SwipeTime && !_skipTimer)
                    {
                        _skipTimer = true;
                        OnSwipeRight.Invoke();
                    }
                }
            }
        }
    }


    private void StretchUpdate(bool complete = false)
    {
        if (complete)
        {
            OnStretchComplete.Invoke(CurrentVector);
        }
        else
        {
            OnStretchUpdate.Invoke(CurrentVector);
        }
    }



}
