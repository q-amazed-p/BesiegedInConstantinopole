using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerUI : UIWindowFam
{
    [SerializeField] GameObject diplomacyUI;
    [SerializeField] GameObject siegeUI;
    [SerializeField] GameObject troopsUI;
    [SerializeField] GameObject spiesUI;
    [SerializeField] GameObject peopleUI;

    [SerializeField] GameObject restPhaseUI;

    [SerializeField] TMP_Text date;


    //*****************************
    public void BringUpDiplomacy()
    {
        SwitchPhase(diplomacyUI);
        ControlSingleton.Instance.LogToEsc(this.gameObject, diplomacyUI);
    }

    //*****************************
    public void BringUpSiegeUI()
    {
        SwitchPhase(siegeUI);
        ControlSingleton.Instance.LogToEsc(this.gameObject, siegeUI);
    }

    [SerializeField] GameObject repairAction;
    public void OrderRepairs()
    {
        repairAction.SetActive(true);
        ControlSingleton.Instance.LogToEsc(siegeUI, repairAction);
    }

    public void ApproveRepairs()
    {
        ActionResults.Instance.LastAction = 0;
        ControlSingleton.Instance.GoBack();
        ControlSingleton.Instance.GoBack();
        ContinueToRest();
    }


    //*****************************
    public void BringUpTroopsUI()
    {
        SwitchPhase(troopsUI);
        ControlSingleton.Instance.LogToEsc(this.gameObject, troopsUI);
    }

    [SerializeField] GameObject recruitmentAction;
    public void OrderTroopRecruitment()
    {
        recruitmentAction.SetActive(true);
        ControlSingleton.Instance.LogToEsc(troopsUI, recruitmentAction);
    }

    public void ApproveTroopRecruitment()
    {
        ActionResults.Instance.LastAction = 1;
        ControlSingleton.Instance.GoBack();
        ControlSingleton.Instance.GoBack();
        ContinueToRest();
    }






    //*****************************
    public void BringUpSpiesUI()
    {
        SwitchPhase(spiesUI);
        ControlSingleton.Instance.LogToEsc(this.gameObject, spiesUI);
    }

    public void BringUpPeopleUI()
    {
        SwitchPhase(peopleUI);
        ControlSingleton.Instance.LogToEsc(this.gameObject, peopleUI);
    }

    public void ContinueToRest()
    {
        SwitchPhase(restPhaseUI);
    }


    //*****************************
    // Refresh

    private void OnEnable()
    {
        date.text = VariableSingleton.GetDate().ToShortDateString();
    }
}
