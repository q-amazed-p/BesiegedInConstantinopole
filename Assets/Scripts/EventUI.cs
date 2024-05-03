using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventUI : UIWindowFam
{
    [SerializeField] GameObject assaultUI;

    public void ContinueToAssault()
    {
        SwitchPhase(assaultUI);
    }
}
