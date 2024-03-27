using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SiegeDecision : MonoBehaviour
{
    public Func<bool> MakeSiegeDecision() 
    {
        Func<bool> decision;

        if (VariableSingleton.GetFloatVariable("fMoat") > VariableSingleton.GetFloatVariable("fWall"))
        {
            decision = DecisionFillMoat;    //insert nuanced decision here
        }
        else decision = DecisionAssault;

        return decision;
    }

    public bool DecisionFillMoat() 
    {
        VariableSingleton.ChangeFloat("fMoat", 0.1f, 0.2f);
        return false;
    }

    public bool DecisionBuildTower()
    {
        VariableSingleton.ChangeBool("bTower", true);
        return false;
    }

    public bool DecisionBuildRam()
    {
        VariableSingleton.ChangeBool("bRam", true);
        return false;
    }

    public bool DecisionBuildRamp() 
    {
        VariableSingleton.ChangeFloat("fRamp", 0.1f, 0.2f);
        return false;
    }

    public bool MoarCannons() 
    {
        return false;
    }

    public bool DecisionAssault() 
    {
        return true;
    }
}
