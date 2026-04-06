using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class TestHealth : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;

    [SerializeField] private Material PlayerMaterial;
    

    public Combos combos;
    public Pause endtextscript;

    [Header("Health Layers")]
    public RectTransform missingHealth;
    public RectTransform regenHealth;
    public RectTransform actualHealth;

    [Header("Settings")]
    public float maxWidth = 560f;

    [Range(0f, 1f)] public float currentHealth = 1f;
    [Range(0f, 1f)] public float regenHealthAmount = 1f;

    public float regenRate = 0.03f;
    private PlayerPowerUps powerUpsPlayer;
    private PlayerPowerUps powerUpsEnemy;



    private void Start()
    {
        PlayerMaterial.SetFloat("_DamageEff", 0f);
        PlayerMaterial.SetFloat("_Hit", 0f);
        UpdateBar();
        powerUpsPlayer = player.GetComponent<PlayerPowerUps>();
        powerUpsEnemy = enemy.GetComponent<PlayerPowerUps>();
    }

    private void Update()
    {
        RegenerateHealth();
    }

    public void TakeDamage(float amount, string EndTitle)
    {
        currentHealth = Mathf.Clamp01(currentHealth - amount);
        regenHealthAmount = Mathf.Clamp01(regenHealthAmount - amount / 2f); 
        UpdateBar();
        FlashRed();

        if (currentHealth <= 0f)
        {
            //koniec gry
            endtextscript.ShowWinnerScrean(EndTitle);
            combos.StopButtons();
        }
    }


    private Coroutine currentFlash;

    public void FlashRed()
    {
        if (currentFlash != null)
            StopCoroutine(currentFlash);

        currentFlash = StartCoroutine(RedFlashCoroutine());
    }

    //Player geet hit visualization
    private IEnumerator RedFlashCoroutine()
    {

        PlayerMaterial.SetFloat("_Hit", 1f);
        yield return new WaitForSeconds(0.2f);
        PlayerMaterial.SetFloat("_Hit", 0f);


        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            PlayerMaterial.SetFloat("_DamageEff", t);
            yield return null;
        }
        PlayerMaterial.SetFloat("_DamageEff", 0f);
        currentFlash = null;
    }


    public float GetHp()
    {
        return currentHealth;
    }


    private void RegenerateHealth()
    {
        if (!combos.comboEnded) return;
        

        if (currentHealth < regenHealthAmount)
        {
            float rate = regenRate;

            if (powerUpsPlayer.activePowerUps.Contains(3))
            {
                rate = rate * 2;
            }
            if (powerUpsEnemy.activePowerUps.Contains(4))
            {
                rate = 0f;
            }

                currentHealth += rate * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth, regenHealthAmount);
            UpdateBar();
        }
    }


    private void UpdateBar()
    {
        if (missingHealth != null)
            missingHealth.sizeDelta = new Vector2(maxWidth, missingHealth.sizeDelta.y);

        if (regenHealth != null)
            regenHealth.sizeDelta = new Vector2(maxWidth * regenHealthAmount, regenHealth.sizeDelta.y);

        if (actualHealth != null)
            actualHealth.sizeDelta = new Vector2(maxWidth * currentHealth, actualHealth.sizeDelta.y);
    }
}