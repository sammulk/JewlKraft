using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    public void ToggleFullscreen(bool stateOfScreen)
    {
        Screen.fullScreen = stateOfScreen;
        Debug.Log("Fullscreen mode set to: " + stateOfScreen);
    }
}
