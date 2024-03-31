using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OttomanDecision : MonoBehaviour
{
    [Serializable]
    class possibility
    {
        [SerializeField] float _chance;
        public float Chance => _chance;

        [SerializeField]
        string[] restrictions;
        //Space for conditions to validate before choice

        [SerializeField]
        UnityEvent ottomanAction;

        public bool IsRestricted() 
        {
            if (restrictions.Length > 0)
            {
                foreach (string restriction in restrictions) 
                {
                    if (VariableSingleton.GetBoolVariable(restriction)) return true;
                }
            }
            return false;
        }
    }

    [SerializeField]
    List<possibility> tacticalOptions;

    
    public void EliminateRedundant() 
    { 
        foreach (possibility option in tacticalOptions) 
        { 
            if(option.IsRestricted()) tacticalOptions.Remove(option);
        }
    }

    public int ExecuteRandomAction() 
    {
        float totalChance = 0;
        foreach (possibility option in tacticalOptions) { totalChance += option.Chance; }

        float[] cumulativeChance = new float[tacticalOptions.Count];
        cumulativeChance[0] = tacticalOptions[0].Chance;
        for(int i = 1; i < tacticalOptions.Count; i++)
        {
            cumulativeChance[1] = tacticalOptions[i].Chance + cumulativeChance[i - 1];
        }

        float randomFloatWithinChance = UnityEngine.Random.value * totalChance;
        
        int selection = 0;
        for(int i = 0; i < cumulativeChance.Length; i++) 
        { 
            if(randomFloatWithinChance < cumulativeChance[i])
            {
                selection = i;
                break;
            }
        }

        return selection;

        //possibleActions[selection].Invoke();
    }

    //DEBUG
    public int DebugNumberOfRolls;

    [ContextMenu("FeignExecuteAction")]

    public void FeignExecuteAction() 
    {

        Debug.Log(ExecuteRandomAction());
    }


    [ContextMenu("FeignMulipleActions")]
    public void FeignMulipleActions()
    {
        int[] results = new int[tacticalOptions.Count];
        for (int i = DebugNumberOfRolls; i>0; i--) 
        {
            results[ExecuteRandomAction()]++;
        }

        for(int i = 0; i < results.Length; i++) 
        {
            Debug.Log("Action " + i + " feigned: " + results[i] + " times");
        } 
    }

}
