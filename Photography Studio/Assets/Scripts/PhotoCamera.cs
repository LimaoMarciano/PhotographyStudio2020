using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class PhotoCamera : MonoBehaviour
{

    public CustomRenderTexture PhotoCameraTexture;
    public Camera mainCamera;
    public Camera viewFinderCamera;
    public Camera photoCamera;
    public float zoomSensitivity = 0.5f;
    public float minFocalLenght = 35f;
    public float maxFocalLenght = 75f;
    public float[] apertures;
    public float[] shutterSpeeds;

    private float focalLenght = 0.5f;
    private int apertureIndex = 5;
    private int shutterSpeedIndex = 5;

    private HDAdditionalCameraData photoCameraAdditionalData;
    private HDAdditionalCameraData viewfinderAdditionalData;
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

        photoCameraAdditionalData = photoCamera.GetComponent<HDAdditionalCameraData>();
        viewfinderAdditionalData = viewFinderCamera.GetComponent<HDAdditionalCameraData>();

        photoCameraAdditionalData.physicalParameters.shutterSpeed = shutterSpeeds[shutterSpeedIndex];
        viewfinderAdditionalData.physicalParameters.shutterSpeed = shutterSpeeds[shutterSpeedIndex];

        photoCameraAdditionalData.physicalParameters.aperture = apertures[apertureIndex];
        viewfinderAdditionalData.physicalParameters.aperture = apertures[apertureIndex];
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

    public void IncreaseAperture ()
    {
        if (apertureIndex > 0)
        {
            apertureIndex--;
        }
        photoCameraAdditionalData.physicalParameters.aperture = apertures[apertureIndex];
        viewfinderAdditionalData.physicalParameters.aperture = apertures[apertureIndex];
    }

    public void DecreaseAperture ()
    {
        if (apertureIndex < apertures.Length - 1)
        {
            apertureIndex++;
        }
        photoCameraAdditionalData.physicalParameters.aperture = apertures[apertureIndex];
        viewfinderAdditionalData.physicalParameters.aperture = apertures[apertureIndex];
    }

    public void IncreaseShutterSpeed ()
    {
        if (shutterSpeedIndex < shutterSpeeds.Length - 1)
        {
            shutterSpeedIndex++;
        }
        photoCameraAdditionalData.physicalParameters.shutterSpeed = shutterSpeeds[shutterSpeedIndex];
        viewfinderAdditionalData.physicalParameters.shutterSpeed = shutterSpeeds[shutterSpeedIndex];
    }

    public void DecreaseShutterSpeed ()
    {
        if (shutterSpeedIndex > 0)
        {
            shutterSpeedIndex--;
        }
        photoCameraAdditionalData.physicalParameters.shutterSpeed = shutterSpeeds[shutterSpeedIndex];
        viewfinderAdditionalData.physicalParameters.shutterSpeed = shutterSpeeds[shutterSpeedIndex];
    }

    public void IncreaseZoom (float input)
    {
        focalLenght += zoomSensitivity * Time.deltaTime * input;
        focalLenght = Mathf.Clamp01(focalLenght);

        float currentFocalLenght = Mathf.Lerp(minFocalLenght, maxFocalLenght, focalLenght);

        photoCamera.focalLength = currentFocalLenght;
        viewFinderCamera.focalLength = currentFocalLenght;
    }

    public void DecreaseZoom (float input)
    {
        focalLenght -= zoomSensitivity * Time.deltaTime * input;
        focalLenght = Mathf.Clamp01(focalLenght);

        float currentFocalLenght = Mathf.Lerp(minFocalLenght, maxFocalLenght, focalLenght);

        photoCamera.focalLength = currentFocalLenght;
        viewFinderCamera.focalLength = currentFocalLenght;
    }

    public void ZoomAnalogInput (float input)
    {
        focalLenght += zoomSensitivity * Time.deltaTime * input;
        focalLenght = Mathf.Clamp01(focalLenght);

        float currentFocalLenght = Mathf.Lerp(minFocalLenght, maxFocalLenght, focalLenght);

        photoCamera.focalLength = currentFocalLenght;
        viewFinderCamera.focalLength = currentFocalLenght;
    }

}
