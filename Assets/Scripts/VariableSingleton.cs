using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Yarn.Unity;

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
        outerWallH = 0.8f;
        transform.GetComponentInChildren<LoaderScript>().RunLoader();
    }



    static DateTime siegeStart = new(1453, 4, 6);

    int turn = 0;
    public int Turn
    {
        get => turn;
        private set => turn = value;
    }

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

    float outerWallH;
    float midWallH;
    float innerWallH;

    public float GetWallHealth()
    {
        return outerWallH;
    }

    [YarnCommand("ChangeWall")]
    public float ChangeWallHealth(int layer, float percBrick)
    {
        float brick = 0.01f * percBrick;

        switch (layer)
        {
            case 0:
                {
                    outerWallH += brick;
                    if (outerWallH > 1) { outerWallH = 1; }

                    else { if (outerWallH < 0) { outerWallH = 0; } }

                    return outerWallH;
                }

            case 1:
                {
                    midWallH += brick;
                    if (midWallH > 1) { midWallH = 1; }

                    else { if (midWallH < 0) { midWallH = 0; } }

                    return midWallH;
                }

            case 2:
                {
                    innerWallH += brick;
                    if (innerWallH > 1) { innerWallH = 1; }

                    else { if (innerWallH < 0) { innerWallH = 0; } }

                    return innerWallH;
                }
        }
        return -1;
    }

    [YarnCommand("ReportWall")]
    public int ReportWall(int layer)
    {
        switch (layer)
        {
            case 0:
                return Mathf.RoundToInt(100 * outerWallH);

            case 1:
                return Mathf.RoundToInt(100 * midWallH);

            case 2:
                return Mathf.RoundToInt(100 * innerWallH);
        }

        return -1;
    }



    //TROOPS

    int infantry;
    int archers;
    int cavalry;

    [YarnCommand("ReportTroops")]
    public int ReportTroops(int type)
    {
        switch (type)
        {
            case 0:
                return infantry;

            case 1:
                return archers;

            case 2:
                return cavalry;
        }

        return -1;
    }

    [YarnCommand("ChangeTroops")]
    public int ChangeTroops(int type, int delta)
    {
        switch (type)
        {
            case 0:
                if (infantry + delta < 0)
                {
                    infantry = 0;
                }
                else
                {
                    infantry += delta;
                }
                return infantry;

            case 1:
                if (archers + delta < 0)
                {
                    archers = 0;
                }
                else
                {
                    archers += delta;
                }
                return archers;

            case 2:
                if (cavalry + delta < 0)
                {
                    cavalry = 0;
                }
                else
                {
                    cavalry += delta;
                }
                return cavalry;
        }

        return -1;
    }

    //STORY POINT LISTS

    List<int> possibleStory = new List<int>() {0, 1, 2, 3};
    List<int> possibleRandom = new List<int>();

    public bool StoryScheduled()
    {
        return possibleStory.Contains(turn);
    }

    public int RandomEventID()
    {
        return possibleRandom[UnityEngine.Random.Range(0, possibleRandom.Count)];
    }

    //SAVE SERVICE
    public string Save()
    {
        string saveCode;

        saveCode = turn.ToString();
        saveCode += " ";
        saveCode += outerWallH.ToString();

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
        outerWallH = float.Parse(saveBreakdown[1]);
        Debug.Log(outerWallH.ToString());
    }
}
