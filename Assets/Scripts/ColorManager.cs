using NUnit.Framework;
using Unity.VisualScripting;
//using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

//Old veersion Codee
public class ColorManager : MonoBehaviour
{
    
    [SerializeField] private Button next1;
    [SerializeField] private Button next2;
    [SerializeField] private Button prev1;
    [SerializeField] private Button prev2;
    [SerializeField] private Image sprite1;
    [SerializeField] private Image sprite11;
    [SerializeField] private Image sprite2;
    [SerializeField] private Image sprite22;

    private Color color1 = new Color(127f / 255f, 255f / 255f, 0f / 255f);//zielony
    private Color color11 = new Color(50f / 255f, 205f / 255f, 50f / 255f);//zielony
    private Color color2 = new Color(0 / 255f, 191 / 255f, 255 / 255f);//niebieski
    private Color color22 = new Color(0 / 255f, 0 / 255f, 205 / 255f);//nieebieski


    private int index1 = 0;
    private int index2 = 1;


    private Color[] listColor1 = new Color[]
    {
        new Color(127f / 255f, 255f / 255f, 0f / 255f),//zielony
        new Color(0 / 255f, 191 / 255f, 255 / 255f),//niebieski
        new Color(255 / 255f, 165 / 255f, 0 / 255f),//pomarancz
        new Color(186 / 255f, 85 / 255f, 211 / 255f),//fiolet
        new Color(255 / 255f, 255 / 255f, 0 / 255f),//zolty
        new Color(211 / 255f, 211 / 255f, 211 / 255f),//szary
        new Color(255 / 255f, 182 / 255f, 193 / 255f),//rozowy
        new Color(255 / 255f, 255 / 255f, 240 / 255f),//bialy
        new Color(160 / 255f, 82 / 255f, 45 / 255f)//brazowy
    };

    private Color[] listColor2 = new Color[]
    {
        new Color(50f / 255f, 205f / 255f, 50f / 255f),//zielony
        new Color(0 / 255f, 0 / 255f, 205 / 255f),//niebieski
        new Color(255 / 255f, 140 / 255f, 0 / 255f),//pomarancz
        new Color(138 / 255f, 43 / 255f, 226 / 255f),//fiolet
        new Color(255 / 255f, 215 / 255f, 0 / 255f),//zolty
        new Color(128 / 255f, 128 / 255f, 128 / 255f),//szary
        new Color(255 / 255f, 20 / 255f, 147 / 255f),//rozowy
        new Color(250 / 255f, 235 / 255f, 215 / 255f),//bialy
        new Color(139 / 255f, 69 / 255f, 19 / 255f)//brazowy
    };


    void Start()
    {
        UpdateColors();
        next1.onClick.AddListener(NextColor1);
        next2.onClick.AddListener(NextColor2);
        prev1.onClick.AddListener(PrevColor1);
        prev2.onClick.AddListener(PrevColor2);
    }


    private void NextColor1()
    {
        do
        {
            index1 = (index1 + 1) % 9;
        }
        while (index1 == index2);
        UpdateColors();
    }

    private void NextColor2()
    {
        do
        {
            index2 = (index2 + 1) % 9;
        }
        while (index1 == index2);
        UpdateColors();
    }

    private void PrevColor1()
    {
        do
        {
            index1 = (index1 - 1) % 9;
            if (index1 == -1)
            {
                if (index2 == 8)
                {
                    index1 = 7;
                }
                else
                {
                    index1 = 8;
                }
            }
        }
        while (index1 == index2);
        UpdateColors();
    }

    private void PrevColor2()
    {
        do
        {
            index2 = (index2 - 1) % 9;
            if (index2 == -1)
            {
                if (index1 == 8)
                {
                    index2 = 7;
                }
                else
                {
                    index2 = 8;
                }
            }
        }
        while (index1 == index2);
        UpdateColors();
    }

    private void UpdateColors()
    {
        sprite1.color = listColor1[index1];
        sprite11.color = listColor1[index1];
        sprite2.color = listColor1[index2];
        sprite22.color = listColor1[index2];

        PlayerColorSave.Player1Color1 = listColor1[index1];
        PlayerColorSave.Player1Color2 = listColor2[index1];

        PlayerColorSave.Player2Color1 = listColor1[index2];
        PlayerColorSave.Player2Color2 = listColor2[index2];

    }



}
