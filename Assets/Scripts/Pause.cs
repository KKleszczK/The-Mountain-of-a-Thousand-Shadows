using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class Pause : MonoBehaviour
{

    [SerializeField] private Material Pausematerial;
    [SerializeField] private Button MenuButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button QuitButton;
    [SerializeField] private GameObject MenuButtonObj;
    [SerializeField] private GameObject RestartButtonObj;
    [SerializeField] private GameObject ContinueButtonObj;
    [SerializeField] private GameObject QuitButtonObj;

    [SerializeField] private Button EndButtonRestart;
    [SerializeField] private Button EndButtonMenu;
    [SerializeField] private Button EndButtonQuit;
    [SerializeField] private GameObject EndButtonRestartObj;
    [SerializeField] private GameObject EndButtonMenuObj;
    [SerializeField] private GameObject EndButtonQuitObj;



    [SerializeField] private AudioSource EndAudio;
    [SerializeField] private AudioSource BgAudio;

    [SerializeField] private float unloadDelay = 1f;

    [SerializeField] private float ButtonsDelay = 1f;



    [SerializeField] private Animator Player1WinsAnim;
    [SerializeField] private Animator Player2WinsAnim;
    [SerializeField] private Animator Paused;


    private bool isPaused = false;

    private bool GameEnded = false;



    void Start()
    {
        Time.timeScale = 1f;
        Pausematerial.SetFloat("_Paused", 0f);
        MenuButtonObj.SetActive(false);
        RestartButtonObj.SetActive(false);
        QuitButtonObj.SetActive(false);
        ContinueButtonObj.SetActive(false);

        MenuButton.onClick.AddListener(MenuButtonFunc);
        RestartButton.onClick.AddListener(RestartButtonFunc);
        ContinueButton.onClick.AddListener(ContinueButtonFunc);
        QuitButton.onClick.AddListener(QuitButtonFunc);

        EndButtonRestart.onClick.AddListener(RestartButtonFunc);
        EndButtonMenu.onClick.AddListener(MenuButtonFunc);
        EndButtonQuit.onClick.AddListener(QuitButtonFunc);

    }

    public void ShowWinnerScrean(string winnerText)
    {

        if (winnerText == "player 1 wins")
        {
            Player1WinsAnim.SetBool("Player1Win", true);
        }

        if (winnerText == "player 2 wins")
        {
            Player2WinsAnim.SetBool("Player2Win", true);
        }
        ShowEndButtons();
        GameEnded = true;
        EndAudio.Play();
        BgAudio.Stop();
        Time.timeScale = 0f;
        //MenuButtonObj.SetActive(true);
        //RestartButtonObj.SetActive(true);
        //QuitButtonObj.SetActive(true);
        Pausematerial.SetFloat("_Paused", 1f);
    }

    private void ShowEscScrean(bool Show)
    {
        MenuButtonObj.SetActive(Show);
        RestartButtonObj.SetActive(Show);
        QuitButtonObj.SetActive(Show);
        ContinueButtonObj.SetActive(Show);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameEnded)
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Pausematerial.SetFloat("_Paused", 1f);
            ShowEscScrean(isPaused);
            Time.timeScale = 0f;
            Paused.SetBool("Paused", true);
            //pauseMenuUI.SetActive(true);
        }
        else
        {
            ShowEscScrean(isPaused);
            Time.timeScale = 1f;
            Pausematerial.SetFloat("_Paused", 0f);
            Paused.SetBool("Paused", false);
            //pauseMenuUI.SetActive(false);
        }
    }

    private void MenuButtonFunc()
    {
        StartCoroutine(MenuWithDelay());
    }

    private void RestartButtonFunc()
    {
        StartCoroutine(RestartWithDelay());
    }

    private void ContinueButtonFunc()
    {
        TogglePause();
    }

    private void QuitButtonFunc()
    {
        StartCoroutine(QuitkWithDelay());
    }



    private IEnumerator MenuWithDelay()
    {
        yield return new WaitForSecondsRealtime(unloadDelay);
        Time.timeScale = 1f;
        Pausematerial.SetFloat("_Eff", 0f);
        Pausematerial.SetFloat("_Paused", 0f);
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator RestartWithDelay()
    {
        yield return new WaitForSecondsRealtime(unloadDelay);
        Time.timeScale = 1f;
        Pausematerial.SetFloat("_Eff", 0f);
        Pausematerial.SetFloat("_Paused", 0f);
        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator QuitkWithDelay()
    {
        yield return new WaitForSecondsRealtime(unloadDelay);
        Application.Quit();
    }

    private void ShowEndButtons()
    {
        StartCoroutine(ShowButtonsAfterDelay());
    }

    private IEnumerator ShowButtonsAfterDelay()
    {
        yield return new WaitForSecondsRealtime(ButtonsDelay);

        EndButtonRestartObj.gameObject.SetActive(true);
        EndButtonMenuObj.gameObject.SetActive(true);
        EndButtonQuitObj.gameObject.SetActive(true);
    }

}

