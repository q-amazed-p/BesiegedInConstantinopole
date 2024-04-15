using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecruitationSelection : MonoBehaviour
{
    [SerializeField] GameObject superMenu;

    [SerializeField] TMP_Text displayName;
    [SerializeField] TMP_Text displayCost;
    [SerializeField] ValueIncrementer costDeterminant;

    string troopVariableName;
    int troopCost;

    public void GetSelectionIndex(int newIndex)
    {
        transform.SetSiblingIndex(newIndex);

        troopVariableName = VariableSingleton.Instance.RecruitmentPricelist[newIndex].GetVariableName();
        troopCost = VariableSingleton.Instance.RecruitmentPricelist[newIndex].GetCost();
        displayCost.text = troopCost.ToString();
        displayName.text = VariableSingleton.Instance.RecruitmentPricelist[newIndex].GetName();

        costDeterminant.SetMaxAffordable(VariableSingleton.GetIntVariable("iMoney") / troopCost);

        LogRecruitationToEsc();
    }

    void LogRecruitationToEsc() 
    {
        ControlSingleton.Instance.LogToEsc(this.gameObject, superMenu);  //first parameter irrelevant for overlay application
                                                                         //alternative implementation of LogToEsc recommended
    }

    public void CommitRecruitmentTransaction() 
    {
        int troopsOrdered = costDeterminant.CommitNumber();
        int totalCost = troopsOrdered * troopCost;

        if (VariableSingleton.GetIntVariable("iMoney") > totalCost) 
        {
            VariableSingleton.ChangeInt("iMoney", -totalCost);
            VariableSingleton.ChangeInt(troopVariableName, troopsOrdered);
        }
        
        ControlSingleton.Instance.GoBack();
    }
}
