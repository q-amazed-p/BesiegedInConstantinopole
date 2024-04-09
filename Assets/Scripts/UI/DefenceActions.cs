using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DefenseActions : MonoBehaviour
{
    string wallSelected;
    bool abandonable;

    [SerializeField] GameObject Options;
    [SerializeField] TMP_Text selectionName;
    [SerializeField] TMP_Text repairCost;
    [SerializeField] Button abandonButton;

    public void GetSelection(string variableName) 
    { 
        Options.SetActive(false);

        wallSelected = variableName;
        abandonable = VariableSingleton.GetFloatVariable(variableName) > 0.2f ? false : true;             //requires real logic
        
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

        repairCost.text = ((1 - VariableSingleton.GetFloatVariable(variableName)) * 1000).ToString();

        abandonButton.interactable = abandonable;

        Options.SetActive(true);
                
    }
}
