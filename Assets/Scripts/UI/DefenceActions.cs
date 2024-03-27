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
        abandonable = VariableSingleton.GetFloatVariable(variableName)<0.2f ? true : false;             //requires real logic
        
        switch (variableName)
        {
            case "fouter_wall":
                selectionName.text = "Theodesian";
                break;

            case "fmiddle_wall":
                selectionName.text = "Constantinian";
                break;

            case "finner_wall":
                selectionName.text = "Severan";
                break;
        }

        selectionName.text += " Wall Actions";

        repairCost.text = ((1 - VariableSingleton.GetFloatVariable(variableName)) * 1000).ToString();

        abandonButton.interactable = abandonable;

        Options.SetActive(true);
                
    }
}
