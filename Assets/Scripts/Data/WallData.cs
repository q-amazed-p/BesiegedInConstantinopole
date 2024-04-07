using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WallData
{
    string enduringWallName;                                //this is component to construct variable key such as "fOuterWallHealth" <= "f" + enduringWall + "Health"
    public string GetCodeName() => enduringWallName;

    [SerializeField] string outerWallName;
    [SerializeField] string middlewallName;
    [SerializeField] string innerWallName;

    public WallData(string wallName) { enduringWallName = wallName; }

    public float GetHealth() => VariableSingleton.GetFloatVariable("f" + enduringWallName + "health");

    public string GetDisplayName() 
    { 
        switch (enduringWallName)
        {
            case "OuterWall":
                return outerWallName;

            case "MiddleWall":
                return middlewallName;

            case "InnerWall":
                return innerWallName;
        }
        Debug.Log("Exception invalid enduringWall = " + enduringWallName);
        return enduringWallName; 
    }

    public void Abandon() 
    {
        if (enduringWallName == "OuterWall") enduringWallName = "MiddleWall";
        else if (enduringWallName == "MiddleWall") enduringWallName = "InnerWall";
    }
}
