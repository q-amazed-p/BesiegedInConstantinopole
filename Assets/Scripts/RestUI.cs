using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Yarn.Unity;

public class RestUI : UIWindowFam
{
    [SerializeField] TMP_Text actionResultText;
    [SerializeField] GameObject eventPhaseUI;


    private void OnEnable()
    {
        actionResultText.text = ActionResults.Instance.GetActionText();
    }


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
