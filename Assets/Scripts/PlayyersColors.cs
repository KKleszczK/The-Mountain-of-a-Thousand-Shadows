using UnityEngine;
using UnityEngine.UI;

public class PlayyersColors : MonoBehaviour
{
    [SerializeField] private Image Player1HpCurrent;
    [SerializeField] private Image Player1HpRegen;
    [SerializeField] private Image Player2HpCurrent;
    [SerializeField] private Image Player2HpRegen;
    [SerializeField] private TrailRenderer Player1Treil;
    [SerializeField] private TrailRenderer Player2Treil;


    void Start()
    {
        Player1HpCurrent.color = PlayerColorSave.Player1Color1;
        Player1HpRegen.color = PlayerColorSave.Player1Color2;
        Player2HpCurrent.color = PlayerColorSave.Player2Color1;
        Player2HpRegen.color = PlayerColorSave.Player2Color2;
        Player1Treil.startColor = PlayerColorSave.Player1Color2;
        Player2Treil.startColor = PlayerColorSave.Player2Color2;
    }
}
