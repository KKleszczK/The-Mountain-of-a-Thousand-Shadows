using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SettingsBack : MonoBehaviour
{
    [SerializeField] private Button BackButton;
    [SerializeField] private float unloadDelay = 0.25f;

    private void Start()
    {
        BackButton.onClick.AddListener(Back);
    }

    private void Back()
    {
        StartCoroutine(BackWithDelay());
    }

    private IEnumerator BackWithDelay()
    {
        yield return new WaitForSeconds(unloadDelay);

        SceneManager.UnloadSceneAsync("Settings");
        MenuState.MenuPaused = false;
    }
}
