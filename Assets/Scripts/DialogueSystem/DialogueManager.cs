using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour {
    [SerializeField] private RectTransform dialogueBox;
    [SerializeField] private Image characterPhoto;
    [SerializeField] private TextMeshProUGUI dialogueArea;
    [SerializeField] private GameObject levelManger;

    // Each scene will contain its own csv file with they're own dialogues
    public TextAsset csvFile;
    public List<DialogueCharacterScripting> characters;
    public List<DialogueLine> dialogueData;
    public string currentLanguage = "ES";
    public UnityEvent DialoguesReady;

    public static DialogueManager Instance { get; private set; }
    
    private Queue<DialogueLine> currentDialogueQueue;
    private DialogueCharacterScripting currentCharacter;

    private void Start() {
        LoadDialogueData();
    }

    private void Awake() {
        Instance = this;
        HideDialogueBox();
    }

    public void StartDialogue(int initialID, int finalID) {
        currentDialogueQueue = new Queue<DialogueLine>();
        // Select lines for the dialogue
        foreach (DialogueLine line in dialogueData) {
            if (line.id > finalID) break;  
            if (line.id >=  initialID) currentDialogueQueue.Enqueue(line);
        }
        StartCoroutine(DialogueCoroutine());
    }

    private IEnumerator DialogueCoroutine() {
        ShowDialogueBox();
        while (currentDialogueQueue.Count > 0) {
            var currentTurn = currentDialogueQueue.Dequeue();
            // Update Character
            currentCharacter = FindCharacterByName(currentTurn.characterName);
            SetCharacterInfo(currentCharacter, currentTurn.emotion);
            // Update Dialogue
            ClearDialogueArea();
            dialogueArea.text = currentTurn.texts[currentLanguage];

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return null;
        }
        HideDialogueBox();
    }

    private void LoadDialogueData() {
        dialogueData = CSVImporter.LoadCSV(csvFile);
        DialoguesReady.Invoke();
    }

    public void ShowDialogueBox() {
        dialogueBox.gameObject.SetActive(true);
    }

    public void HideDialogueBox() {
        dialogueBox.gameObject.SetActive(false);
    }

    public void SetCharacterInfo(DialogueCharacterScripting character, string emotion) {
        if (characterPhoto == null) return;
        characterPhoto.sprite = character.GetEmotionSprite(emotion);
    }

    public void ClearDialogueArea() {
        dialogueArea.text = string.Empty;
    }

    DialogueCharacterScripting FindCharacterByName(string name) {
        foreach (var c in characters) {
            if (c != null && c.Name == name)
                
                return c;
        }

        Debug.LogWarning($"Character {name} doesn't found in the list.");
        return null;
    }
}


