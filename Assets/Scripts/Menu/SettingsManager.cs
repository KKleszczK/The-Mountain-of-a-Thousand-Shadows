using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.UIElements;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private TextMeshProUGUI MasterVolumeSliderText;
    [SerializeField] private Toggle FullScreenToggle;
    [SerializeField] private GameObject cross;
    [SerializeField] private Button frames1;
    [SerializeField] private Button frames2;
    [SerializeField] private Button frames3;
    [SerializeField] private Button frames4;

    [SerializeField] private GameObject Line1;
    [SerializeField] private GameObject Line2;
    [SerializeField] private GameObject Line3;
    [SerializeField] private GameObject Line4;



    void Start()
    {

        MasterVolumeSlider.value = GameSettings.MasterVolume;
        MasterVolumeSlider.onValueChanged.AddListener(OnSliderValueChange);
        FullScreenToggle.onValueChanged.AddListener(UpdateCross);
        FullScreenToggle.isOn = GameSettings.Fullscreen;
        frames1.onClick.AddListener(Frames1Func);
        frames2.onClick.AddListener(Frames2Func);
        frames3.onClick.AddListener(Frames3Func);
        frames4.onClick.AddListener(Frames4Func);
    }

    private void OnSliderValueChange(float value)
    {
        GameSettings.MasterVolume = value;
        MasterVolumeSliderText.text = (value * 100f).ToString("F0") + "%";
        AudioListener.volume = value;
    }

    private void UpdateCross(bool full)
    {
        cross.SetActive(full);
        GameSettings.Fullscreen = full;
        Screen.fullScreen = full;
    }

    private void Frames1Func()
    {
        Application.targetFrameRate = 30;
        Line1.SetActive(true);
        Line2.SetActive(false);
        Line3.SetActive(false);
        Line4.SetActive(false);
    }
    private void Frames2Func()
    {
        Application.targetFrameRate = 60;
        Line1.SetActive(false);
        Line2.SetActive(true);
        Line3.SetActive(false);
        Line4.SetActive(false);
    }
    private void Frames3Func()
    {
        Application.targetFrameRate = 120;
        Line1.SetActive(false);
        Line2.SetActive(false);
        Line3.SetActive(true);
        Line4.SetActive(false);
    }
    private void Frames4Func()
    {
        Application.targetFrameRate = -1;
        Line1.SetActive(false);
        Line2.SetActive(false);
        Line3.SetActive(false);
        Line4.SetActive(true);
    }
}
