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
    static public string GetPercentFloat(string varName)
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
        /*foreach(bool b in bDict)
        {
            saveCode += b;
            saveCode += " ";
        }*/


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
        //outerWallH = float.Parse(saveBreakdown[1]);
    }

    private void Awake()
    {
        _instance = this;
        iDict.Add("cavalry", 2);
        iDict.Add("money", 100);
        fDict.Add("wall_outer", 0.8f);
        fDict.Add("people_rep", 2);
        fDict.Add("noble_rep", 2);
    }

    private void Start()
    {
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
