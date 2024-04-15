using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PercentConscriptedUpdater : MonoBehaviour
{
    [SerializeField]TMP_Text targetText;

    private void OnEnable()
    {
        float fractionConscripted = (float)((VariableSingleton.GetIntVariable("iInfantry") + VariableSingleton.GetIntVariable("iArchers")
                                    + VariableSingleton.GetIntVariable("iCavalry"))) / (float)VariableSingleton.GetIntVariable("iPopulation");

        float percentageToThreeDecimals = Mathf.Round(fractionConscripted * 100000) / 1000;

        targetText.text = percentageToThreeDecimals.ToString() + "% Population Conscripted";
    }
}
