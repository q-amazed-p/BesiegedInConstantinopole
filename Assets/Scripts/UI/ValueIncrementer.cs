using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueIncrementer : MonoBehaviour
{
    [SerializeField] TMP_InputField numberDisplayed;

    int maxAffordable;
    public void SetMaxAffordable(int newMax) => maxAffordable = newMax;

    int numberHeld = 0;
    public int CommitNumber() 
    {
        int reading = numberHeld;
        numberHeld = 0;
        return reading;
    }

    public void IncreaseNumber(int delta) 
    {
        if(numberHeld + delta >= 0 && numberHeld + delta <= maxAffordable) numberHeld += delta;
        else numberHeld = 0;
        numberDisplayed.text = numberHeld.ToString();
    }

    public void OverrideNumber(string newNumberText) 
    {
        int newNumber = int.Parse(newNumberText);
        if (newNumber >= 0 && numberHeld <= maxAffordable) numberHeld = newNumber;
        else numberHeld = 0;
        numberDisplayed.text = numberHeld.ToString();
    }

    private void OnEnable()
    {
        numberHeld = 0;
    }
}
