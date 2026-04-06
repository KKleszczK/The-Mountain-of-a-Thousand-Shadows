using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections;

//using UnityEngine.UIElements;
using UnityEngine.UI;



public class MenuScene1 : MonoBehaviour
{
    [Header("Scene 1")]
    [SerializeField] private MonoBehaviour menuScript;

    [SerializeField] private SpriteRenderer SymbolBackground;
    [SerializeField] private SpriteRenderer Symbol;
    [SerializeField] private TextMeshProUGUI Press;

    private bool finish = false;

    [SerializeField] private float SymbolTimer = 2f;
    [SerializeField] private float SymbolSpeed = 0.3f;
    [SerializeField] private float PressTimer = 8f;
    [SerializeField] private float PressSpeed = 0.5f;
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private float ColorPulsing = 0f;
    [SerializeField] private float disapearSpeedPress = 1f;
    [SerializeField] private float disapearSpeedSymbol = 1f;

    private float SymbolBackgroundMax = 18f / 255f;

    private float StartPoint = 0f;
    private float SymbolProgress = 0f;
    private float PressProgress = 0f;
    private float DisapearPressProgress = 1f;
    private float DisapearSymbolProgress = 1f;
    private float DisapearSymbolBackgrundProgress = 1f;

    private bool StartSymbol = false;
    private bool StartPress = false;
    private bool PressActivator = false;
    private bool PressPresent = false;
    private bool SymbolPulsing = false;
    private bool FlyAwayBool = false;

    private Vector3 baseSymbolScale;

    [Header("Scene 2")]
    [SerializeField] private Image PlayButton;
    [SerializeField] private Image TutorialButton;
    [SerializeField] private Image SettingsButton;
    [SerializeField] private Image CreditsButton;
    [SerializeField] private Image QuitButton;
    [SerializeField] private SpriteRenderer Scene2Background;
    [SerializeField] private SpriteRenderer Line1;
    [SerializeField] private SpriteRenderer Line2;
    [SerializeField] private SpriteRenderer Line3;
    [SerializeField] private SpriteRenderer Line4;
    [SerializeField] private SpriteRenderer Line5;
    [SerializeField] private Button Button1;
    [SerializeField] private Button Button2;
    [SerializeField] private Button Button3;
    [SerializeField] private Button Button4;
    [SerializeField] private Button Button5;
    [SerializeField] private Image Button1Image;
    [SerializeField] private Image Button2Image;
    [SerializeField] private Image Button3Image;
    [SerializeField] private Image Button4Image;
    [SerializeField] private Image Button5Image;


    private bool Scene2Bool = false;

    private float Scene2BackgroundAppearSpeed = 1f;
    private float Scene2BackgroundProgress = 0f;
    private float Line1FlagTime = 3f;
    private float Line2FlagTime = 9f;
    private float Line3FlagTime = 15f;
    private float Line4FlagTime = 21f;
    private float Line5FlagTime = 27f;
    private float LinesAppearSpeed = 6f;

    private float ButtonsAppearSpeed = 1f;
    private float ButtonsAppearProgress = 0f;

    private bool buttonsBool = false;
    private bool BackgroundBool = false;

    private float Scene2Time;

    private float NewTime = 0;



    private void Start()
    {
        Screen.fullScreen = GameSettings.Fullscreen;

        Line1.material.SetFloat("_ShaderProgress", 1);
        Line2.material.SetFloat("_ShaderProgress", 1);
        Line3.material.SetFloat("_ShaderProgress", 1);
        Line4.material.SetFloat("_ShaderProgress", 1);
        Line5.material.SetFloat("_ShaderProgress", 1);
        baseSymbolScale = Symbol.transform.localScale;

        Button1.onClick.AddListener(PlayButtonFunc);
        Button2.onClick.AddListener(TutorialButtonFunc);
        Button3.onClick.AddListener(SettingsButtonFunc);
        Button4.onClick.AddListener(CreditsButtonFunc);
        Button5.onClick.AddListener(QuitButtonFunc);

    }

    private void Update()
    {
        
        if (finish) return;

        StartPoint += Time.deltaTime;



        if (!StartSymbol && StartPoint >= SymbolTimer)
        {
            SymbolFunc();
        }

        if (!StartPress && StartPoint >= PressTimer && !FlyAwayBool)
        {
            PressFunc();
        }



        if (Input.anyKeyDown && PressPresent)
        {
            Debug.Log("Wcisniêto dowolny przycisk!");
            FlyAwayBool = true;
            PressPresent = false;
        }



        //pulsing
        if (SymbolPulsing && !FlyAwayBool)
        {
            float colorSin = (Mathf.Cos(Time.time * pulseSpeed) + 1f) / 2f;
            float t = Mathf.Lerp(ColorPulsing, 1f, colorSin);
            Symbol.color = new Color(t, t, t, 1f);
            DisapearSymbolProgress = t;

        }
        //dissapear and fly away
        if (FlyAwayBool)
        {
            Button1.interactable = true;
            Button2.interactable = true;
            Button3.interactable = true;
            Button4.interactable = true;
            Button5.interactable = true;

            Color c1 = Button1Image.color;
            c1.a = PlayButton.color.a;
            Button1Image.color = c1;

            Color c2 = Button2Image.color;
            c2.a = TutorialButton.color.a;
            Button2Image.color = c2;

            Color c3 = Button3Image.color;
            c3.a = SettingsButton.color.a;
            Button3Image.color = c3;

            Color c4 = Button4Image.color;
            c4.a = CreditsButton.color.a;
            Button4Image.color = c4;

            Color c5 = Button5Image.color;
            c5.a = QuitButton.color.a;
            Button5Image.color = c5;


            DisapearPressProgress -= Time.deltaTime * disapearSpeedPress;
            DisapearSymbolProgress -= Time.deltaTime * disapearSpeedSymbol;
            DisapearSymbolBackgrundProgress -= Time.deltaTime * disapearSpeedSymbol;
            Color BackgroundTmp1 = SymbolBackground.color;
            Color SymbolTmp1 = Symbol.color;
            Color PressTmp1 = Press.color;
            BackgroundTmp1.a = DisapearSymbolProgress * SymbolBackgroundMax;
            PressTmp1.a = DisapearPressProgress;
            Symbol.color = new Color(DisapearSymbolProgress, DisapearSymbolProgress, DisapearSymbolProgress, 1f);
            Press.color = PressTmp1;
            SymbolBackground.color = BackgroundTmp1;
            if (DisapearPressProgress <= 0)
            {
                SymbolPulsing = false;
                Symbol.color = new Color(0f, 0f, 0f, 0f);
                Press.color = new Color(0f, 0f, 0f, 0f);
                ActiveScene2();
                Scene2Bool = true;
            }
        }

        //Scene2 Time set
        if (Scene2Bool)
        {
            NewTime += Time.deltaTime;
        }

        //Line1
        if (Scene2Bool == true && NewTime >= Line1FlagTime)
        {
            UpdateLine(Line1, NewTime - Line1FlagTime);
        }
        //Line2
        if (Scene2Bool == true && NewTime >= Line2FlagTime)
        {
            UpdateLine(Line2, NewTime - Line2FlagTime);
        }
        //Line3
        if (Scene2Bool == true && NewTime >= Line3FlagTime)
        {
            UpdateLine(Line3, NewTime - Line3FlagTime);
        }
        //Line4
        if (Scene2Bool == true && NewTime >= Line4FlagTime)
        {
            UpdateLine(Line4, NewTime - Line4FlagTime);
        }
        //Line5
        if (Scene2Bool == true && NewTime >= Line5FlagTime)
        {
            UpdateLine(Line5, NewTime - Line5FlagTime);
        }



        if (Scene2Bool)
        {
            if (!buttonsBool)
            {
                ButtonsAppearProgress += Time.deltaTime * ButtonsAppearSpeed;
                Color B1 = PlayButton.color;
                B1.a = ButtonsAppearProgress;
                PlayButton.color = B1;
                TutorialButton.color = B1;
                SettingsButton.color = B1;
                CreditsButton.color = B1;
                QuitButton.color = B1;
                PlayButton.GetComponentInChildren<TextMeshProUGUI>().color = B1;
                TutorialButton.GetComponentInChildren<TextMeshProUGUI>().color = B1;
                SettingsButton.GetComponentInChildren<TextMeshProUGUI>().color = B1;
                CreditsButton.GetComponentInChildren<TextMeshProUGUI>().color = B1;
                QuitButton.GetComponentInChildren<TextMeshProUGUI>().color = B1;
                if (ButtonsAppearProgress >= 1)
                {
                    buttonsBool = true;
                }
            }

            if (!BackgroundBool)
            {
                Scene2BackgroundProgress += Time.deltaTime * Scene2BackgroundAppearSpeed;
                Color Backg = Scene2Background.color;
                Backg.a = Scene2BackgroundProgress;
                Scene2Background.color = Backg;
                if (Scene2BackgroundProgress >= 1)
                {
                    BackgroundBool = true;
                }
            }

        }





    }


    private void ActiveScene2()
    {

    }
    


    private void SymbolFunc()
    {

        SymbolProgress += Time.deltaTime * SymbolSpeed;
        float Clamped = Mathf.Clamp01(SymbolProgress);

        //Background
        Color SymbolBackgroundTmp = SymbolBackground.color;
        SymbolBackgroundTmp.a = Clamped * SymbolBackgroundMax;
        SymbolBackground.color = SymbolBackgroundTmp;

        //Symbol
        Color SymbolTmp = Symbol.color;
        Symbol.color = new Color(Clamped, Clamped, Clamped, Symbol.color.a);

        if (SymbolProgress >= 1f)
        {
            SymbolPulsing = true;
            StartSymbol = true;
        }
    }

    private void PressFunc()
    {
        if (!PressActivator)
        {
            PressActivator = true;
            PressPresent = true;
        }
        PressProgress += Time.deltaTime * PressSpeed;
        float Clamped = Mathf.Clamp01(PressProgress);

        //press
        Color PressTmp = Press.color;
        PressTmp.a = Clamped;
        Press.color = PressTmp;
        if (PressProgress >= 1)
        {
            StartPress = true;
        }
    }
    //lines
    private void UpdateLine(SpriteRenderer line, float v)
    {
        float t = Mathf.Clamp01(v / LinesAppearSpeed);
        line.material.SetFloat("_ShaderProgress", Mathf.Lerp(1f, -1f, t));
    }



    //play button
    private void PlayButtonFunc()
    {
        StartCoroutine(PlayWithDelay());
        //SceneManager.LoadScene("SampleScene");
    }

    //Tutorial button
    private void TutorialButtonFunc()
    {
        SceneManager.LoadScene("Instructions", LoadSceneMode.Additive);
        MenuState.MenuPaused = true;
    }

    //Settings button
    private void SettingsButtonFunc()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
        MenuState.MenuPaused = true;
    }

    //Credits button
    private void CreditsButtonFunc()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
        MenuState.MenuPaused = true;
    }

    //Quit button
    private void QuitButtonFunc()
    {
        Application.Quit();
    }

    private IEnumerator PlayWithDelay()
    {
        yield return new WaitForSeconds(0.4f);

        SceneManager.LoadScene("SampleScene");
    }


}
