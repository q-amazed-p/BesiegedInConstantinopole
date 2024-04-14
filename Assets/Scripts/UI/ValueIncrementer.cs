using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ValueIncrementer : MonoBehaviour
{
    [SerializeField] TMP_InputField numberDisplayed;
    [SerializeField] string connectedVariablleName;

    int numberHeld = 0;

    public void IncreaseNumber(int delta) 
    {
        if(numberHeld + delta >= 0) numberHeld += delta;
        else numberHeld = 0;
        numberDisplayed.text = numberHeld.ToString();
    }

    public void OverrideNumber(string newNumberText) 
    {
        int newNumber = int.Parse(newNumberText);
        if (newNumber >= 0) numberHeld = newNumber;
        else numberHeld = 0;
        numberDisplayed.text = numberHeld.ToString();
    }

    private void OnEnable()
    {
        numberHeld = 0;
    }
}
