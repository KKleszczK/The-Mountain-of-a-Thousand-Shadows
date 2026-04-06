using UnityEngine;

public class ResolutionSetup : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution(1920, 1080, GameSettings.Fullscreen);
    }
}
