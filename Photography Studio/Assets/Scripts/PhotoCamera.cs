using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCamera : MonoBehaviour
{

    public CustomRenderTexture PhotoCameraTexture;
    public Camera mainCamera;
    public Camera viewFinderCamera;
    public Camera photoCamera;
    private bool isUsingPhotoCamera = false;
    private bool isShutterOpen = false;

    /// <summary>
    /// Returns true if the player is aiming with the photo camera
    /// </summary>
    public bool IsUsingPhotoCamera
    {
        get { return isUsingPhotoCamera; }
    }

    public bool IsShutterOpen
    {
        get { return isShutterOpen; }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        viewFinderCamera.enabled = false;
        photoCamera.enabled = false;
        PhotoCameraTexture.Initialize();
    }

    void Update()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isShutterOpen)
        {
            photoCamera.Render();
            PhotoCameraTexture.Update();
        }
    }

    public void PressShutter()
    {
        if (isUsingPhotoCamera)
        {
            PhotoCameraTexture.Initialize();
            viewFinderCamera.enabled = false;
            isShutterOpen = true;
        }
        
    }

    public void ReleaseShutter()
    {
        if (isUsingPhotoCamera)
        {
            isShutterOpen = false;
            viewFinderCamera.enabled = true;
        }
        
    }

    public void ToggleCameraAim()
    {
        if (isUsingPhotoCamera)
        {
            mainCamera.enabled = true;
            viewFinderCamera.enabled = false;
            isUsingPhotoCamera = false;
        }
        else
        {
            mainCamera.enabled = false;
            viewFinderCamera.enabled = true;
            isUsingPhotoCamera = true;
        }
    }

}
