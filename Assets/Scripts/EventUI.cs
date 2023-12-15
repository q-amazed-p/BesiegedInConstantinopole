using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventUI : UIWindowFam
{
    [SerializeField] GameObject managerUI;

    public void ContinueToEvent()
    {
        SwitchPhase(managerUI);
    }
}
