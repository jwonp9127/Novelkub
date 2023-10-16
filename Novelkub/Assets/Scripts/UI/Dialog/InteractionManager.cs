using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public DialogManager DialogManager { get; private set; }
    public QuestManager QuestManager { get; private set; }
    
    [Header("DialogUI")]
    public GameObject dialogUI;
    public TMP_Text objectName;
    public TMP_Text dialogText;

    [Header("QuestInfoUI")]
    public GameObject questInfoUI;
    public TMP_Text questNameText;
    public TMP_Text questInfoText;
    
    [Header("Etc")]
    public GameObject scanObject;
    public bool isAction;
    public int dialogIndex;
    
    private void Awake()
    {
        DialogManager = GetComponent<DialogManager>();
        QuestManager = GetComponent<QuestManager>();
    }

    private void Start()
    {
        Debug.Log(QuestManager.CheckQuest());
        questInfoUI.SetActive(true);
        dialogUI.SetActive(false);
        ShowQuestInfo("", "");
    }

    public void Interaction(GameObject obj)
    {
        scanObject = obj;
        objectName.text = scanObject.name;
        ObjectData scanObjectData = scanObject.GetComponent<ObjectData>();
        Dialog(scanObjectData.objectId, scanObjectData.isNPC);
    }

    public void Dialog(int objectId, bool isNPC)
    {
        int questDialogIndex = QuestManager.GetQuestDialogIndex(objectId);
        string dialogData = DialogManager.GetDialog(objectId + questDialogIndex, dialogIndex);

        if (dialogData == null)
        {
            ShowQuestInfo(DialogManager.questName, DialogManager.questInfo);
            QuestManager.CheckQuest(objectId);
            ExitDialog(out dialogIndex);
            Debug.Log(QuestManager.CheckQuest(objectId));
            return;
        }
        
        if (isNPC)
        {
            dialogText.text = dialogData;
        }
        else
        {
            dialogText.text = dialogData;
        }
        OnDialog();
        dialogIndex++;
    }

    public void OnDialog()
    {
        isAction = true;
        dialogUI.SetActive(true);
    }

    public void ExitDialog(out int index)
    {
        isAction = false;
        dialogUI.SetActive(false);
        index = 0;
    }

    public void ShowQuestInfo(string qName, string qInfo)
    {
        questNameText.text = qName;
        questInfoText.text = qInfo;
    }
}
