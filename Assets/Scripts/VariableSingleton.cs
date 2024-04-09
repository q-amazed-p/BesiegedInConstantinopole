using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Yarn.Unity;
using System.Collections;

public class VariableSingleton : MonoBehaviour
{
    static private VariableSingleton _instance;

    static public VariableSingleton Instance => _instance;

    static Dictionary<string, bool> bDict = new Dictionary<string, bool>();
    static Dictionary<string, int> iDict = new Dictionary<string, int>();
    static Dictionary<string, float> fDict = new Dictionary<string, float>();

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

    static DateTime siegeStart = new(1453, 4, 6);
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

    [SerializeField]
    public WallData EnduringWall = new WallData("OuterWall");

    /*******************
     * VARIABLE ACCESS */

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

    /*************************
     * VARIABLE MODIFICATION */

    [YarnCommand("Change")]
    static public void Change(string varName, float Value)
    {
        switch (varName[0])
        {
            case 'b':
                {
                    ChangeBool(varName, Value!=0 ? true : false);
                    break;
                }

            case 'i':
                {
                    ChangeInt(varName, Mathf.RoundToInt(Value));
                    break;
                }

            case 'f':
                {
                    ChangeFloat(varName, Value);
                    break;
                }
        }
    }

    [YarnCommand("RndChange")]
    static public void RndChange(string varName, float min, float max)
    {
        if (varName[0] == 'i') ChangeInt(varName, (int)min, (int)max);
        else if (varName[0] == 'f') ChangeFloat(varName, min, max);
    }


    static public void ChangeBool(string varName, bool newValue)
    {
        bDict[varName] = newValue;
    }

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

    static public void ChangeInt(string varName, int min, int max)
    {
        ChangeInt(varName, UnityEngine.Random.Range(min, max + 1));
    }

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

    static public void ChangeFloat(string varName, float min, float max)
    {
        ChangeFloat(varName, UnityEngine.Random.Range(min, max));
    }

    /*************************
     *VARIABLE INITIALISATION*/

    class VarCSVParser : IEnumerator
    {
        public string input;
        int position = -1;
        char type;
        public char Type => type;

        (string name, float val) fReadout;
        (string name, int val) iReadout;
        (string name, bool val) bReadout;

        public (string, float) FReadout => fReadout;
        public (string, int) IReadout => iReadout;
        public (string, bool) BReadout => bReadout;

        public VarCSVParser(string init)
        {
            input = init;
        }

        public bool MoveNext()
        {
            if (position > input.Length - 2) return false;
            else
            {
                Debug.Log(position + " " + input.Length);
                string varName = "";
                position++;
                type = input[position];
                Debug.Log(type);

                while (input[position] != ',')
                {
                    varName += input[position];
                    position++;
                }
                Debug.Log(varName);
                position++;

                switch (type)
                {
                    case 'f':
                        {
                        int intComp = 0;
                        float outcome;

                        while (input[position] != '.' && input[position] != '\n' && input[position] != '\r')
                        {
                            intComp = 10 * intComp + AmazUtil.NumFromChar(input[position]);
                            position++;
                        }
                        outcome = intComp;
                        if (input[position] != '\n' && input[position] != '\r') position++;

                        float decimalComp = 0.1f;
                        while (input[position] != '\n' && input[position] != '\r')
                        {
                            outcome += decimalComp * AmazUtil.NumFromChar(input[position]);
                            decimalComp /= 10;
                            position++;
                        }
                        fReadout = (varName, outcome);
                        Debug.Log(outcome);
                        break;
                        }

                    case 'i':
                        {
                        int outcome = 0;
                        bool negative = false;
                        if (input[position] == '-')
                        {
                            negative = true;
                            position++;
                        }
                        while (input[position] != '\n' && input[position] != '\r')
                        {
                            outcome *= 10;
                            outcome += AmazUtil.NumFromChar(input[position]);
                            position++;
                        }
                        if (negative) outcome *= -1;
                        iReadout = (varName, outcome);
                        Debug.Log(outcome);
                        break;
                        }


                    case 'b':
                        {
                        if (input[position] == 'T' || input[position] == 't') bReadout = (varName, true);
                        else bReadout = bReadout = (varName, false);
                        while (input[position] != '\n' && input[position] != '\r')
                        {
                            position++;
                        }
                        Debug.Log(bReadout.val);
                        break;
                        }

                }
                position++;
                return true;
            }
                
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                switch (type)
                {
                    case 'f':
                        return fReadout;

                    case 'i':
                        return iReadout;

                    case 'b':
                        return bReadout;
                }
                return type;
            }
        }

        public object Current
        {
            get
            {
                switch (type)
                {
                    case 'f':
                        return fReadout;

                    case 'i':
                        return iReadout;

                    case 'b':
                        return bReadout;
                }
                return type;
            }
        }

        public bool NotComplete()
        {
            return position < input.Length-1;
        }
    }


    /*********************
     * STORY POINT LISTS */

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


    /****************
     * SAVE SERVICE */
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

    /*********
     * UNITY */

    private void Awake()
    {

    }

    private void Start()
    {
        if (File.Exists("Save"))        //temporary, double file exists check, LoaderScript 
        {                               //should be redesigned to handle all of this
            transform.GetComponentInChildren<LoaderScript>().RunLoader();
        }
        else
        {
            Debug.Log("No save file found.");

            string varCSVFile = File.ReadAllText("Constantinopole_System_Variables.csv");
            for (VarCSVParser csvEnum = new VarCSVParser(varCSVFile); csvEnum.MoveNext();) //csvEnum.NotComplete()
            {
                switch (csvEnum.Type)
                {
                    case 'f':
                        {
                            (string name, float val) readout = ((string, float))csvEnum.Current;
                            fDict.Add(readout.name, readout.val);
                            break;
                        }

                    case 'i':
                        {
                            (string name, int val) readout = ((string, int))csvEnum.Current;
                            iDict.Add(readout.name, readout.val);
                            break;
                        }

                    case 'b':
                        {
                            (string name, bool val) readout = ((string, bool))csvEnum.Current;
                            bDict.Add(readout.name, readout.val);
                            break;
                        }
                }
            }
        }
    }

    /*********
     * DEBUG */

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
