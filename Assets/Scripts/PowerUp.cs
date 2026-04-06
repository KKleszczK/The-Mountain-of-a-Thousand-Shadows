using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class PowerUp : MonoBehaviour
{


    public float floatStrength = 0.2f;
    public float speed = 1f;
    private Vector3 startPos;

    private float timer = 0f;
    public float fadeDuration = 1f;

    public float ScaleDuration = 1f;

    [SerializeField] private SpriteRenderer spriteImage;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private SpriteRenderer backSpriteRend;
    [SerializeField] private AudioSource pickupAudio;

    private int index;




    void Start()
    {

        transform.localScale = Vector3.zero;

        startPos = transform.position;

        Color c = spriteImage.color;
        c.a = 0f;
        spriteImage.color = c;

        //powerup randomizer
        int PowerUpIndex = Random.Range(0, 9);
        if (PowerUpIndex == 8)
        {
            index = Random.Range(0, 9);
        }
        else
        {
            index = PowerUpIndex;
        }
        spriteImage.sprite = sprites[index];

        //change backgraoun color for epic powerUp
        if (index == 8)
        {
            backSpriteRend.color = Color.red;
            floatStrength = 0.2f;
            speed = 1.75f;
            spriteImage.transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
            backSpriteRend.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
}
    }


    void Update()
    {
        if (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            Color c = spriteImage.color;
            c.a = alpha;
            spriteImage.color = c;

            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, alpha);
        }

        float newY = Mathf.Sin(Time.time * speed) * floatStrength;
        transform.position = startPos + new Vector3(0, newY, 0);
    }

    public int GetIndex()
    {
        return index; //give index of a powertup
    }


    private bool PickedUp = false;
    public void destroyPowerUp()
    {
        if (!PickedUp)
        {
            PickedUp = true;
            pickupAudio.Play();
        }
        StartCoroutine(DestroyAnimation());
    }

    private IEnumerator DestroyAnimation()
    {
        float duration = 0.5f;
        float timer = 0f;

        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale * 1.5f;

        Color startColor = spriteImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            spriteImage.color = Color.Lerp(startColor, targetColor, t);

            yield return null;
        }
        Destroy(gameObject);
    }
}
