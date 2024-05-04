using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class EventUI : UIWindowFam
{
    [SerializeField] GameObject assaultUI;
    [SerializeField] DialogueController dialogueController;

    public void ContinueToAssault()
    {
        SwitchPhase(assaultUI);
    }

    public void SetStartNode(string NodeName) 
    { 
        dialogueController.SetStartNode(NodeName);
    }

}
