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
        string nodeSelection;
        if (VariableSingleton.Instance.StoryScheduled())
        {
            nodeSelection = "story" + VariableSingleton.Turn;
            eventPhaseUI.GetComponent<EventUI>().SetStartNode(nodeSelection);
        }
        else
        {
            nodeSelection = "random" + VariableSingleton.Instance.RandomEventID();
            eventPhaseUI.GetComponent<EventUI>().SetStartNode(nodeSelection);
        }
        Debug.Log("Node selected: " + nodeSelection);
        SwitchPhase(eventPhaseUI);
    }
}
