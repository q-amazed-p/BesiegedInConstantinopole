using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUI : UIWindowFam
{
    [SerializeField] GameObject optionsDialogue;
    [SerializeField] GameObject loadDialogue;

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Load()
    {
        SwitchPhase(optionsDialogue);
    }

    public void Options()
    {
        SwitchPhase(optionsDialogue);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
