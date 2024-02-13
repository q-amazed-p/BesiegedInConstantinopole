using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Yarn.Unity;

public class EventDriver : MonoBehaviour
{
    //[SerializeField] TMP_Text damageDescript;
    //[SerializeField] TMP_Text wallHealth;

    [SerializeField] DialogueRunner myRunner;
    private void OnEnable()
    {
        myRunner.StartDialogue(myRunner.startNode);
        //float damage = Random.value * 0.4f;
        //switch (damage)
        //{
            //case < 0.1f:
            //    damageDescript.text = "Light";
            //    break;

            //case < 0.2f:
            //    damageDescript.text = "Moderate";
            //    break;

            //case < 0.3f:
            //    damageDescript.text = "Heavy";
            //    break;

            //case >= 0.3f:
            //    damageDescript.text = "Daunting";
            //    break;
        //}

        //wallHealth.text = (Mathf.Round(VariableSingleton.Instance.ChangeWallHealth(-damage)*100)).ToString() + "%";

    }
}
