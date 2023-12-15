using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestUI : UIWindowFam
{
    [SerializeField] GameObject eventPhaseUI;

    public void ContinueToEvent()
    {
        SwitchPhase(eventPhaseUI);
    }
}
