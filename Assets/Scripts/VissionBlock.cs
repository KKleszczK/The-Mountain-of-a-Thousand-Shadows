using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using TMPro;

public class VissionBlock : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [SerializeField] private Image player1Block;
    [SerializeField] private Image player2Block;


    private PlayerPowerUps powerUpsPlayer1;
    private PlayerPowerUps powerUpsPlayer2;


    private void Start()
    {
        powerUpsPlayer1 = player1.GetComponent<PlayerPowerUps>();
        powerUpsPlayer2 = player2.GetComponent<PlayerPowerUps>();
    }


    private void Update()
    {
        if (powerUpsPlayer1.activePowerUps.Contains(5))
        {
            Color c1 = player2Block.color;
            c1.a = 1f;
            player2Block.color = c1;
        }
        else
        {
            Color c1 = player2Block.color;
            c1.a = 0f;
            player2Block.color = c1;
        }

        if (powerUpsPlayer2.activePowerUps.Contains(5))
        {
            Color c1 = player1Block.color;
            c1.a = 1f;
            player1Block.color = c1;
        }
        else
        {
            Color c1 = player1Block.color;
            c1.a = 0f;
            player1Block.color = c1;
        }
    }
}
