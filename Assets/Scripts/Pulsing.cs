using UnityEngine;

public class Pulsing : MonoBehaviour
{

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float pulseSpeed;
    [SerializeField] private float maxAlpha;
    [SerializeField] private float minAlpha;




    void Update()
    {

        float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);

        Color c = sprite.color;
        c.a = alpha;
        sprite.color = c;
    }
}
