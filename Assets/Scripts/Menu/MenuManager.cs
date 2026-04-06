using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] MenuScene1 MenuScript;
    [SerializeField] Canvas Buttons;


 

    void Update()
    {
        if (MenuState.MenuPaused == false)
        {
            MenuScript.enabled = true;
            Buttons.enabled = true;
        }

        else
        {
            MenuScript.enabled = false;
            Buttons.enabled = false;
        }
            
    }
}