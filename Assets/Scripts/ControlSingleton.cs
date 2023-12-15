using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlSingleton : MonoBehaviour
{
    bool escActive;

    GameObject returnFrom;
    GameObject returnTo;

    private static ControlSingleton _instance;
    public static ControlSingleton Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if(escActive && Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
    }

    public void LogToEsc(GameObject goingFrom, GameObject goingTo)
    {
        returnFrom = goingTo;
        returnTo = goingFrom;
        escActive = true;
    }

    public void GoBack()
    {
        returnTo.SetActive(true);
        returnFrom.SetActive(false);
        escActive = false;
    }

}
