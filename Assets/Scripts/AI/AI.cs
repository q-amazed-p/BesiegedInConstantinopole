using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AI: MonoBehaviour
{
    private static AI _instance;
    public static AI Instance => _instance;

    [SerializeField] AssaultUI assaultUI;

    OttomanDecision ottomanTactic;

    [SerializeField] GameObject TacticPrepare;
    [SerializeField] GameObject TacticBombardWall;
    [SerializeField] GameObject TacticTestWall;
    [SerializeField] GameObject TacticAttackWall;
    [SerializeField] GameObject TacticSendAssault;
    [SerializeField] GameObject TacticAdvance;

    [SerializeField] AssaultSystem assaultSystem;

    [SerializeField] int WallOverrunThreshold;

    [SerializeField] int christianBalkanSubjectAV;
    [SerializeField] int bashiBazoukAV;
    [SerializeField] int janissaryAV;

    [SerializeField] int averageBalkanSubjectForce;
    [SerializeField] int averageBashiBazoukForce;
    [SerializeField] int averageJanissaryForce;
    //fields for assault numbers

    public void TakeAIAction()
    {
        float moatRemaining = VariableSingleton.GetFloatVariable("fMoat");
        float wallHealth = VariableSingleton.GetFloatVariable("fOuterWall");     //needs function identifying current wall
        float sultanRage = VariableSingleton.GetFloatVariable("fSultanRage");    //needs update
        if (moatRemaining >= wallHealth) 
        {
            Debug.Log("Enemy chose preparatory tactic");
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

    //there could be a custom object to contain these for turns where it's necessary
    int assaultTest;            //equal to required varangian n
    bool isJanissaryAssault;
    int attackerNumbers;

    public void BreachingAssaultAftermath(bool deployVarangians) 
    {
        assaultSystem.AssaultAftermath(isJanissaryAssault, attackerNumbers, false, deployVarangians? assaultTest : 0);
        assaultUI.AssaultAftermathUI();
    }

    public void BalkanSubjectAssault()
    {
        Debug.Log("Enemy launched Balkan Subject Assault");
        int balkanSubjectNumbers = VariableSingleton.GetIntVariable("iBalkans");
        assaultTest = assaultSystem.TestAssault(christianBalkanSubjectAV) - WallOverrunThreshold;
        if (assaultTest > 0) 
        {
            isJanissaryAssault = false;
            attackerNumbers = averageBalkanSubjectForce < balkanSubjectNumbers ? averageBalkanSubjectForce : balkanSubjectNumbers;
            assaultUI.SetAssaultResultButton(false);
        }
        else
        {
            assaultSystem.AssaultAftermath(false, averageBalkanSubjectForce < balkanSubjectNumbers ? averageBalkanSubjectForce : balkanSubjectNumbers);
            assaultUI.SetAssaultResultButton(true);
        }
    }

    public void BashiBazoukAssault()
    {
        Debug.Log("Enemy launched Bashi Bazouk Assault");
        int bashiBazoukNumbers = VariableSingleton.GetIntVariable("iBazouks");
        assaultTest = assaultSystem.TestAssault(bashiBazoukAV) - WallOverrunThreshold;
        if (assaultTest > 0) 
        {
            isJanissaryAssault = false;
            attackerNumbers = averageBashiBazoukForce < bashiBazoukNumbers ? averageBashiBazoukForce : bashiBazoukNumbers;
            assaultUI.SetAssaultResultButton(false);
        }
        else
        {
            assaultSystem.AssaultAftermath(false, averageBashiBazoukForce < bashiBazoukNumbers ? averageBashiBazoukForce : bashiBazoukNumbers);
            assaultUI.SetAssaultResultButton(true);
        }
    }

    public void JanissaryAssault()
    {
        Debug.Log("Enemy launched Janissary Assault");
        int janissaryNumbers = VariableSingleton.GetIntVariable("iJanissaries");
        assaultTest = assaultSystem.TestAssault(janissaryAV) - WallOverrunThreshold;
        if (assaultTest > 0)
        {
            isJanissaryAssault = true;
            attackerNumbers = averageJanissaryForce < janissaryNumbers ? averageJanissaryForce : janissaryNumbers;
            assaultUI.SetAssaultResultButton(false);
        }
        else
        {
            assaultSystem.AssaultAftermath(true, averageJanissaryForce < janissaryNumbers ? averageJanissaryForce : janissaryNumbers);
            assaultUI.SetAssaultResultButton(true);
        }
    }


    /*********
     * UNITY */

    private void Awake()
    {
        _instance = this;
    }
}
