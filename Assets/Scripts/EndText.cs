using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndText : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private TextMeshProUGUI winText;

    [SerializeField] private Button buttonRestart;
    [SerializeField] private GameObject buttonRestartObj;
    [SerializeField] private Button buttonMenu;
    [SerializeField] private GameObject buttonMenuObj;

    [SerializeField] private AudioSource EndAudio;
    [SerializeField] private AudioSource BgAudio;

    private void Start()
    {
        buttonRestart.onClick.AddListener(LoadRestart);   
        buttonMenu.onClick.AddListener(LoadMenu);
    }

    public void StartEffect(string winnerText)
    {
        EndAudio.Play();
        BgAudio.Stop();
        backgroundPanel.SetActive(true);
        buttonMenuObj.SetActive(true);
        buttonRestartObj.SetActive(true);
        Time.timeScale = 0f;
        winText.text = winnerText;
    }

    private void LoadRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    private void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}