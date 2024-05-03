using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AssaultUI : UIWindowFam
{
    [SerializeField] GameObject managerUI;

    [SerializeField] GameObject wallsHeldMessage;
    [SerializeField] GameObject wallsOverwhelmedChoice;
    [SerializeField] GameObject assaultAftermathMessage;

    public bool DeployVarangiansDecision { private get; set; }

    public void ContinueToEmperorAction()
    {
        VariableSingleton.IncrementTurn();
        SwitchPhase(managerUI);
    }

    [SerializeField] Button procedeToAssaultResult;

    public void SetAssaultResultButton(bool wallsHeld) 
    {
        procedeToAssaultResult.onClick.AddListener(wallsHeld ? WallsHeld : WallsOverwhelmed);
    }

    public void WallsHeld() 
    { 
        wallsHeldMessage.SetActive(true);
    }

    public void WallsOverwhelmed()
    {
        wallsOverwhelmedChoice.SetActive(true);
    }

    public void AssaultAftermathUI() 
    { 
        assaultAftermathMessage.SetActive(true);
    }

    public void DecideVarangianDeployment() 
    {
        AI.Instance.BreachingAssaultAftermath(DeployVarangiansDecision);
    }

    /*********
     * UNITY */

    private void OnEnable()
    {
        AI.Instance.TakeAIAction();             //should return choice so the UI text can be adjusted
    }
}
