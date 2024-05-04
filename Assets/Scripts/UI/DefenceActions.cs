using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DefenseActions : MonoBehaviour
{
    string wallSelected;
    bool abandonable;
    int repairCost;

    [SerializeField] GameObject Options;
    [SerializeField] TMP_Text selectionName;
    [SerializeField] TMP_Text repairCostDisplay;
    [SerializeField] Button abandonButton;
    [SerializeField] Button repairButton;

    public void GetSelection(string variableName) 
    { 
        Options.SetActive(false);

        wallSelected = variableName;
        abandonable = variableName == "f" + VariableSingleton.Instance.EnduringWall.GetCodeName() ? true : false;
        
        switch (variableName)
        {
            case "fOuterWall":
                selectionName.text = "Theodesian";
                break;

            case "fMiddleWall":
                selectionName.text = "Constantinian";
                break;

            case "fInnerWall":
                selectionName.text = "Severan";
                break;
        }

        selectionName.text += " Wall Actions";

        repairCost = Mathf.RoundToInt((1 - VariableSingleton.GetFloatVariable(variableName)) * 1000);
        repairCostDisplay.text = repairCost.ToString();
        repairButton.interactable = repairCost <= VariableSingleton.GetIntVariable("iMoney") && 1 > VariableSingleton.GetFloatVariable(variableName);

        abandonButton.interactable = abandonable;

        Options.SetActive(true);
    }


    /// <summary>
    /// Repairs currently selected wall for random value between 0.3 and 0.6
    /// </summary>
    public void RepairSelected() 
    {
        VariableSingleton.ChangeInt("iMoney", -repairCost);
        VariableSingleton.ChangePercentFloat(wallSelected, Random.value*0.3f + 0.3f);
    }

    public void AbandonSelected() 
    {
        VariableSingleton.Instance.EnduringWall.Abandon();

    }
}
