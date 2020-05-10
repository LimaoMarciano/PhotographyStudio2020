using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCamera : MonoBehaviour
{

    public CustomRenderTexture PhotoCameraTexture;
    public Camera photoCamera;
    public Camera viewFinderCamera;
    private Camera mainCamera;
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
        mainCamera = Camera.main;
        mainCamera.enabled = true;
        viewFinderCamera.enabled = false;
        photoCamera.enabled = false;
        PhotoCameraTexture.Initialize();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
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

    // Update is called once per frame
    void LateUpdate()
    {
        if (isUsingPhotoCamera)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                PhotoCameraTexture.Initialize();
                viewFinderCamera.enabled = false;
                isShutterOpen = true;
            }

            if (Input.GetButton("Fire1"))
            {
                photoCamera.Render();
                PhotoCameraTexture.Update();
            }

            if (Input.GetButtonUp("Fire1"))
            {

                viewFinderCamera.enabled = true;
                isShutterOpen = false;
            }
        }

    }

}
