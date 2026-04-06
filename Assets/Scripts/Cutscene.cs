using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


//Old veersion Codee
public class CutsceneManager : MonoBehaviour
{
    public Image backgroundFade;
    public Image cutsceneImage;
    public TextMeshProUGUI cutsceneText;
    public Image topBar;
    public Image bottomBar;

    public List<Sprite> images;
    public List<string> texts;
    public List<float> slideDurations;

    private int currentSlide = 0;
    private float timer = 0f;
    private bool isTransitioning = false;

    private Vector2 topBarStartPos;
    private Vector2 topBarEndPos;
    private Vector2 bottomBarStartPos;
    private Vector2 bottomBarEndPos;

    public float barMoveDistance = 100f; 
    

    
    

    void Start()
    {

        
        topBarStartPos = topBar.rectTransform.anchoredPosition;
        bottomBarStartPos = bottomBar.rectTransform.anchoredPosition;

       
        topBarEndPos = topBarStartPos + new Vector2(barMoveDistance, 0f);
        bottomBarEndPos = bottomBarStartPos - new Vector2(barMoveDistance, 0f);

        ShowSlide(currentSlide);
        AnimateBarsIn();
        
    }

    void Update()
    {

        if (isTransitioning) return;

        timer += Time.deltaTime;

        AnimateBarsProgress();

        if (timer > slideDurations[currentSlide] || Input.GetKeyDown(KeyCode.Space))
        {
            NextSlide(); 
        }
        
    }

    void ShowSlide(int index)
    {
        cutsceneImage.sprite = images[index];
        cutsceneText.text = texts[index];
        timer = 0f;

        
        ResetBars();
    }

    void NextSlide()
    {
        currentSlide++;
        if (currentSlide >= images.Count)
        {
            EndCutscene();
            return;
        }
        
        ShowSlide(currentSlide);
    }

    void AnimateBarsIn()
    {

    }

    void AnimateBarsProgress()
    {
        float t = Mathf.Clamp01(timer / slideDurations[currentSlide]);

        topBar.rectTransform.anchoredPosition = Vector2.Lerp(topBarStartPos, topBarEndPos, t);
        bottomBar.rectTransform.anchoredPosition = Vector2.Lerp(bottomBarStartPos, bottomBarEndPos, t);
    }

    void ResetBars()
    {
        topBar.rectTransform.anchoredPosition = topBarStartPos;
        bottomBar.rectTransform.anchoredPosition = bottomBarStartPos;
    }

  
    

    void EndCutscene()
    {
        SceneManager.LoadScene("Menu"); 
    }
}
