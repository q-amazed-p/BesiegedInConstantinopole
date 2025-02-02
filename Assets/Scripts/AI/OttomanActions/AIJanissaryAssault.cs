using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJanissaryAssault : OttomanAction
{
    public override bool IsRestricted()
    {
        return VariableSingleton.GetFloatVariable("iJanissaries") == 0;
    }

    public override void Execute()
    {
        AI.Instance.AIJanissaryAssault();
    }
}