using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultDecision : MonoBehaviour
{
    public Func<int> MakeAssaultDecision() 
    {
        Func<int> decision = null;
        return decision;
    }

    public int Bombard()
    {
        return VariableSingleton.GetIntVariable("iTurkCannons");    //can do maths on it first
    }

    public int BalkanSubjectAssault() 
    {
        return VariableSingleton.GetIntVariable("iBalkanSubjects");
    }

    public int BashiBazoukAssault() 
    {
        return VariableSingleton.GetIntVariable("iBashiBazouk");
    }

    public int JanissaryAssault() 
    { 
        return VariableSingleton.GetIntVariable("iJanissaries");
    }

}
