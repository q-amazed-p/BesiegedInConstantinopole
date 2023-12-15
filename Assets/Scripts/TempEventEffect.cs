using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempEventEffect : MonoBehaviour
{
    [SerializeField] TMP_Text wallAfterRepair;
    private void OnEnable()
    {
        wallAfterRepair.text = (Mathf.Round(VariableSingleton.Instance.ChangeWallHealth(Random.Range(0.08f, 0.2f)) * 100)).ToString() + "%" ;
    }
}
