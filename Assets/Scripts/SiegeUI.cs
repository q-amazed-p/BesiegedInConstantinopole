using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SiegeUI : MonoBehaviour
{
    [SerializeField] TMP_Text wallHealthDisplay;
    private void OnEnable()
    {
        wallHealthDisplay.text = Mathf.Round(VariableSingleton.Instance.GetWallHealth()*100) + "%";
    }

}
