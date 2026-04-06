using UnityEngine;

public class MusicToHpDiff : MonoBehaviour
{
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private float minVolume = 0.1f;
    [SerializeField] private float maxVolume = 0.15f;

    public TestHealth player1Health;
    public TestHealth player2Health;

    private void Update()
    {
        float HpSum = player1Health.GetHp() + player2Health.GetHp();
        float t = Mathf.Clamp01((2f - HpSum) / (2f - 0.6f));
        bgMusic.volume = Mathf.Lerp(minVolume, maxVolume, t);
    }
}
