using UnityEngine;
using UnityEngine.SceneManagement;


//Old veersion code
public class NextScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
