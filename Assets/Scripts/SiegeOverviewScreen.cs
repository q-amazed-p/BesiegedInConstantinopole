using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SiegeOverviewScreen : MonoBehaviour
{
    [SerializeField] BarInput spyNumber;

    [SerializeField] TMP_Text[] intelTypes;
    [SerializeField] TMP_Text[] intelValues;

    public void UpdateIntelTypes() 
    { 
        for(int i = intelTypes.Length-1; i >= 0; i--)
        {
            if (i >= spyNumber.Value) intelTypes[i].alpha = 0.5f;
            else intelTypes[i].alpha = 1;
        }
    }

    public void UpdateIntelValues() 
    {
        for (int i = intelValues.Length-1; i >= 0; i--)
        {
            if (i >= spyNumber.Value) intelValues[i].enabled = false;
            else intelValues[i].enabled = true;
        }
    }

    private void Start()
    {
        UpdateIntelTypes();
        UpdateIntelValues();
    }
}
