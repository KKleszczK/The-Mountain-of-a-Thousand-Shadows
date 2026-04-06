using UnityEngine;
using UnityEngine.SceneManagement;

//Old Version Script

public class PressSpace : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  
        {
            SceneManager.LoadScene("Cutscene");
        }
    }
}
