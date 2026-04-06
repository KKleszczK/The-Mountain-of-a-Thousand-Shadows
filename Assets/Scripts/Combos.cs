using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;
using TMPro;
using NUnit.Framework;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;
//using static UnityEditor.Experimental.GraphView.GraphView;
using System.Linq;
using JetBrains.Annotations;
using static Unity.Burst.Intrinsics.X86.Avx;
//using UnityEngine.UIElements;

public class Combos : MonoBehaviour
{
    private float Damage = 0.10f;

    [SerializeField] private AudioSource Uaaaaa;

    [SerializeField] private Animator animator1;
    [SerializeField] private Animator animator2;


    [SerializeField] private Rigidbody2D fighter1;
    [SerializeField] private Rigidbody2D fighter2;

    [SerializeField] private GameObject fighter1GameObject;
    [SerializeField] private GameObject fighter2GameObject;

    private PlayerPowerUps powerUpsPlayer1;
    private PlayerPowerUps powerUpsPlayer2;


    [SerializeField] private ScriptableStats stats1;
    [SerializeField] private ScriptableStats stats2;


    [SerializeField] private float ComboDistance;


    [Header("Camera")]
    [SerializeField] private Camera GameCamera;
    [SerializeField] private float zoomInSize = 3f;
    [SerializeField] private float zoomSpeed = 2f;


    [Header("Key Sprites")]
    [SerializeField] private Sprite KeyLeft;
    [SerializeField] private Sprite KeyUp;
    [SerializeField] private Sprite KeyDown;
    [SerializeField] private Sprite KeyRight;


    [Header("HUD")]
    [SerializeField] private Image[] NextkeyImages1;
    [SerializeField] private Image[] NextkeyImages2;
    [SerializeField] private Image[] CurrentkeyImages1;
    [SerializeField] private Image[] CurrentkeyImages2;
    [SerializeField] private RectTransform CurrentAttackFrame1;
    [SerializeField] private RectTransform CurrentAttackFrame2;
    [SerializeField] private RectTransform CurrentShareAttackFrame;
    [SerializeField] private Image[] ShareKeyImages;
    [SerializeField] private Image[] ShareKeyImagesColor1;
    [SerializeField] private Image[] ShareKeyImagesColor2;




    [SerializeField] private List<char> keyList1 = new List<char> {  };
    [SerializeField] private List<char> keyList2 = new List<char> {  };
    [SerializeField] private Canvas[] canvases;

    [SerializeField] private TextMeshProUGUI ComboCounterPlayer1;
    [SerializeField] private TextMeshProUGUI ComboCounterPlayer2;

    public PopupEffects popupEffect1;
    public PopupEffects popupEffect2;

    public TestHealth player1Health;
    public TestHealth player2Health;

    private float defaultSize;
    private float defaultPositionY;
    private float defaultPositionX;
    private bool inCombo = false;

    private float fighter1OriginalRotation;
    private float fighter2OriginalRotation;

    [SerializeField] private SpriteRenderer fighter1SpriteRenderer;
    [SerializeField] private SpriteRenderer fighter2SpriteRenderer;

    [SerializeField] private CameraMovement cameraMovement;


    [Header("Slice Images")]
    [SerializeField] private Image[] SlicePlayer1True; 
    [SerializeField] private Image[] SlicePlayer1False; 
    [SerializeField] private Image[] SlicePlayer2True;
    [SerializeField] private Image[] SlicePlayer2False;
    [SerializeField] private Image[] SlicePlayer1SharedTrue;
    [SerializeField] private Image[] SlicePlayer1SharedFalse;
    [SerializeField] private Image[] SlicePlayer2SharedTrue;
    [SerializeField] private Image[] SlicePlayer2SharedFalse;

    private Animator[] animSlicePlayer1True;
    private Animator[] animSlicePlayer1False;
    private Animator[] animSlicePlayer2True;
    private Animator[] animSlicePlayer2False;
    private Animator[] animSlicePlayer1SharedTrue;
    private Animator[] animSlicePlayer1SharedFalse;
    private Animator[] animSlicePlayer2SharedTrue;
    private Animator[] animSlicePlayer2SharedFalse;



    private void Awake()
    {
        animSlicePlayer1True = new Animator[SlicePlayer1True.Length];
        animSlicePlayer1False = new Animator[SlicePlayer1False.Length];
        animSlicePlayer2True = new Animator[SlicePlayer2True.Length];
        animSlicePlayer2False = new Animator[SlicePlayer2False.Length];
        animSlicePlayer1SharedTrue = new Animator[SlicePlayer1SharedTrue.Length];
        animSlicePlayer1SharedFalse = new Animator[SlicePlayer1SharedFalse.Length];
        animSlicePlayer2SharedTrue = new Animator[SlicePlayer2SharedTrue.Length];
        animSlicePlayer2SharedFalse = new Animator[SlicePlayer2SharedFalse.Length];

        for (int i = 0; i < SlicePlayer1True.Length; i++)
            animSlicePlayer1True[i] = SlicePlayer1True[i].GetComponent<Animator>();

        for (int i = 0; i < SlicePlayer1False.Length; i++)
            animSlicePlayer1False[i] = SlicePlayer1False[i].GetComponent<Animator>();

        for (int i = 0; i < SlicePlayer2True.Length; i++)
            animSlicePlayer2True[i] = SlicePlayer2True[i].GetComponent<Animator>();

        for (int i = 0; i < SlicePlayer2False.Length; i++)
            animSlicePlayer2False[i] = SlicePlayer2False[i].GetComponent<Animator>();

        for (int i = 0; i < SlicePlayer1SharedTrue.Length; i++)
            animSlicePlayer1SharedTrue[i] = SlicePlayer1SharedTrue[i].GetComponent<Animator>();

        for (int i = 0; i < SlicePlayer1SharedFalse.Length; i++)
            animSlicePlayer1SharedFalse[i] = SlicePlayer1SharedFalse[i].GetComponent<Animator>();

        for (int i = 0; i < SlicePlayer2SharedTrue.Length; i++)
            animSlicePlayer2SharedTrue[i] = SlicePlayer2SharedTrue[i].GetComponent<Animator>();

        for (int i = 0; i < SlicePlayer2SharedFalse.Length; i++)
            animSlicePlayer2SharedFalse[i] = SlicePlayer2SharedFalse[i].GetComponent<Animator>();

        //colors
        foreach (var img in SlicePlayer1True)
            img.color = PlayerColorSave.Player1Color1;

        foreach (var img in SlicePlayer2True)
            img.color = PlayerColorSave.Player2Color1;

        foreach (var img in SlicePlayer1False)
            img.color = Color.red;

        foreach (var img in SlicePlayer2False)
            img.color = Color.red;

        foreach (var img in SlicePlayer1SharedFalse)
            // img.color = PlayerColorSave.Player1Color2;
            img.color = Color.red;

        foreach (var img in SlicePlayer2SharedFalse)
            //img.color = PlayerColorSave.Player2Color2;
            img.color = Color.red;

        foreach (var img in SlicePlayer1SharedTrue)
            img.color = PlayerColorSave.Player1Color1;
        
        foreach (var img in SlicePlayer2SharedTrue)
            img.color = PlayerColorSave.Player2Color1;

    }


    private void Start()
    {








        powerUpsPlayer1 = fighter1GameObject.GetComponent<PlayerPowerUps>();
        powerUpsPlayer2 = fighter2GameObject.GetComponent<PlayerPowerUps>();

        fighter1OriginalRotation = fighter1.rotation;
        fighter2OriginalRotation = fighter2.rotation;

        defaultSize = GameCamera.orthographicSize;
        

        keyList1.Add(stats1.JumpKey.ToString()[0]);
        keyList1.Add(stats1.MoveLeftKey.ToString()[0]);
        keyList1.Add(stats1.MoveRightKey.ToString()[0]);
        keyList1.Add(stats1.CrouchKey.ToString()[0]);

        keyList2.Add(stats2.JumpKey.ToString()[0]);
        keyList2.Add(stats2.MoveLeftKey.ToString()[0]);
        keyList2.Add(stats2.MoveRightKey.ToString()[0]);
        keyList2.Add(stats2.CrouchKey.ToString()[0]);

        AddKeyToList(NextAttack1, 4, keyList1);
        AddKeyToList(NextAttack2, 4, keyList2);
        Createshare();
        UpdateNextAttackImages();

        //
    }


    void Update()
    {
        Vector3 pos1 = fighter1.position;
        Vector3 pos2 = fighter2.position;
        float distance = Vector3.Distance(pos1, pos2);

        if (distance <= ComboDistance && inCombo == false)
        {
            inCombo = true;
            StartCombo();
            StartCoroutine(Combo());

        }
        if (!comboEnded)
        {
            GatherInput();
        }
        if (inCombo)
        {
            fighter1SpriteRenderer.flipX = fighter1.position.x > fighter2.position.x;
            fighter2SpriteRenderer.flipX = fighter2.position.x > fighter1.position.x;

            Vector2 dir1 = (fighter2.position - fighter1.position).normalized;
            float angle1 = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;
            

            Vector2 dir2 = (fighter1.position - fighter2.position).normalized;
            float angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
            
            if (fighter1.position.x > fighter2.position.x) 
            {
                fighter1.MoveRotation(angle1 + 180f);
                fighter2.MoveRotation(angle2);
            }
            else
            {
                fighter1.MoveRotation(angle1);
                fighter2.MoveRotation(angle2 + 180f);
            }
        }
        else
        {
            fighter1.MoveRotation(fighter1OriginalRotation);
            fighter2.MoveRotation(fighter2OriginalRotation);
        }



    }

    //fixed
    private float time = 0;
    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
    }


    private void StartCombo()
    {
        // Fighter 1
        fighter1.linearVelocity = Vector2.zero;
        fighter1.gravityScale = 0;
        fighter1.constraints = RigidbodyConstraints2D.FreezeAll;
        fighter1.GetComponent<PlatformerMovement>().enabled = false;
        animator1.SetBool("isFighting", true);
        animator1.SetTrigger("Attack");


        // Fighter 2
        fighter2.linearVelocity = Vector2.zero;
        fighter2.gravityScale = 0;
        fighter2.constraints = RigidbodyConstraints2D.FreezeAll;
        fighter2.GetComponent<PlatformerMovement>().enabled = false;
        animator2.SetBool("isFighting", true);
        animator2.SetTrigger("Attack");

        Uaaaaa.Play();
    }


    private IEnumerator Combo()
    {
        playerAttackIndex1 = 0;
        playerAttackIndex2 = 0;
        UpdateCollors();

        Coroutine ZoomIn = StartCoroutine(CameraZoomIn());
        Coroutine Sprites1 = StartCoroutine(MoveSprites1());
        Coroutine Sprites2 = StartCoroutine(MoveSprites2());

        yield return ZoomIn;
        yield return Sprites1;
        yield return Sprites2;
        //yield return StartCoroutine(CameraZoomIn());

        yield return StartCoroutine(FightQTE());

        yield return StartCoroutine(CameraZoomOut());

        EndCombo();
    }


    private void EndCombo()
    {
        // Fighter 1
        fighter1.gravityScale = 1;
        fighter1.constraints = RigidbodyConstraints2D.FreezeRotation;
        fighter1.GetComponent<PlatformerMovement>().enabled = true;
        fighter1.GetComponent<PlatformerMovement>().HandleKnockback();
        animator1.SetBool("isFighting", false);

        // Fighter 2
        fighter2.gravityScale = 1;
        fighter2.constraints = RigidbodyConstraints2D.FreezeRotation;
        fighter2.GetComponent<PlatformerMovement>().enabled = true;
        fighter2.GetComponent<PlatformerMovement>().HandleKnockback();

        StartCoroutine(EndComboDelay());
        animator2.SetBool("isFighting", false);

        fighter1.GetComponent<PlayerPowerUps>().ResetPowerUps();
        fighter2.GetComponent<PlayerPowerUps>().ResetPowerUps();

        DamageBuffUsed1 = false;
        DamageBuffUsed2 = false;
        DeffenceBuffUsed1 = false;
        DeffenceBuffUsed2 = false;

}

    //camera zoom in, current attack panels go in from sides
    private IEnumerator CameraZoomIn()
    {
        cameraMovement.overrideFollow = true;
        defaultPositionY = GameCamera.transform.position.y;
        defaultPositionX = GameCamera.transform.position.x;
        Vector2 midPoint = (fighter1.position + fighter2.position) / 2f;
        Vector3 targetCamPos = new Vector3(midPoint.x, midPoint.y, GameCamera.transform.position.z);

        Vector3 CurrentAttackFramePos1 = CurrentAttackFrame1.transform.localPosition;
        Vector3 CurrentAttackFramePos2 = CurrentAttackFrame2.transform.localPosition;
        Vector3 CurrentShareAttackFramePos = CurrentShareAttackFrame.transform.localPosition;

        Vector3 CurrentAttackFrameEndPos1 = new Vector3(-700, 0, 0);
        Vector3 CurrentAttackFrameEndPos2 = new Vector3(700, 0, 0);
        Vector3 CurrentShareAttackFrameEndPos = new Vector3(0, -350, 0);

        float progress = 0f;
        while (Vector3.Distance(GameCamera.transform.position, targetCamPos) > 0.01f)
        {
            progress += Time.unscaledDeltaTime * zoomSpeed;
            GameCamera.transform.position = Vector3.Lerp(GameCamera.transform.position, targetCamPos, progress);
            GameCamera.orthographicSize = Mathf.Lerp(GameCamera.orthographicSize, zoomInSize, progress);

            CurrentAttackFrame1.transform.localPosition = Vector3.Lerp(CurrentAttackFramePos1, CurrentAttackFrameEndPos1, progress * 6);
            CurrentAttackFrame2.transform.localPosition = Vector3.Lerp(CurrentAttackFramePos2, CurrentAttackFrameEndPos2, progress * 6);
            CurrentShareAttackFrame.transform.localPosition = Vector3.Lerp(CurrentShareAttackFramePos, CurrentShareAttackFrameEndPos, progress * 6);

            yield return null;
        }
        //end
        CurrentAttackFrame1.transform.localPosition = CurrentAttackFrameEndPos1;
        CurrentAttackFrame2.transform.localPosition = CurrentAttackFrameEndPos2;
        CurrentShareAttackFrame.transform.localPosition = CurrentShareAttackFrameEndPos;
        GameCamera.transform.position = targetCamPos;
        GameCamera.orthographicSize = zoomInSize;


    }


    //zoom out camera, slide out side current attacks frames
    private IEnumerator CameraZoomOut()
    {
        
        Vector3 CurrentAttackFramePos1 = CurrentAttackFrame1.transform.localPosition;
        Vector3 CurrentAttackFramePos2 = CurrentAttackFrame2.transform.localPosition;
        Vector3 CurrentShareAttackFramePos = CurrentShareAttackFrame.transform.localPosition;


        Vector3 CurrentAttackFrameEndPos1 = new Vector3(-1100, 0, 0);
        Vector3 CurrentAttackFrameEndPos2 = new Vector3(1100, 0, 0);
        Vector3 CurrentShareAttackFrameEndPos = new Vector3(0, -750, 0);

        float progress = 0f;

        while (Mathf.Abs(GameCamera.transform.position.y - defaultPositionY) > 0.01f
                && Mathf.Abs(GameCamera.transform.position.x - defaultPositionX) > 0.01f
                && Mathf.Abs(GameCamera.orthographicSize - defaultSize) > 0.01f)
        {
            progress += Time.unscaledDeltaTime * zoomSpeed;
            Vector3 camPos = GameCamera.transform.position;

            camPos.x = Mathf.Lerp(camPos.x, defaultPositionX, progress);
            camPos.y = Mathf.Lerp(camPos.y, defaultPositionY, progress);

            GameCamera.transform.position = camPos;
            GameCamera.orthographicSize = Mathf.Lerp(GameCamera.orthographicSize, defaultSize, progress);

            CurrentAttackFrame1.transform.localPosition = Vector3.Lerp(CurrentAttackFramePos1, CurrentAttackFrameEndPos1, progress * 6);
            CurrentAttackFrame2.transform.localPosition = Vector3.Lerp(CurrentAttackFramePos2, CurrentAttackFrameEndPos2, progress * 6);
            CurrentShareAttackFrame.transform.localPosition = Vector3.Lerp(CurrentShareAttackFramePos, CurrentShareAttackFrameEndPos, progress * 6);

            yield return null;
        }
        //end
        CurrentAttackFrame1.transform.localPosition = CurrentAttackFrameEndPos1;
        CurrentAttackFrame2.transform.localPosition = CurrentAttackFrameEndPos2;
        CurrentShareAttackFrame.transform.localPosition = CurrentShareAttackFrameEndPos;
        Vector3 CamPosYEnd = GameCamera.transform.position;
        CamPosYEnd.y = defaultPositionY;
        CamPosYEnd.x = defaultPositionX;
        GameCamera.transform.position = CamPosYEnd;
        GameCamera.orthographicSize = defaultSize;
        cameraMovement.overrideFollow = false;


    }
    
    private IEnumerator EndComboDelay()
    {
        yield return new WaitForSeconds(0.1f);
        inCombo = false;
    }

    private IEnumerator MoveSprites1()
    {
        float duration = 0.3f;
        float time = 0f;
        Next(NextAttack1, 1);

        float scaleStart = 2f;
        float scaleEnd = 1f;

        int value;

        if (!comboEnded)
        {
            value = 0;
        }
        else { value = 400; }

        Vector3 EndPos1 = CurrentkeyImages1[0].transform.position + new Vector3(value, 0, 0);
        Vector3 EndPos2 = CurrentkeyImages1[1].transform.position + new Vector3(value, 0, 0);
        Vector3 EndPos3 = CurrentkeyImages1[2].transform.position + new Vector3(value, 0, 0);
        Vector3 EndPos4 = CurrentkeyImages1[3].transform.position + new Vector3(value, 0, 0);

        Vector3 EndPosPos1 = CurrentkeyImages1[0].transform.localPosition;
        Vector3 EndPosPos2 = CurrentkeyImages1[1].transform.localPosition;
        Vector3 EndPosPos3 = CurrentkeyImages1[2].transform.localPosition;
        Vector3 EndPosPos4 = CurrentkeyImages1[3].transform.localPosition;

        for (int i = 0; i < 4; i++)
        {
            CurrentkeyImages1[i].gameObject.SetActive(true);
            CurrentkeyImages1[i].transform.position = NextkeyImages1[i].transform.position;
        }
        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / duration);
            float currentScale = Mathf.Lerp(scaleStart, scaleEnd, t);
            //position
            CurrentkeyImages1[0].transform.position = Vector3.Lerp(CurrentkeyImages1[0].transform.position, EndPos1, t);
            CurrentkeyImages1[1].transform.position = Vector3.Lerp(CurrentkeyImages1[1].transform.position, EndPos2, t);
            CurrentkeyImages1[2].transform.position = Vector3.Lerp(CurrentkeyImages1[2].transform.position, EndPos3, t);
            CurrentkeyImages1[3].transform.position = Vector3.Lerp(CurrentkeyImages1[3].transform.position, EndPos4, t);
            //scale
            CurrentkeyImages1[0].transform.localScale = Vector3.one * currentScale;
            CurrentkeyImages1[1].transform.localScale = Vector3.one * currentScale;
            CurrentkeyImages1[2].transform.localScale = Vector3.one * currentScale;
            CurrentkeyImages1[3].transform.localScale = Vector3.one * currentScale;


            yield return null;
        }
        CurrentkeyImages1[0].transform.localPosition = EndPosPos1;
        CurrentkeyImages1[1].transform.localPosition = EndPosPos2;
        CurrentkeyImages1[2].transform.localPosition = EndPosPos3;
        CurrentkeyImages1[3].transform.localPosition = EndPosPos4;
        // animowanie

        // 

    }

    private IEnumerator MoveSprites2()
    {
        float duration = 0.3f;
        float time = 0f;
        Next(NextAttack2, 2);

        float scaleStart = 2f;
        float scaleEnd = 1f;

        int value;

        if (!comboEnded)
        {
            value = 0;
        }
        else { value = -400; }

        Vector3 EndPos1 = CurrentkeyImages2[0].transform.position + new Vector3(value, 0, 0);
        Vector3 EndPos2 = CurrentkeyImages2[1].transform.position + new Vector3(value, 0, 0);
        Vector3 EndPos3 = CurrentkeyImages2[2].transform.position + new Vector3(value, 0, 0);
        Vector3 EndPos4 = CurrentkeyImages2[3].transform.position + new Vector3(value, 0, 0);

        Vector3 EndPosPos1 = CurrentkeyImages2[0].transform.localPosition;
        Vector3 EndPosPos2 = CurrentkeyImages2[1].transform.localPosition;
        Vector3 EndPosPos3 = CurrentkeyImages2[2].transform.localPosition;
        Vector3 EndPosPos4 = CurrentkeyImages2[3].transform.localPosition;

        for (int i = 0; i < 4; i++)
        {
            CurrentkeyImages2[i].gameObject.SetActive(true);
            CurrentkeyImages2[i].transform.position = NextkeyImages2[i].transform.position;
        }
        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / duration);
            float currentScale = Mathf.Lerp(scaleStart, scaleEnd, t);
            //position
            CurrentkeyImages2[0].transform.position = Vector3.Lerp(CurrentkeyImages2[0].transform.position, EndPos1, t);
            CurrentkeyImages2[1].transform.position = Vector3.Lerp(CurrentkeyImages2[1].transform.position, EndPos2, t);
            CurrentkeyImages2[2].transform.position = Vector3.Lerp(CurrentkeyImages2[2].transform.position, EndPos3, t);
            CurrentkeyImages2[3].transform.position = Vector3.Lerp(CurrentkeyImages2[3].transform.position, EndPos4, t);
            //scale
            CurrentkeyImages2[0].transform.localScale = Vector3.one * currentScale;
            CurrentkeyImages2[1].transform.localScale = Vector3.one * currentScale;
            CurrentkeyImages2[2].transform.localScale = Vector3.one * currentScale;
            CurrentkeyImages2[3].transform.localScale = Vector3.one * currentScale;


            yield return null;
        }
        CurrentkeyImages2[0].transform.localPosition = EndPosPos1;
        CurrentkeyImages2[1].transform.localPosition = EndPosPos2;
        CurrentkeyImages2[2].transform.localPosition = EndPosPos3;
        CurrentkeyImages2[3].transform.localPosition = EndPosPos4;
        // animowanie

        // 

    }

    private void Createshare()
    {
        char tmp1;
        char tmp2;
        NextShare1.Clear();
        NextShare2.Clear();
        do
        {
            int randomIndex = UnityEngine.Random.Range(0, 4);
            Debug.Log("Losowanie: " + randomIndex);
            tmp1 = keyList1[randomIndex];
            tmp2 = keyList2[randomIndex];
        }
        while (tmp1 == NextAttack1[0] || tmp2 == NextAttack2[0]);

        Debug.Log("Finalnie: " + tmp1 + " , " + tmp2);

        NextShare1.Add(tmp1);
        NextShare2.Add(tmp2);
        int counterTimes = 0;
        while (counterTimes < 5)
        {
            char tmp3;
            char tmp4;
            do
            {
                int randomIndex = UnityEngine.Random.Range(0, 4);
                tmp3 = keyList1[randomIndex];
                tmp4 = keyList2[randomIndex];
            }
            while (NextShare1[NextShare1.Count - 1] == tmp3 || NextShare2[NextShare2.Count - 1] == tmp4);
            NextShare1.Add(tmp3);
            NextShare2.Add(tmp4);
            counterTimes++;
        }
        for (int i = 0; i < NextShare1.Count; i++)
        {
            switch (NextShare1[i])
            {
                case 'A':
                    ShareKeyImages[i].sprite = KeyLeft;
                    break;
                case 'W':
                    ShareKeyImages[i].sprite = KeyUp;
                    break;
                case 'S':
                    ShareKeyImages[i].sprite = KeyDown;
                    break;
                case 'D':
                    ShareKeyImages[i].sprite = KeyRight;
                    break;
            }
        }
        //debug
        /*
        string output1 = "NextShare1: ";
        foreach (char c in NextShare1)
        {
            output1 += c + " ";
        }
        Debug.Log(output1);
        

        // 
        string output2 = "NextShare2: ";
        foreach (char c in NextShare2)
        {
            output2 += c + " ";
        }
        Debug.Log(output2);
        */
    }

    // adds keys to players attack lists without repeats
    private void AddKeyToList(List<char> listToAdd, int times, List<char> PlayerControllsList)
    {
        int counterTimes = 0;
        while (counterTimes < times)
        {
            char tmp;
            do
            {
                tmp = PlayerControllsList[UnityEngine.Random.Range(0, PlayerControllsList.Count)];
            }
            while (listToAdd.Count > 0 && tmp == listToAdd[listToAdd.Count - 1]);
            listToAdd.Add(tmp);
            counterTimes++;
        }
    }

    //inputs QTE
    private char? currentKeyPressed1 = null;
    private char? currentKeyPressed2 = null;


    private void GatherInput()
    {
        if (currentKeyPressed1 == null)
        {
            KeyCode[] keys1 = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
            foreach (KeyCode key in keys1)
            {
                if (Input.GetKeyDown(key))
                {
                    currentKeyPressed1 = key.ToString()[0];
                    break;
                }
            }
        }

        if (currentKeyPressed2 == null)
        {
            KeyCode[] keys2 = { KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L };
            foreach (KeyCode key in keys2)
            {
                if (Input.GetKeyDown(key))
                {
                    currentKeyPressed2 = key.ToString()[0];
                    break;
                }
            }
        }
    }

    //change Time Bar size
    private void UpdateTimeBar(float time)
    {
        //Debug.Log(time);
        float maxWidth = 1700f;
        float minWidth = 10f;
        float maxTime = BaseComboTime;
        
        time = Mathf.Clamp(time, 0f, maxTime);

        float t = time / maxTime;
        float newWidth = Mathf.Lerp(minWidth, maxWidth, t);
        Vector2 size = timeBar.sizeDelta;
        size.x = newWidth;
        timeBar.sizeDelta = size;
    }

    //Next set of attacks
    private void Next(List<char> list, int index)
    {
        if (index == 1)
        {
            CurrentAttack1 = new List<char>(list);
            list.Clear();
            AddKeyToList(NextAttack1, 4, keyList1);
            CurrentkeyImages1[0].sprite = NextkeyImages1[0].sprite;
            CurrentkeyImages1[1].sprite = NextkeyImages1[1].sprite;
            CurrentkeyImages1[2].sprite = NextkeyImages1[2].sprite;
            CurrentkeyImages1[3].sprite = NextkeyImages1[3].sprite;
        }
        else if (index == 2)
        {
            CurrentAttack2 = new List<char>(list);
            list.Clear();
            AddKeyToList(NextAttack2, 4, keyList2);
            CurrentkeyImages2[0].sprite = NextkeyImages2[0].sprite;
            CurrentkeyImages2[1].sprite = NextkeyImages2[1].sprite;
            CurrentkeyImages2[2].sprite = NextkeyImages2[2].sprite;
            CurrentkeyImages2[3].sprite = NextkeyImages2[3].sprite;
        }
        UpdateNextAttackImages();
    }

    //Update nextattack
    private void UpdateNextAttackImages()
    {
        for (int i = 0; i < 4; i++)
        {
            switch (NextAttack1[i])
            {
                case 'A':
                    NextkeyImages1[i].sprite = KeyLeft;
                    break;
                case 'W':
                    NextkeyImages1[i].sprite = KeyUp;
                    break;
                case 'S':
                    NextkeyImages1[i].sprite = KeyDown;
                    break;
                case 'D':
                    NextkeyImages1[i].sprite = KeyRight;
                    break;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            switch (NextAttack2[i])
            {
                case 'J':
                    NextkeyImages2[i].sprite = KeyLeft;
                    break;
                case 'I':
                    NextkeyImages2[i].sprite = KeyUp;
                    break;
                case 'K':
                    NextkeyImages2[i].sprite = KeyDown;
                    break;
                case 'L':
                    NextkeyImages2[i].sprite = KeyRight;
                    break;
            }
        }
    }

    private void UpdateCollors()
    {
        if (IsDoingShareAttack1 == true)
        {
            foreach (var img in CurrentkeyImages1)
            {
                img.color = Color.white;
            }
            for (int i = 0; i < 6; i++)
            {
                if (i < playerAttackIndex1)
                {
                    //ShareKeyImagesColor1[i].color = Color.green;
                    animSlicePlayer1SharedTrue[i].Play("Start");
                }
                else
                {
                    //animSlicePlayer1SharedTrue[i].Play("Back");
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (i < playerAttackIndex1)
                {
                    //CurrentkeyImages1[i].color = Color.green; // poprawne
                    animSlicePlayer1True[i].Play("Start");
                }
                else
                {
                    //CurrentkeyImages1[i].color = Color.white; // reset nieklikniêtych
                    animSlicePlayer1True[i].Play("Back");
                }
            }
        }
        
            
        if (IsDoingShareAttack2 == true)
        {
            foreach (var img in CurrentkeyImages2)
            {
                img.color = Color.white;
            }
            for (int i = 0; i < 6; i++)
            {
                if (i < playerAttackIndex2)
                {
                    //ShareKeyImagesColor2[i].color = Color.green;
                    animSlicePlayer2SharedTrue[i].Play("Start");
                }
                else
                {
                    //ShareKeyImagesColor2[i].color = Color.white;
                    //animSlicePlayer2SharedTrue[i].Play("Back");
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (i < playerAttackIndex2)
                {
                    //CurrentkeyImages2[i].color = Color.green;
                    animSlicePlayer2True[i].Play("Start");
                }
                else
                {
                    //CurrentkeyImages2[i].color = Color.white;
                    animSlicePlayer2True[i].Play("Back");
                }
            }
        }
    }

    private void BadCombination(int i)
    {
        if (i == 1) {
            playerAttackIndex1 = 0;
            UpdateCollors();
            if (IsDoingShareAttack1)
            {
                foreach (var anim in animSlicePlayer1SharedFalse)
                {
                    anim.Play("Start");
                }    
            }
            else
            {
                foreach (var anim in animSlicePlayer1False)
                {
                    anim.Play("Start");
                }
            }
                
        }
        if (i == 2)
        {
            playerAttackIndex2 = 0;
            UpdateCollors();
            if (IsDoingShareAttack2)
            {
                foreach (var anim in animSlicePlayer2SharedFalse)
                {
                    anim.Play("Start");
                }
            }
            else
            {
                foreach (var anim in animSlicePlayer2False)
                {
                    anim.Play("Start");
                }
            }
                
        }
    }




    private int playerAttackIndex1 = 0;
    private int playerAttackIndex2 = 0;

    private bool DamageBuffUsed1 = false;
    private bool DamageBuffUsed2 = false;

    private bool DeffenceBuffUsed1 = false;
    private bool DeffenceBuffUsed2 = false;

    private bool IsDoingShareAttack1 = true;
    private bool IsDoingShareAttack2 = true;
    private bool SharedAttackSuccess1 = false;
    private bool SharedAttackSuccess2 = false;

    private bool StopButtonsBool = false;

    public void StopButtons()
    {
        StopButtonsBool = true;
    }
    private void checkPlayersAttacks()
    {
        if (!StopButtonsBool)
        {
            if (currentKeyPressed1 != null)
            {
                if (currentKeyPressed1 == NextShare1[playerAttackIndex1] && IsDoingShareAttack1 && !SharedAttackSuccess1 && !SharedAttackSuccess2)
                {
                    playerAttackIndex1 += 1;
                    if (playerAttackIndex1 >= 6)
                    {
                        SharedAttackSuccess1 = true;
                        float currentDamage = Damage * 1.75f;
                        //damage buff
                        if (powerUpsPlayer1.activePowerUps.Contains(0) && !DamageBuffUsed1)
                        {
                            DamageBuffUsed1 = true;
                            currentDamage += 0.175f;
                        }
                        if (!powerUpsPlayer1.activePowerUps.Contains(7))
                        {
                            if (powerUpsPlayer2.activePowerUps.Contains(2) && !DeffenceBuffUsed2)
                            {
                                DeffenceBuffUsed2 = true;
                                currentDamage = 0;
                            }
                            else if (powerUpsPlayer2.activePowerUps.Contains(1))
                            {
                                currentDamage = currentDamage / 2f;
                            }
                        }
                        //int attackIndex = UnityEngine.Random.Range(1, 5);
                        //animator1.SetInteger("AttackIndex", attackIndex);
                        animator1.SetTrigger("AttackPeerform");
                        player2Health.TakeDamage(currentDamage, "player 1 wins");
                        playerAttackIndex1 = 0;
                        animSlicePlayer1SharedTrue[5].Play("Start");
                        //StartCoroutine(MoveSprites1()); // coroutine for next
                        if (!powerUpsPlayer1.activePowerUps.Contains(8))
                        {
                            BaseComboTime += attackPlusTime;
                        }
                    }
                    currentKeyPressed1 = null;
                    UpdateCollors();
                }
                else if (IsDoingShareAttack1)
                {
                    BadCombination(1);
                    IsDoingShareAttack1 = false;
                    playerAttackIndex1 = 0;
                }




                else if (currentKeyPressed1 == CurrentAttack1[playerAttackIndex1])
                {
                    //IsDoingShareAttack1 = false;
                    playerAttackIndex1 += 1;
                    if (playerAttackIndex1 >= 4)
                    {
                        float currentDamage = Damage;
                        //damage buff
                        if (powerUpsPlayer1.activePowerUps.Contains(0) && !DamageBuffUsed1)
                        {
                            DamageBuffUsed1 = true;
                            currentDamage += 0.1f;
                        }
                        if (powerUpsPlayer1.activePowerUps.Contains(8))
                        {
                            currentDamage = currentDamage / 2f;
                        }
                        //deffences
                        if (!powerUpsPlayer1.activePowerUps.Contains(7))
                        {
                            if (powerUpsPlayer2.activePowerUps.Contains(2) && !DeffenceBuffUsed2)
                            {
                                DeffenceBuffUsed2 = true;
                                currentDamage = 0;
                            }
                            else if (powerUpsPlayer2.activePowerUps.Contains(1))
                            {
                                currentDamage = currentDamage / 2f;
                            }
                        }

                        //int attackIndex = UnityEngine.Random.Range(1, 5);
                        //animator1.SetInteger("AttackIndex", attackIndex);
                        animator1.SetTrigger("AttackPeerform");
                        player2Health.TakeDamage(currentDamage, "player 1 wins");
                        playerAttackIndex1 = 0;
                        StartCoroutine(MoveSprites1()); // coroutine for next
                        if (!powerUpsPlayer1.activePowerUps.Contains(8))
                        {
                            BaseComboTime += attackPlusTime;
                        }
                    }
                    currentKeyPressed1 = null;
                    UpdateCollors();
                }
                else
                {
                    //IsDoingShareAttack1 = false;
                    if (powerUpsPlayer1.activePowerUps.Contains(8))
                    {
                        currentKeyPressed1 = null;
                    }
                    else
                    {
                        BadCombination(1);
                        playerAttackIndex1 = 0;
                        currentKeyPressed1 = null;
                    }

                }
            }


            if (currentKeyPressed2 != null)
            {
                if (currentKeyPressed2 == NextShare2[playerAttackIndex2] && IsDoingShareAttack2 && !SharedAttackSuccess1 && !SharedAttackSuccess2)
                {
                    playerAttackIndex2 += 1;
                    if (playerAttackIndex2 >= 6)
                    {
                        SharedAttackSuccess2 = true;
                        float currentDamage = Damage * 1.75f;
                        //damage buff
                        if (powerUpsPlayer2.activePowerUps.Contains(0) && !DamageBuffUsed2)
                        {
                            DamageBuffUsed2 = true;
                            currentDamage += 0.175f;
                        }
                        if (!powerUpsPlayer2.activePowerUps.Contains(7))
                        {
                            if (powerUpsPlayer1.activePowerUps.Contains(2) && !DeffenceBuffUsed1)
                            {
                                DeffenceBuffUsed1 = true;
                                currentDamage = 0;
                            }
                            else if (powerUpsPlayer1.activePowerUps.Contains(1))
                            {
                                currentDamage = currentDamage / 2f;
                            }
                        }
                        //int attackIndex = UnityEngine.Random.Range(1, 5);
                        //animator2.SetInteger("AttackIndex", attackIndex);
                        animator2.SetTrigger("AttackPeerform");
                        player1Health.TakeDamage(currentDamage, "player 2 wins");
                        playerAttackIndex2 = 0;
                        animSlicePlayer2SharedTrue[5].Play("Start");
                        //StartCoroutine(MoveSprites2()); // coroutine for next 
                        if (!powerUpsPlayer2.activePowerUps.Contains(8))
                        {
                            BaseComboTime += attackPlusTime;
                        }
                    }
                    currentKeyPressed2 = null;
                    UpdateCollors();
                }
                else if (IsDoingShareAttack2)
                {
                    BadCombination(2);
                    IsDoingShareAttack2 = false;
                    playerAttackIndex2 = 0;
                }


                else if (currentKeyPressed2 == CurrentAttack2[playerAttackIndex2])
                {
                    //IsDoingShareAttack2 = false;
                    playerAttackIndex2 += 1;
                    if (playerAttackIndex2 >= 4)
                    {
                        float currentDamage = Damage;
                        //damage buff
                        if (powerUpsPlayer2.activePowerUps.Contains(0) && !DamageBuffUsed2)
                        {
                            DamageBuffUsed2 = true;
                            currentDamage += 0.1f;
                        }
                        if (powerUpsPlayer2.activePowerUps.Contains(8))
                        {
                            currentDamage = currentDamage / 2f;
                        }
                        //deffences
                        if (!powerUpsPlayer2.activePowerUps.Contains(7))
                        {
                            if (powerUpsPlayer1.activePowerUps.Contains(2) && !DeffenceBuffUsed1)
                            {
                                DeffenceBuffUsed1 = true;
                                currentDamage = 0;
                            }
                            else if (powerUpsPlayer1.activePowerUps.Contains(1))
                            {
                                currentDamage = currentDamage / 2f;
                            }
                        }
                        //int attackIndex = UnityEngine.Random.Range(1, 5);
                        //animator2.SetInteger("AttackIndex", attackIndex);
                        animator2.SetTrigger("AttackPeerform");
                        player1Health.TakeDamage(currentDamage, "player 2 wins");
                        playerAttackIndex2 = 0;
                        StartCoroutine(MoveSprites2()); // coroutine for next
                        if (!powerUpsPlayer2.activePowerUps.Contains(8))
                        {
                            BaseComboTime += attackPlusTime;
                        }
                    }
                    currentKeyPressed2 = null;
                    UpdateCollors();
                }
                else
                {
                    //IsDoingShareAttack2 = false;
                    if (powerUpsPlayer2.activePowerUps.Contains(8))
                    {
                        currentKeyPressed2 = null;
                    }
                    else
                    {
                        BadCombination(2);
                        playerAttackIndex2 = 0;
                        currentKeyPressed2 = null;
                    }
                }
            }
        }
    }



    //QTE
    [SerializeField] private GameObject timeBarGroup; // Whole time bar group just to switch its visabiliti
    [SerializeField] private RectTransform timeBar; // Time Bar indicator

    List<char> RandomKeys1 = new List<char>();
    List<char> RandomKeys2 = new List<char>();
    private float BaseComboTimeSave = 4f; /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private float BaseComboTime = 4f;
    private float attackPlusTime = 0.3f;
    List<char> NextAttack1 = new List<char>();
    List<char> NextAttack2 = new List<char>();

    List<char> NextShare1 = new List<char>();
    List<char> NextShare2 = new List<char>();

    //List<char> CurrentShare1 = new List<char>();
    //List<char> CurrentShare2 = new List<char>();

    List<char> CurrentAttack1 = new List<char>();
    List<char> CurrentAttack2 = new List<char>();

    public bool comboEnded = true;


    private IEnumerator FightQTE() 
    {
        BaseComboTime = BaseComboTimeSave;
        timeBarGroup.SetActive(true);
        comboEnded = false;
        float roundStartTime = time;
        float timeLeft;

        if (powerUpsPlayer1.activePowerUps.Contains(6) || powerUpsPlayer2.activePowerUps.Contains(6))
        {
            BaseComboTime = BaseComboTime / 2f;
        }



        while (!comboEnded)
        {
            while ((timeLeft = roundStartTime + BaseComboTime - time) > 0)
            {
                checkPlayersAttacks();
                UpdateTimeBar(timeLeft);
                yield return null;
            }
            comboEnded = true;
        }
        playerAttackIndex1 = 0;
        playerAttackIndex2 = 0;
        UpdateCollors();
        IsDoingShareAttack1 = true;
        IsDoingShareAttack2 = true;
        SharedAttackSuccess1 = false;
        SharedAttackSuccess2 = false;
        UpdateTimeBar(0);
        timeBarGroup.SetActive(false);
        Debug.Log("end");
        Createshare();
        playerAttackIndex1 = 0;
        playerAttackIndex2 = 0;
        UpdateCollors();
        for(int i = 0; i < 6; i++)
        {
            animSlicePlayer1SharedTrue[i].Play("Back");
            animSlicePlayer2SharedTrue[i].Play("Back");
        }

        foreach (var anim in animSlicePlayer1SharedFalse)
        {
            anim.Play("Back");
        }

        foreach (var anim in animSlicePlayer2SharedFalse)
        {
            anim.Play("Back");
        }

        yield return null;
    }

}
