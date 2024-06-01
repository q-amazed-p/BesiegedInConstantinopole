using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLossDisplay : MonoBehaviour
{
    [SerializeField] string wallsHeldLine = "The walls held off the assaullt.";
    [SerializeField] string wallsLostLine = "The walls given in, we had to retreat!";

    [SerializeField] TMP_Text infLoss;
    [SerializeField] TMP_Text archLoss;
    [SerializeField] TMP_Text cavLoss; 
    [SerializeField] TMP_Text varanLoss;
    [SerializeField] TMP_Text WallStatus;

    public void SetLossesInfo(int infN,  int archN, int cavN, int varanN, bool wallHeld) 
    {
        infLoss.text = infN.ToString();
        archLoss.text = archN.ToString();
        cavLoss.text = cavN.ToString();
        varanLoss.text = varanN.ToString();

        WallStatus.text = wallHeld ? wallsHeldLine : wallsLostLine;
    }

}
