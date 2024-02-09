using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class RestUI : UIWindowFam
{
    [SerializeField] GameObject eventPhaseUI;



    public void ContinueToEvent()
    {
        if (VariableSingleton.Instance.StoryScheduled())
        {
            eventPhaseUI.GetComponentInChildren<DialogueRunner>().startNode = "story" + VariableSingleton.Turn;
        }
        else
        {
            eventPhaseUI.GetComponentInChildren<DialogueRunner>().startNode = "random" + VariableSingleton.Instance.RandomEventID();
        }
        SwitchPhase(eventPhaseUI);
    }
}
