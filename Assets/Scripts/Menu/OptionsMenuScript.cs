using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuScript : MonoBehaviour
{
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
