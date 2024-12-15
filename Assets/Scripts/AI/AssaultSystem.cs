using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultSystem : MonoBehaviour
{
    [Space(3)]
    [SerializeField] int rampAV;
    [SerializeField] int siegeTowerAV;
    [SerializeField] int batteringRamAV;
    [Space(3)]
    [SerializeField] int outerWallBonus;
    [SerializeField] int middleWallBonus;
    [SerializeField] int innerWallBonus;
    [Space(3)]
    [SerializeField] float cannonLethalityFactor;
    [SerializeField] float varangianLethalityFactor;
    [SerializeField] float jannisaryLethalityFacotr;
    [SerializeField] float commonEnemyLethalityFactor;
    [SerializeField] int archerLossWeight;
    [SerializeField] int infantryLossWeight;


    [SerializeField] PlayerLossDisplay playerLossDisplay;

    public int AssaultAftermath(bool isJanissaryAssault, int attackerNumbers, bool isWallHeld = true, int VarangiansDeployed = 0)
    {
        int attackersPervading = Mathf.RoundToInt( attackerNumbers * (VariableSingleton.GetFloatVariable("fMoat") > 0 ? 0.9f : 1) //info needed
                                 - VariableSingleton.GetFloatVariable("fArchersQuality") * VariableSingleton.GetIntVariable("iArchers")
                                 - cannonLethalityFactor * VariableSingleton.GetIntVariable("iCannons"));

        VariableSingleton.ChangeFloat("fMoat", -0.1f);

        int cavalryCasualities = 0;
        int archerCasualities = 0;
        int infantryCasualities = 0;
        {
            float wallProtection = isWallHeld ? 1.0f - VariableSingleton.Instance.EnduringWall.GetHealth() : 0;
            int defenderCasualities = Mathf.RoundToInt((attackersPervading * (isJanissaryAssault ? jannisaryLethalityFacotr : commonEnemyLethalityFactor)
                                                        + Random.value * 100 - 50) * wallProtection);

            archerCasualities = defenderCasualities * archerLossWeight / (archerLossWeight + infantryLossWeight);
            infantryCasualities = defenderCasualities * infantryLossWeight / (archerLossWeight + infantryLossWeight);
        }

        int invaderCasualities = attackersPervading - Mathf.RoundToInt(VariableSingleton.GetFloatVariable("fInfantryQuality") * VariableSingleton.GetIntVariable("iInfantry")
                                 + varangianLethalityFactor * VarangiansDeployed + Random.value * 100 - 50);


        if (archerCasualities > VariableSingleton.GetIntVariable("iArchers"))
        { 
            infantryCasualities += -archerCasualities + VariableSingleton.GetIntVariable("iArchers"); 
        }
        VariableSingleton.ChangeInt("iArchers", -archerCasualities);

        if(infantryCasualities > VariableSingleton.GetIntVariable("iInfantry"))
        {
            cavalryCasualities = infantryCasualities - VariableSingleton.GetIntVariable("iInfantry");
            VariableSingleton.ChangeInt("iCavalry", -cavalryCasualities); 
        }
        VariableSingleton.ChangeInt("iInfantry", -infantryCasualities);
        //forgives Negative Cavalry
        //healers

        if (!isWallHeld) VariableSingleton.ChangeFloat("fSiegeRamp", -VariableSingleton.GetFloatVariable("fSiegeRamp"));
        else 
        {
            VariableSingleton.ChangeBool("bSiegeTower", false);
            VariableSingleton.ChangeBool("bBattteringRam", false);
        }

        playerLossDisplay.SetLossesInfo(infantryCasualities, archerCasualities, cavalryCasualities, VarangiansDeployed, isWallHeld);

        return invaderCasualities;
    }

    public int TestAssault(int assaultTypeAddend)
    {
        return assaultTypeAddend + VariableSingleton.GetIntVariable("iSultan") + SiegeEngeneeringAddend()
                            - Mathf.RoundToInt(100 * VariableSingleton.Instance.EnduringWall.GetHealth())
                            - VariableSingleton.GetIntVariable("iMorale") - WallBonus();                            //May need to add optional value from fleet
    }

    int SiegeEngeneeringAddend() 
    {
        return VariableSingleton.GetFloatVariable("fSiegeRamp") >= 1 ? rampAV : 0
                  + (VariableSingleton.GetBoolVariable("bSiegeTower") ? siegeTowerAV : 0 )
                  + (VariableSingleton.GetBoolVariable("bBatteringRam") ? batteringRamAV : 0);
    }  

    int WallBonus() 
    { 
        switch (VariableSingleton.Instance.EnduringWall.GetCodeName()) 
        {
            case "OuterWall":
                return outerWallBonus;

            case "MiddleWall":
                return middleWallBonus;

            case "InnerWall":
                return innerWallBonus;
        }
        Debug.Log("Exception invalid enduringWall = " + VariableSingleton.Instance.EnduringWall.GetCodeName());
        return 0;
    }
}
