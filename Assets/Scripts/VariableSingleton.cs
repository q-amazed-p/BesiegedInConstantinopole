using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Yarn.Unity;
using Unity.Collections.LowLevel.Unsafe;

public class VariableSingleton : MonoBehaviour
{
    static private VariableSingleton _instance;

    static public VariableSingleton Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    static Dictionary<string, bool> bDict = new Dictionary<string, bool>();
    static Dictionary<string, int> iDict = new Dictionary<string, int>();
    static Dictionary<string, float> fDict = new Dictionary<string, float>();


    static DateTime siegeStart = new(1453, 4, 6);

    static int turn = 0;
    static public int Turn
    {
        get => turn;
        private set => turn = value;
    }

    static public int IncrementTurn()
    {
        turn++;
        return turn;
    }

    static public DateTime GetDate()
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

    //VARIABLE ACCESS

    [YarnFunction("GetBool")]
    static public bool GetBoolVariable(string varName)
    {
        return bDict[varName];
    }


    [YarnFunction("GetInt")]
    static public int GetIntVariable(string varName)
    {
        return iDict[varName];
    }


    [YarnFunction("GetFloat")]
    static public float GetFloatVariable(string varName)
    {
        return fDict[varName];
    }

    [YarnFunction("GetPercent")]
    static public string GetPercentFLoat(string varName)
    {
        return Mathf.RoundToInt(100 * fDict[varName]).ToString() + "%";
    }

    //VARIABLE MODIFICATION

    [YarnCommand("ChangeBool")]
    static public void ChangeBool(string varName, bool newValue)
    {
        bDict[varName] = newValue;
    }

    [YarnCommand("ChangeInt")]
    static public void ChangeInt(string varName, int delta)
    {
        if (iDict[varName] + delta < 0)
        {
            iDict[varName] = 0;
        }
        else
        {
            iDict[varName] += delta;
        }
    }

    [YarnCommand("RndChangeInt")]
    static public void ChangeInt(string varName, int min, int max)
    {
        ChangeInt(varName, UnityEngine.Random.Range(min, max + 1));
    }

    [YarnCommand("ChangeFloat")]
    static public void ChangeFloat(string varName, float delta)
    {
        if (fDict[varName] + delta < 0)
        {
            fDict[varName] = 0;
        }
        else
        {
            fDict[varName] += delta;
        }
    }

    [YarnCommand("RndChangeFloat")]
    static public void ChangeFloat(string varName, float min, float max)
    {
        ChangeFloat(varName, UnityEngine.Random.Range(min, max));
    }



    // Wall Condition

    float outerWallH;
    float midWallH;
    float innerWallH;

    public float GetWallHealth()
    {
        return outerWallH;
    }

    public void ChangeValue<T>(string variableName, T value)
    {

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

    [SerializeField] List<int> possibleStory = new List<int>() {0, 1, 2, 3};
    [SerializeField] List<int> possibleRandom = new List<int>();

    public bool StoryScheduled()
    {
        return possibleStory.Contains(turn);
    }

    public int RandomEventID()
    {
        return possibleRandom[UnityEngine.Random.Range(0, possibleRandom.Count)];
    }

    [YarnCommand("AddEvent")]
    public void AddPossibleEvent(string eventList, int eventId)
    {
        switch (eventList) 
        {
            case "story":
                if (!possibleStory.Contains(eventId))
                {
                    possibleStory.Add(eventId);
                }
                break;

            case "random":
                if (!possibleRandom.Contains(eventId))
                {
                    possibleRandom.Add(eventId);
                }
                break;
        }
    }

    [YarnCommand("RemoveEvent")]
    public void RemovePossibleEvent(string eventList, int eventId)
    {
        switch (eventList)
        {
            case "story":
                if (!possibleStory.Contains(eventId))
                {
                    possibleStory.Remove(eventId);
                }
                break;

            case "random":
                if (!possibleRandom.Contains(eventId))
                {
                    possibleRandom.Remove(eventId);
                }
                break;
        }
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

    private void Awake()
    {
        _instance = this;
        iDict.Add("cavalry", 2);
        iDict.Add("money", 100);
        fDict.Add("people_rep", 2);
        fDict.Add("noble_rep", 2);
    }

    private void Start()
    {
        outerWallH = 0.8f;
        transform.GetComponentInChildren<LoaderScript>().RunLoader();
    }

    //DEBUG

    [ContextMenu("PrintVariables")]
    public void PrintVariables()
    {
        foreach (KeyValuePair<string, bool> b in bDict)
        {
            Debug.Log(b.Key + " : " + b.Value);
        }

        foreach (KeyValuePair<string, int> b in iDict)
        {
            Debug.Log(b.Key + " : " + b.Value);
        }

        foreach (KeyValuePair<string, float> b in fDict)
        {
            Debug.Log(b.Key + " : " + b.Value);
        }
    }
}
