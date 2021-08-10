using System;
using UnityEngine;
using Listeners;

public class SwerveListener : MonoBehaviour
{
    #region variables for set script
    [SerializeField] private SwerverType swerverType = SwerverType.LeftRight;
    private bool isListening = true;
    public Action<float, float> RunFunction;

    private bool isTouched;
    private Vector3 touchPos;
    private float xPersentage, yPersentage;
    #endregion

    private void Update()
    {
        SwerveInputListener();
    }

    private void SwerveInputListener()
    {
        if (isListening)
        {
            if (isTouched)
            {
                xPersentage = (Input.mousePosition.x - touchPos.x) * 100f / Screen.width;
                yPersentage = (Input.mousePosition.y - touchPos.y) * 100 / Screen.height;

                ReturnValue(xPersentage, yPersentage);
            }

            if (Input.GetMouseButton(0))
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

    private void ReturnValue(float xPersentage, float yPersentage)
    {
        if (swerverType.Equals(SwerverType.LeftRight))
            RunFunction?.Invoke(xPersentage, 0f);
        else if (swerverType.Equals(SwerverType.UpDown))
            RunFunction?.Invoke(0f, yPersentage);
        else if(swerverType.Equals(SwerverType.FourSides))
            RunFunction?.Invoke(xPersentage, yPersentage);
    }
}