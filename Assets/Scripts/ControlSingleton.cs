using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlSingleton : MonoBehaviour
{

    private static ControlSingleton _instance;
    public static ControlSingleton Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    // GO BACK functionality

    bool escActive;

    public GameObject returnFrom;
    public GameObject returnTo;

    List<(GameObject from, GameObject to)> backHistory;


    public void LogToEsc(GameObject goingFrom, GameObject goingTo)
    {
        if(returnFrom != null && returnTo != null)
        {
            (GameObject, GameObject) historyLog = (returnFrom, returnTo);
            backHistory.Add(historyLog);
        }

        returnFrom = goingTo;
        returnTo = goingFrom;
        escActive = true;
    }

    public void GoBack()
    {
        returnTo.SetActive(true);
        returnFrom.SetActive(false);

        if(backHistory.Count == 0)
        {
            returnFrom = null;
            returnTo = null;
            escActive = false;
        }
        else
        {
            returnFrom = backHistory[backHistory.Count - 1].from;
            returnTo = backHistory[backHistory.Count - 1].to;
            backHistory.RemoveAt(backHistory.Count - 1);
        }
    }


    // Live Controls

    private void Awake()
    {
        _instance = this;
        backHistory = new List<(GameObject, GameObject)>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escActive)
            {
                GoBack();
            }

            else
            {
                VariableSingleton.Instance.Save();
                SceneManager.LoadScene(0);
            }
        }
    }

}
