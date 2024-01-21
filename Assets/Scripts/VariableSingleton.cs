using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class VariableSingleton : MonoBehaviour
{
    static private VariableSingleton _instance;

    static public VariableSingleton Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    private void Start()
    {
        _instance = this;
        wallHealth = 0.8f;
        transform.GetComponentInChildren<LoaderScript>().RunLoader();
    }



    static DateTime siegeStart = new (1453, 4, 6);
    int turn = 1;

    public int IncrementTurn()
    {
        turn++;
        return turn;
    }

    public DateTime GetDate()
    {
        if (turn < 15)
        {
            return siegeStart - new TimeSpan((14 - turn) * 7, 0, 0, 0);
        }
        else
        {
            return siegeStart + new TimeSpan(turn - 14, 0, 0, 0);
        }
    }

    // Wall Condition

    float wallHealth;
    public float GetWallHealth()
    {
        return wallHealth;
    }
    public float ChangeWallHealth(float brick)
    {
        wallHealth += brick;
        if (wallHealth > 1) { wallHealth = 1; } else { 
        if (wallHealth < 0) { wallHealth = 0; }}
        return wallHealth;
    }

    public string Save()
    {
        string saveCode;

        saveCode = turn.ToString();
        saveCode += " ";
        saveCode += wallHealth.ToString();

        File.WriteAllText("Save", saveCode);
        return saveCode;
    }

    

    public void Load()
    {
        string[] saveBreakdown = new string[2];

        {
            string saveCode = File.ReadAllText("Save");
            int idx = 0;
            foreach (char sig in saveCode)
            {
                if(sig == ' ')
                {
                    idx++;
                }
                else
                {
                    saveBreakdown[idx] += sig;
                }
            }
        }

        turn = int.Parse(saveBreakdown[0]);
        Debug.Log(turn.ToString());
        wallHealth = float.Parse(saveBreakdown[1]);
        Debug.Log(wallHealth.ToString());
    }
}
