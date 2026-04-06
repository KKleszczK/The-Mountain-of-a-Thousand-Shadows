using UnityEngine;
using UnityEngine.UI;

public class ClickSounds : MonoBehaviour
{

    [SerializeField] private AudioSource clickSource;
    [SerializeField] private AudioClip[] clickSounds;
    [SerializeField] private Button[] buttons;


    void Start()
    {
        foreach (var btn in buttons)
            btn.onClick.AddListener(PlayRandomClick);
    }

    private void PlayRandomClick()
    {
        int index = Random.Range(0, clickSounds.Length);
        clickSource.clip = clickSounds[index];
        clickSource.Play();
    }
}
