using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AI: MonoBehaviour
{
    private AI _instance;
    public AI Instance => _instance;

    OttomanDecision ottomanTactic;

    [SerializeField] GameObject TacticPrepare;
    [SerializeField] GameObject TacticBombardWall;
    [SerializeField] GameObject TacticTestWall;
    [SerializeField] GameObject TacticAttackWall;
    [SerializeField] GameObject TacticSendAssault;
    [SerializeField] GameObject TacticAdvance;

    [SerializeField] AssaultSystem assaultSystem;

    [SerializeField] int christianBalkanSubjectAV;
    [SerializeField] int bashiBazoukAV;
    [SerializeField] int janissaryAV;

    //fields for assault numbers

    public void TakeAIAction()
    {
        float moatRemaining = VariableSingleton.GetFloatVariable("fMoat");
        float wallHealth = VariableSingleton.GetFloatVariable("fOuterWall");     //needs function identifying current wall
        float sultanRage = VariableSingleton.GetFloatVariable("fSultanRage");    //needs update
        if (moatRemaining > wallHealth) 
        {
            ottomanTactic = Instantiate(TacticPrepare, transform).GetComponent<OttomanDecision>();
        }
        else switch (wallHealth) 
        {
            case > 0.75f:
                ottomanTactic = Instantiate(TacticBombardWall, transform).GetComponent<OttomanDecision>();
                break;

            case > 0.5f:
                ottomanTactic = Instantiate(TacticTestWall, transform).GetComponent<OttomanDecision>();
                break;

            case > 0.25f:
                ottomanTactic = Instantiate(TacticAttackWall, transform).GetComponent<OttomanDecision>();
                break;

            case > 0:
                ottomanTactic = Instantiate(TacticSendAssault, transform).GetComponent<OttomanDecision>();
                break;

            default:
                ottomanTactic = Instantiate(TacticAdvance, transform).GetComponent<OttomanDecision>();
                break;

        }

        ottomanTactic.EliminateRedundant();
        ottomanTactic.ExecuteRandomAction();

        Destroy(ottomanTactic.gameObject);
    }

    /****************
     * SIEGE OPTIONS*/

    public void DecisionFillMoat()
    {
        VariableSingleton.ChangeFloat("fMoat", 0.1f, 0.2f);
    }

    public void DecisionBuildTower()
    {
        VariableSingleton.ChangeBool("bTower", true);
    }

    public void DecisionBuildRam()
    {
        VariableSingleton.ChangeBool("bRam", true);
    }

    public void DecisionBuildRamp()
    {
        VariableSingleton.ChangeFloat("fRamp", 0.1f, 0.2f);
    }

    public void MoarCannons()
    {
        
    }


    /******************
     * ASSAULT OPTIONS*/

    public void Bombard()
    {
        VariableSingleton.GetIntVariable("iTurkCannons");    //can do maths on it first
    }

    public void BalkanSubjectAssault()
    {
        int balkanSubjectNumbers = VariableSingleton.GetIntVariable("iBalkanSubjects");
        if (assaultSystem.TestAssault(christianBalkanSubjectAV)) 
        {
            assaultSystem.AssaultAftermath(false, 5000 < balkanSubjectNumbers ? 5000 : balkanSubjectNumbers);
        }
    }

    public void BashiBazoukAssault()
    {
        int bashiBazoukNumbers = VariableSingleton.GetIntVariable("iBashiBazouk");
        if (assaultSystem.TestAssault(bashiBazoukAV)) 
        {
            assaultSystem.AssaultAftermath(false, 10000 < bashiBazoukNumbers ? 10000 : bashiBazoukNumbers);
        }
    }

    public void JanissaryAssault()
    {
        int janissaryNumbers = VariableSingleton.GetIntVariable("iJanissaries");
        if (assaultSystem.TestAssault(janissaryAV))
        {
            assaultSystem.AssaultAftermath(true, 8000 < janissaryNumbers ? 8000 : janissaryNumbers);
        }
    }


    /*********
     * UNITY */

    private void Awake()
    {
        _instance = this;
    }
}
