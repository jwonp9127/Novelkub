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

    [Header("MiniGame")]
    public GameObject MiniGameUI;
    public TMP_Text MiniGameNameText;
    public TMP_Text MiniGameText;

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

    public bool IsMiniGame;

    public static InteractionManager instance;
  
    private void Awake()
    {
        DialogManager = GetComponent<DialogManager>();
        QuestManager = GetComponent<QuestManager>();
        instance = this;
    }

    private void Start()
    {
        MiniGameUI.SetActive(false);
        Debug.Log(QuestManager.CheckQuest());
        questInfoUI.SetActive(true);
        dialogUI.SetActive(false);
        ShowQuestInfo("모험의 시작", "경찰에게 찾아가세요.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FinishMiniGame();
        }
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
        CheckAddItem(objectId + questDialogIndex, QuestManager.questActionIndex, dialogIndex);
        CheckMiniGame(objectId + questDialogIndex, QuestManager.questActionIndex, dialogIndex);
        //FinishMiniGame();
        if (DialogManager.IsMiniGame)
        {
            Debug.Log("이곳은 못가");
            dialogText.text = "퀘스트를 먼저 진행해주세요";
            //ExitDialog(out dialogIndex);
            dialogUI.SetActive(IsMiniGame);
            IsMiniGame =! IsMiniGame;
            return;
        }
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
        }
    }

    public void CheckMiniGame(int Questid, int npcIdex, int talkIndex)
    {
        for (int i = 0; i < 2; i++) //이거 2라고 써있는 것은 바꿔야 한당.
        {
            if (Questid == DialogManager._MiniGame[i, 0] && npcIdex == DialogManager._MiniGame[i, 1] && talkIndex == DialogManager._MiniGame[i, 2])
            {
                DialogManager.IsMiniGame = true;
                switch (i)
                {
                    case (int)QuestMiniGame.First:
                        OnMiniGame("경찰에게 받은 단서 ", "첫 미니게임 내용 첫 미니게임 내용 첫 미니게임 내용 첫 미니게임 내용 첫 미니게임 내용");
                        break;
                    case (int)QuestMiniGame.Second:
                        OnMiniGame("두 번쨰 미니게임", "두번째 미니게임 내용");
                        break;
                    case 2:
                        break;
                }

                //Debug.Log(DialogManager._QuestItem[i, 3] + "ADD인벤토리하기");
            }
           
        }
    }

    public void FinishMiniGame()
    {
        // 각자의 미니게임에서 이메서드를 통해 미니 게임이 끝난 것을 알려주세요
        DialogManager.IsMiniGame = false;
        dialogIndex++;
        OffMiniGame();
    }

    public void OnMiniGame(string mininame, string miniContent)
    {
        questInfoUI.SetActive(false);
        MiniGameUI.SetActive(true);
        MiniGameNameText.text = mininame;
        MiniGameText.text = miniContent;
    }

    public void OffMiniGame()
    {
        questInfoUI.SetActive(true);
        MiniGameUI.SetActive(false);
        MiniGameNameText.text = "";
        MiniGameText.text = "";
    }
}
