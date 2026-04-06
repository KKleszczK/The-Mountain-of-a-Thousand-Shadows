using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPowerUps : MonoBehaviour
{
    public bloodMoonShaderScript blood;
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private List<Sprite> sprites;

    public int[] activePowerUps = new int[2];
    void Start()
    {
        for (int i = 0; i < activePowerUps.Length; i++)
            activePowerUps[i] = -1;

        UpdatePowerUpUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PowerUp powerUp = other.GetComponent<PowerUp>();

        int powerIndex = powerUp.GetIndex();
        //chech if poweru is in list
        for (int i = 0; i < activePowerUps.Length; i++)
        {
            if (activePowerUps[i] == powerIndex)
            {
                other.gameObject.GetComponent<PowerUp>().destroyPowerUp();
                //Destroy(other.gameObject);
                return;
            }
        }
        //add to free slot
        for (int i = 0; i < activePowerUps.Length; i++)
        {
            if (activePowerUps[i] == -1)
            {
                if (powerIndex == 8)
                {
                    blood.HaveBloodMoon();
                }
                activePowerUps[i] = powerIndex;
                UpdatePowerUpUI();
                
                break;
            }
        }
        //destroy if full
        other.gameObject.GetComponent<PowerUp>().destroyPowerUp();
        //Destroy(other.gameObject);
    }
    //reset of the powerups l;ist
    public void ResetPowerUps()
    {
        for (int i = 0; i < activePowerUps.Length; i++)
        {
            activePowerUps[i] = -1;
        }
        blood.BloodReset();
        UpdatePowerUpUI();
    }
    private void UpdatePowerUpUI()
    {
        // Slot 1
        if (activePowerUps[0] >= 0 && activePowerUps[0] < sprites.Count)
            image1.sprite = sprites[activePowerUps[0]];
        else
            image1.sprite = sprites[9];

        // Slot 2
        if (activePowerUps[1] >= 0 && activePowerUps[1] < sprites.Count)
            image2.sprite = sprites[activePowerUps[1]];
        else
            image2.sprite = sprites[9];
    }

}
