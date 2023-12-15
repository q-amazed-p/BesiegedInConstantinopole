using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
