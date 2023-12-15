using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    }

    // Date

    static DateTime siegeStart = new (1453, 4, 1);
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
}
