using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SiegeUI : MonoBehaviour
{
    [SerializeField] ManagerUI manager;
    [SerializeField] TMP_Text wallHealthDisplay;
    private void OnEnable()
    {
        wallHealthDisplay.text = VariableSingleton.Instance.GetWallHealth() + "%";
    }


    [SerializeField] GameObject repairAction; 
    public void OrderRepairs()
    {
        repairAction.SetActive(true);
    }

    public void ApproveRepairs()
    {
        this.gameObject.SetActive(false);
        manager.ContinueToRest();
        
    }

}
