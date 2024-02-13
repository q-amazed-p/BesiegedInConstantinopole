using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionResults : MonoBehaviour
{
    private static ActionResults _instance;
    public static ActionResults Instance => _instance;


    [SerializeField] private List<string> actionText;
    /**Action indices:
     * 0 - repair wall
     * 1 - recruit troops
     * 2 - upgrade troops
     */
    private int _lastAction;
    public int LastAction { set => _lastAction = value; }

    public string GetActionText()
    {
        return actionText[_lastAction];
    }
    

    private void Awake()
    {
        _instance = this;
    }

}
