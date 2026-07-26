using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public List<DialogueNodes> dialogue;
}
[System.Serializable]
public class DialogueNodes
{
    public enum DialogueNodeType
    {
        node_Line,
        node_UI,
        node_End
    }
    public DialogueNodeType dialogueNodeType;
    public string talker;
    [TextArea(3, 10)]
    public string conversation;
    public string audioID;
}