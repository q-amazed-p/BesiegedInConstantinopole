using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecruitationSelection : MonoBehaviour
{
    [SerializeField] GameObject superMenu;
    [SerializeField] TMP_Text displayName;

    public void GetSelectionIndex(int newIndex) 
    {
        transform.SetSiblingIndex(newIndex);
    }

    public void GetSelectionName(TMP_Text newNameContainer) 
    { 
        displayName.text = newNameContainer.text;
        LogRecruitationToEsc();
    }

    void LogRecruitationToEsc() 
    {
        ControlSingleton.Instance.LogToEsc(this.gameObject, superMenu);  //first parameter irrelevant for overlay application
                                                                         //alternative implementation of LogToEsc recommended
    }

}
