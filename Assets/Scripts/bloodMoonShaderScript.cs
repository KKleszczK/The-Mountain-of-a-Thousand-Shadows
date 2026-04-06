using UnityEngine;
using UnityEngine.Experimental.Playables;


public class bloodMoonShaderScript : MonoBehaviour
{
    public Material material;
    public float speed = 1f;
    public float resetSpeed = 0.2f;
    private float intensity = 0f;
    private bool isActive = false;
    private bool Resetbool = false;

    [SerializeField] private SpriteRenderer DeathSprite;

    public void HaveBloodMoon()
    {
        isActive = true;
        Resetbool = false;
    }

    public void BloodReset()
    {
        Resetbool = true;
        isActive = false;
    }



    private void Start()
    {
        material.SetFloat("_Eff", 0f);
    }


    private void Update()
    {
        if (isActive && intensity < 1f)
        {
            intensity += Time.deltaTime * speed;
            material.SetFloat("_Eff", Mathf.Clamp01(intensity));
            DeathSprite.transform.localScale = Vector3.one * (1f + intensity * 2f);
            Color tmp = DeathSprite.color;
            tmp.a = 1f - intensity;
            DeathSprite.color = tmp;

        }
        if (Resetbool && intensity > 0f)
        {
            intensity -= Time.deltaTime / resetSpeed;
            material.SetFloat("_Eff", Mathf.Clamp01(intensity));
        }
    }
}
