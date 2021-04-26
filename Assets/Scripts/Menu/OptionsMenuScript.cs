using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuScript : MonoBehaviour
{
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SetVolume(float volume)
    {
        //waiting for the lvl scene
    }
}
