using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class EventUI : UIWindowFam
{
    [SerializeField] GameObject assaultUI;
    [SerializeField] GameObject managerUI;
    [SerializeField] DialogueController dialogueController;

    public void ContinueToAssault()
    {
        if (VariableSingleton.HasSiegeStarted()) SwitchPhase(assaultUI);
        else SwitchPhase(managerUI);

    }

    public void SetStartNode(string NodeName) 
    { 
        dialogueController.SetStartNode(NodeName);
    }

}
