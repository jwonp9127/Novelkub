using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InteractionManager : MonoBehaviour
{
    public DialogManager DialogManager { get; private set; }
    public QuestManager QuestManager { get; private set; }

    [Header("DialogUI")]
    public GameObject dialogUI;
    public GameObject nameUI;
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
        ShowQuestInfo("모험의 시작", "경찰에게 찾아가세요.");
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
        Debug.Log(objectId + questDialogIndex);
        Debug.Log(QuestManager.questActionIndex);
        Debug.Log(dialogIndex);
        CheckAddItem(objectId + questDialogIndex, QuestManager.questActionIndex, dialogIndex); //아마 이거지 않을까???? 추가
        if (dialogData == null)
        {

            QuestManager.CheckQuest(objectId);
            ShowQuestInfo(QuestManager.QuestName, QuestManager.QuestInfo);
            ExitDialog(out dialogIndex);
            Debug.Log(QuestManager.CheckQuest(objectId));
            return;
        }

        if (isNPC)
        {
            dialogText.text = dialogData;
          //  CheckAddItem(objectId + questDialogIndex, QuestManager.questActionIndex, dialogIndex); //아마 이거지 않을까???? 
            nameUI.SetActive(true);
        }
        else
        {
            dialogText.text = dialogData;

            nameUI.SetActive(false);
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
    public void CheckAddItem(int Questid, int npcIdex, int talkIndex)
    {
        for (int i = 0; i < 2; i++)
        {
            if (Questid == DialogManager._QuestItem[i, 0] && npcIdex == DialogManager._QuestItem[i, 1] && talkIndex == DialogManager._QuestItem[i, 2])
            {
                Inventory.instance.AddItem(DialogManager.QuestItemDatas[DialogManager._QuestItem[i, 3]]);
                Debug.Log(DialogManager._QuestItem[i, 3] + "ADD인벤토리하기");
            }
            //if (Questid == DialogManager._QuestItem[i, 0])
            //{
            //    if (npcIdex == DialogManager._QuestItem[i, 1])
            //    {
            //        if (talkIndex == DialogManager._QuestItem[i, 2])
            //        {
            //            Inventory.instance.AddItem(DialogManager.QuestItemDatas[DialogManager._QuestItem[i, 3]]);
            //            Debug.Log(DialogManager._QuestItem[i, 3] + "ADD인벤토리하기");
            //        }
            //    }
            //}
        }

        
    }
}
