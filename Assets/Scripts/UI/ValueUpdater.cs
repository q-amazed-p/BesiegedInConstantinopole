using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text targetText;
    [SerializeField] string variableName;

    private void OnEnable()
    {
        switch(variableName[0])
        {
            case 'f':
                targetText.text = VariableSingleton.GetFloatVariable(variableName).ToString();
                break;

            case 'i':
                targetText.text = VariableSingleton.GetIntVariable(variableName).ToString(); 
                break;

        }
    }
}
