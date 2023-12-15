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
        wallHealth = 80;
    }



    float wallHealth;
    public float GetWallHealth()
    {
        return wallHealth;
    }
    public void ChangeWallHealth(float brick)
    {
        wallHealth += brick;
    }
}
