using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewFinderUI : MonoBehaviour
{

    public PhotoCamera photoCamera;
    public GameObject border;
    public Animator shutterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        border.SetActive(photoCamera.IsUsingPhotoCamera);
    }

    private void Update()
    {
        shutterAnimator.SetBool("ShutterOpen", photoCamera.IsShutterOpen);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        border.SetActive(photoCamera.IsUsingPhotoCamera);
    }
}
