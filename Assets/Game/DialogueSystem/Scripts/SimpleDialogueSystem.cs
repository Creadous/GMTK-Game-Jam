using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class SimpleDialogueSystem : MonoBehaviour
{
    public static SimpleDialogueSystem instance;

    [Header("management")]
    public int dialogueIndex;
    public Dialogue dialogueReff;
    public InteractableObject_NPC talkerReff;

    [Header("UI")]
    public TMP_Text talkerName;
    public TMP_Text dialogueText;

    [Header("Animation")]
    [SerializeField] private PlayableDirector director;
    [SerializeField] private PlayableAsset intro;
    [SerializeField] private PlayableAsset outro;
    private bool animationInProgress;

    public void Awake()
    {
        instance = this;
        animationInProgress = false;
    }

    public void SetUp(Dialogue dialogue, InteractableObject_NPC talker)
    {
        dialogueReff = dialogue;
        dialogueIndex = 0;
        talkerReff = talker;
        StartCoroutine(IntroSequence());
        GameAudioManager.instance.PlayTownTrack();
        
    }
    public void UpdateDialogueWindow()
    {
        switch (dialogueReff.dialogue[dialogueIndex].dialogueNodeType)
        {
            case DialogueNodes.DialogueNodeType.node_Line:
                talkerName.text = dialogueReff.dialogue[dialogueIndex].talker;
                dialogueText.text = dialogueReff.dialogue[dialogueIndex].conversation;
                PlayAudio(dialogueReff.dialogue[dialogueIndex].audioID);
                break;
            case DialogueNodes.DialogueNodeType.node_UI:
                GameController.instance.LaunchShopMenu(talkerReff);
                PlayAudio(dialogueReff.dialogue[dialogueIndex].audioID);
                break;
            case DialogueNodes.DialogueNodeType.node_End:
                StartCoroutine(EndSequence());
                break;
        }
    }
    public void PlayAudio(string audioID)
    {
        if(string.IsNullOrWhiteSpace(audioID) == false)
        {
            GameAudioManager.instance.PlaySoundEffect(audioID);
        }
    }
    public void Update()
    {
        if (animationInProgress) return;
        if (dialogueReff.dialogue[dialogueIndex].dialogueNodeType == DialogueNodes.DialogueNodeType.node_End) return;
        if (dialogueReff.dialogue[dialogueIndex].dialogueNodeType == DialogueNodes.DialogueNodeType.node_UI) return;

        HandleInput();
    }
    public void HandleInput()
    {
        if (InputManager.instance.AcceptInputRequested())
        {
            NextNode();
        }
    }
    public void NextNode()
    {
        dialogueIndex++;
        UpdateDialogueWindow();
    }
    public IEnumerator IntroSequence()
    {
        animationInProgress = true;
        director.Play(intro);
        talkerName.text = dialogueReff.dialogue[dialogueIndex].talker;

        while (animationInProgress)
        {
           yield return new WaitForFixedUpdate();
        }

        UpdateDialogueWindow();

        yield return null;
    }
    public IEnumerator EndSequence()
    {
        animationInProgress = true;
        director.Play(outro);
        while (animationInProgress)
        {
            yield return new WaitForFixedUpdate();
        }
        GameController.instance.CloseDialogueSystem();
        GameAudioManager.instance.PlayDungeonTrack();
        yield return null;
    }
    public void FinisheAnimation()
    {
        animationInProgress = false;
    }
}

