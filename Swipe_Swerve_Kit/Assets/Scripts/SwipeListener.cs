using UnityEngine;
using System;
using Listeners;

public class SwipeListener : MonoBehaviour
{
    #region variables for set script
    [SerializeField] private SwipeListenerType swipeType = SwipeListenerType.FourSides;
    [SerializeField] private float swipePersentage = 10f;

    private bool isListening = true;
    public Action<SwipeType> RunFunction;

    private bool isTouched;
    private Vector3 touchPos;
    private float xPersentage, yPersentage;
    #endregion

    private void Update()
    {
        SwipeInputListener();
    }

    private void SwipeInputListener()
    {
        if (isListening)
        {
            if (isTouched)
            {
                xPersentage = (Input.mousePosition.x - touchPos.x) * 100f / Screen.width;
                yPersentage = (Input.mousePosition.y - touchPos.y) * 100 / Screen.height;

                RunDecisionFunction(xPersentage, yPersentage);
            }

            if (Input.GetMouseButtonDown(0))
            {
                touchPos = Input.mousePosition;
                isTouched = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                touchPos = Vector3.zero;
                isTouched = false;
            }
        }  
    }

    private void RunDecisionFunction(float xPersentage, float yPersentage)
    {
        switch (swipeType)
        {
            case SwipeListenerType.LeftRight:
                LeftRightCalculator(xPersentage);
                break;
            case SwipeListenerType.UpDown:
                UpDownCalculator(yPersentage);
                break;
            case SwipeListenerType.FourSides:
                FourSidesCalculator(xPersentage, yPersentage);
                break;
            case SwipeListenerType.EightSides:
                EightSidesCalculator(xPersentage, yPersentage);
                break;
        }
    }

    private bool LeftRightCalculator(float xPersentage)
    {
        if (xPersentage >= swipePersentage)
        {
            DoSwipe(SwipeType.RightSwipe);
            return true;
        }
        else if (xPersentage <= -swipePersentage)
        {
            DoSwipe(SwipeType.LeftSwipe);
            return true;
        }

        return false;
    }
    private bool UpDownCalculator(float yPersentage)
    {
        if (yPersentage >= swipePersentage)
        {
            DoSwipe(SwipeType.UpSwipe);
            return true;
        }
        else if (yPersentage <= -swipePersentage)
        {
            DoSwipe(SwipeType.DownSwipe);
            return true;
        }
        return false;
    }
    private void FourSidesCalculator(float xPersentage, float yPersentage)
    {
        bool isbreak = LeftRightCalculator(xPersentage);
        if (isbreak)
            return;
        UpDownCalculator(yPersentage);
    }
    private void EightSidesCalculator(float xPersentage, float yPersentage)
    {
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(xPersentage, 2) * Mathf.Pow(yPersentage, 2));

        if (hypotenuse >= 10)
        {
            if (xPersentage > 3.5 && yPersentage > 3.5)
            {
                DoSwipe(SwipeType.RightUpSwipe);
                return;
            }
            if (xPersentage > 3.5 && yPersentage < -3.5)
            {
                DoSwipe(SwipeType.RightDownSwipe);
                return;
            }
            if (xPersentage < -3.5 && yPersentage < -3.5)
            {
                DoSwipe(SwipeType.LeftDownSwipe);
                return;
            }
            if (xPersentage < -3.5 && yPersentage > 3.5)
            {
                DoSwipe(SwipeType.LeftUpSwipe);
                return;
            }
        }

        bool isbreak = LeftRightCalculator(xPersentage);
        if (isbreak)
            return;
        UpDownCalculator(yPersentage);
    }

    private void DoSwipe(SwipeType swipeType)
    {
        isTouched = false;
        RunFunction?.Invoke(swipeType);
    }

    public void StartListening()
    {
        isListening = true;
    }

    public void StopListening()
    {
        isListening = false;
    }
}