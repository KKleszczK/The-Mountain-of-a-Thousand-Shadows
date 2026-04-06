using System.Runtime.CompilerServices;
using TMPro;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Image EpicImage;
    [SerializeField] private Image BekaImage;
    [SerializeField] private RawImage Delman;
    [SerializeField] private AudioSource EpicMusic;
    [SerializeField] private AudioSource yoooo;
    [SerializeField] private AudioSource HappyMusic;
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private TextMeshProUGUI text3;
    [SerializeField] private TextMeshProUGUI text4;
    [SerializeField] private TextMeshProUGUI text5;
    [SerializeField] private Image text6;
    [SerializeField] private TextMeshProUGUI text7;
    [SerializeField] private TextMeshProUGUI text8;

    [SerializeField] private Image ButtonMultiplayer;
    [SerializeField] private Image ButtonQuit;
    [SerializeField] private Image ButtonSigleplayer;
    [SerializeField] private Image ButtonStoryline;
    [SerializeField] private TextMeshProUGUI t1;
    [SerializeField] private TextMeshProUGUI t2;
    [SerializeField] private TextMeshProUGUI t3;
    [SerializeField] private TextMeshProUGUI t4;

    [SerializeField] private Button buttonMultiplayer;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private Button buttonSingleplayer;
    [SerializeField] private Button buttonStoryline;


    private float time;
    void Start()
    {
        buttonMultiplayer.onClick.AddListener(LoadMultiplayer);
        buttonQuit.onClick.AddListener(QuitGame);
        buttonStoryline.onClick.AddListener(LoadStoryline);
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }
    private bool yooooPlayed = false;

    void Update()
    {
        time += Time.deltaTime;

        if (time > 1f && time < 3f)
        {
            float t = Mathf.PingPong(time - 1f, 2f);
            EpicImage.color = Color.Lerp(Color.black, Color.white, t);
        }
        if (time > 4f && time < 5f)
        {
            float t = Mathf.PingPong(time - 4f, 1f);
            Color c = text1.color;
            c.a = t;
            text1.color = c;
        }
        if (time > 6f && time < 7f)
        {
            float t = Mathf.PingPong(time - 6f, 1f);
            Color c = text2.color;
            c.a = t;
            text2.color = c;
        }
        if (time > 8f && time < 9f)
        {
            float t = Mathf.PingPong(time - 8f, 1f);
            Color c = text3.color;
            c.a = t;
            text3.color = c;
        }
        if (time > 10f && time < 11f)
        {
            float t = Mathf.PingPong(time - 10f, 1f);
            Color c = text4.color;
            c.a = t;
            text4.color = c;
        }
        if (time > 12f && time < 13f)
        {
            float t = Mathf.PingPong(time - 12f, 1f);
            Color c = text5.color;
            c.a = t;
            text5.color = c;
        }
        if (time > 15f && time < 16f)
        {
            float t = Mathf.PingPong(time - 15f, 1f) / 5;
            EpicMusic.volume = t;
        }

        
        if (time > 16 && !yooooPlayed)
        {
            EpicMusic.volume = 0;
            yooooPlayed = true;
            yoooo.Play();
            HappyMusic.Play();
        }






            //beka time
            if (time > 16f && time < 17f)
        {
            float t = Mathf.PingPong(time - 16f, 1f);
            Color c = text6.color;
            c.a = t;
            text6.color = c;
            Color c1 = BekaImage.color;
            c1.a = t;
            BekaImage.color = c1;
            Color c2 = Delman.color;
            c2.a = t;
            Delman.color = c2;
        }
     


        if (time > 18f && time < 19f)
        {
            float t = Mathf.PingPong(time - 18f, 1f);
            Color c = text7.color;
            c.a = t;
            text7.color = c;
        }
        if (time > 20f && time < 21f)
        {
            float t = Mathf.PingPong(time - 20f, 1f);
            Color c = text8.color;
            c.a = t;
            text8.color = c;
        }

        if (time > 22f && time < 23f)
        {
            float t = Mathf.PingPong(time - 22f, 1f);
            Color c1 = ButtonMultiplayer.color;
            c1.a = t;
            ButtonMultiplayer.color = c1;

            Color c2 = ButtonQuit.color;
            c2.a = t;
            ButtonQuit.color = c2;

            Color c3 = ButtonSigleplayer.color;
            c3.a = t;
            ButtonSigleplayer.color = c3;

            Color c4 = ButtonStoryline.color;
            c4.a = t;
            ButtonStoryline.color = c4;

            Color c5 = t1.color;
            c5.a = t;
            t1.color = c5;

            Color c6 = t2.color;
            c6.a = t;
            t2.color = c6;

            Color c7 = t3.color;
            c7.a = t;
            t3.color = c7;

            Color c8 = t4.color;
            c8.a = t;
            t4.color = c8;
        }


    }
    private void LoadMultiplayer()
    {
        SceneManager.LoadScene("Instructions");
    }
    private void LoadStoryline()
    {
        SceneManager.LoadScene("Cutscene");
    }
    private void QuitGame()
    {
        Application.Quit();
    }

}


