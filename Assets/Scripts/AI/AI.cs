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

    [SerializeField] EnemyLossDisplay enemyLossDisplay;
    
    public void TakeAIAction()
    {
        float moatRemaining = VariableSingleton.GetFloatVariable("fMoat");
        float wallHealth = VariableSingleton.GetFloatVariable("fOuterWall");     //needs function identifying current wall
        float sultanRage = VariableSingleton.GetFloatVariable("fSultanRage");    //needs update
        if (moatRemaining > 0) 
        {
            Debug.Log("Enemy chose preparatory tactic");
            ottomanTactic = Instantiate(TacticPrepare, transform).GetComponent<OttomanDecision>();
        }
        else switch (wallHealth) 
        {
            case > 0.75f:
                Debug.Log("Enemy chose bombardment tactic");
                ottomanTactic = Instantiate(TacticBombardWall, transform).GetComponent<OttomanDecision>();
                break;

            case > 0.5f:
                Debug.Log("Enemy chose test wall tactic");
                ottomanTactic = Instantiate(TacticTestWall, transform).GetComponent<OttomanDecision>();
                break;

            case > 0.25f:
                Debug.Log("Enemy chose attack wall tactic");
                ottomanTactic = Instantiate(TacticAttackWall, transform).GetComponent<OttomanDecision>();
                break;

            case > 0:
                Debug.Log("Enemy chose send assault tactic");
                ottomanTactic = Instantiate(TacticSendAssault, transform).GetComponent<OttomanDecision>();
                break;

            default:
                Debug.Log("Enemy chose advance tactic");
                ottomanTactic = Instantiate(TacticAdvance, transform).GetComponent<OttomanDecision>();
                break;

        }

        ottomanTactic.EliminateRedundant();
        ottomanTactic.ExecuteRandomAction();

        Destroy(ottomanTactic.gameObject);
    }

    /****************
     * SIEGE OPTIONS*/

    public void AIFillMoat()
    {
        Debug.Log("*Enemy is filling the moat!");
        VariableSingleton.ChangeFloat("fMoat", -0.09f, -0.2f);
    }

    public void AIBuildTower()
    {
        Debug.Log("*Enemy is building a tower!");
        VariableSingleton.ChangeBool("bTower", true);
    }

    public void AIBuildRam()
    {
        Debug.Log("*Enemy is building a ram!");
        VariableSingleton.ChangeBool("bRam", true);
    }

    public void AIBuildRamp()
    {
        Debug.Log("*Enemy is building a ramp!");
        VariableSingleton.ChangeFloat("fRamp", 0.1f, 0.2f);
    }

    public void AIMoarCannons()
    {
        Debug.Log("*Enemy is bringing more cannons!");
        VariableSingleton.ChangeInt("iTurkCannons", 1);
    }


    /******************
     * ASSAULT OPTIONS*/

    public void AIBombard()
    {
        Debug.Log("*Enemy launched a bompardment(this so far does nothing)");
        VariableSingleton.GetIntVariable("iTurkCannons");    //can do maths on it first
    }

    enum AssaultType 
    { 
        Balkan,
        Bazouk,
        Janissary
    }
    //there could be a custom object to contain these for turns where it's necessary
    int assaultTest;            //equal to required varangian n
    AssaultType assaultType;
    int attackerNumbers;

    public void BreachingAssaultAftermath(bool deployVarangians) 
    {
        int enemyLosses = assaultSystem.AssaultAftermath(assaultType==AssaultType.Janissary, attackerNumbers, false, deployVarangians? assaultTest : 0);
        
        RecordEnemyLosses(enemyLosses);

        assaultUI.AssaultAftermathUI();
    }

    void RecordEnemyLosses(int enemyLosses) 
    {
        switch (assaultType)
        {
            case AssaultType.Balkan:
                VariableSingleton.ChangeInt("iBalkans", -enemyLosses); break;

            case AssaultType.Bazouk:
                VariableSingleton.ChangeInt("iBazouks", -enemyLosses); break;

            case AssaultType.Janissary:
                VariableSingleton.ChangeInt("iJanissaries", -enemyLosses); break;
        }

        enemyLossDisplay.SetLossesInfo((int)assaultType, enemyLosses);
    }

    public void AIBalkanSubjectAssault()
    {
        Debug.Log("*Enemy launched Balkan Subject Assault");
        int balkanSubjectNumbers = VariableSingleton.GetIntVariable("iBalkans");
        assaultTest = assaultSystem.TestAssault(christianBalkanSubjectAV) - WallOverrunThreshold;
        if (assaultTest > 0) 
        {
            assaultType = AssaultType.Balkan;
            attackerNumbers = averageBalkanSubjectForce < balkanSubjectNumbers ? averageBalkanSubjectForce : balkanSubjectNumbers;
            assaultUI.SetAssaultResultButton(false);
        }
        else
        {
            int enemyLosses = assaultSystem.AssaultAftermath(false, averageBalkanSubjectForce < balkanSubjectNumbers ? averageBalkanSubjectForce : balkanSubjectNumbers);
            VariableSingleton.ChangeInt("iBalkans", -enemyLosses);
            RecordEnemyLosses(enemyLosses);
            assaultUI.SetAssaultResultButton(true);
        }
    }

    public void AIBashiBazoukAssault()
    {
        Debug.Log("*Enemy launched Bashi Bazouk Assault");
        int bashiBazoukNumbers = VariableSingleton.GetIntVariable("iBazouks");
        assaultTest = assaultSystem.TestAssault(bashiBazoukAV) - WallOverrunThreshold;
        if (assaultTest > 0) 
        {
            assaultType = AssaultType.Bazouk;
            attackerNumbers = averageBashiBazoukForce < bashiBazoukNumbers ? averageBashiBazoukForce : bashiBazoukNumbers;
            assaultUI.SetAssaultResultButton(false);
        }
        else
        {
            int enemyLosses = assaultSystem.AssaultAftermath(false, averageBashiBazoukForce < bashiBazoukNumbers ? averageBashiBazoukForce : bashiBazoukNumbers);
            VariableSingleton.ChangeInt("iBazouks", -enemyLosses);
            RecordEnemyLosses(enemyLosses);
            assaultUI.SetAssaultResultButton(true);
        }
    }

    public void AIJanissaryAssault()
    {
        Debug.Log("*Enemy launched Janissary Assault");
        int janissaryNumbers = VariableSingleton.GetIntVariable("iJanissaries");
        assaultTest = assaultSystem.TestAssault(janissaryAV) - WallOverrunThreshold;
        if (assaultTest > 0)
        {
            assaultType = AssaultType.Janissary;
            attackerNumbers = averageJanissaryForce < janissaryNumbers ? averageJanissaryForce : janissaryNumbers;
            assaultUI.SetAssaultResultButton(false);
        }
        else
        {
            int enemyLosses = assaultSystem.AssaultAftermath(true, averageJanissaryForce < janissaryNumbers ? averageJanissaryForce : janissaryNumbers);
            VariableSingleton.ChangeInt("iJanissaries", -enemyLosses);
            RecordEnemyLosses(enemyLosses);
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
