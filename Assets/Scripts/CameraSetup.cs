using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraSetup : MonoBehaviour
{
    public bool IsTateMode = false;
    public Vector3 DefaultEuler = new Vector3(90, 0, 90);
    public Vector3 TateEuler = new Vector3(90, 0, 0);
    public Vector2 XYScale = new Vector2(16, 9);

    int lastWidth = 0, lastHeight = 0;
    private bool lastTateModeSetting = false;

    private void Update()
    {
        if(this.lastWidth != Screen.width || this.lastHeight != Screen.height || this.lastTateModeSetting != this.IsTateMode)
        {
            this.SetCameraResolution();
        }
    }

    void Start()
    {
        SetCameraResolution();
    }

    public void SwitchTATEMode()
    {
        this.IsTateMode = !this.IsTateMode;
    }

    void SetCameraResolution()
    {
        if ((this.lastWidth = Screen.width) >= (this.lastHeight = Screen.height) && !IsTateMode)
        {
            Camera.main.transform.gameObject.transform.eulerAngles = DefaultEuler;

            float height = this.lastHeight * (this.XYScale.y / this.XYScale.x);//(float)this.lastWidth / (16f / 9f);
            float newWidth = (int)((float)height * (this.XYScale.x / this.XYScale.y));

            height = (int)((float)height * ((float)lastWidth / newWidth));
            newWidth *= lastWidth / newWidth;

            int heightOffset = this.lastHeight - (int)height;
            heightOffset /= 2;

            Camera.main.pixelRect = new Rect(0, heightOffset, (int)newWidth, height);
            Camera.main.fieldOfView = 90f;
            //Debug.Log(Camera.main.pixelRect);
        }
        else
        {
            //TATE MODE
            Camera.main.transform.gameObject.transform.eulerAngles = TateEuler;

            float width = (float)this.lastHeight / (this.XYScale.x / this.XYScale.y);
            int widthOffset = this.lastWidth - (int)width;
            widthOffset /= 2;
            Camera.main.pixelRect = new Rect(widthOffset, 0, width, Screen.height);
            Camera.main.fieldOfView = Camera.VerticalToHorizontalFieldOfView(90f, this.XYScale.x / this.XYScale.y);
            //Debug.Log(Camera.main.pixelRect);
        }

        this.lastTateModeSetting = this.IsTateMode;
    }
}
