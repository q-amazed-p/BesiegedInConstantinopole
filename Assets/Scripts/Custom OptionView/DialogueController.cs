using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueController : MonoBehaviour
{
    string startNode;
    public void SetStartNode(string nodeName) => startNode = nodeName;

    [SerializeField] DialogueRunner dialogueRunner;

    private void OnEnable()
    {
        dialogueRunner.StartDialogue(startNode);
    }

}
