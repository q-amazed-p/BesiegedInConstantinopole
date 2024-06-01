using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyLossDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text enemyLossInfo;

    public void SetLossesInfo(int AssaultType, int casualities) 
    {
        switch (AssaultType) 
        {
            case 0:
                enemyLossInfo.text = "Christian Balkan Subjects ";
                break;

            case 1:
                enemyLossInfo.text = "Bashi-Bazouk Troops ";
                break;

            case 2:
                enemyLossInfo.text = "Janissary Warriors ";
                break;
        }

        enemyLossInfo.text += casualities.ToString();
    }
}
