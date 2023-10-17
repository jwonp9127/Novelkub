using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;

    public string QuestName { get; private set; }
    public string QuestInfo { get; private set; }
    
    private Dictionary<int, QuestData> _questDate;

    private void Awake()
    {
        _questDate = new Dictionary<int, QuestData>();
        GenerateData();
    }

    private void Start()
    {
        QuestName = CheckQuest();
    }

    private void GenerateData()
    {
        _questDate.Add((int)QuestNum.First,
            new QuestData("모험의 시작", new string[] { "NPC2를 찾아가세요.", "다음 퀘스트를 진행해주세요." },
                new int[] { (int)ObjectNum.NPC1, (int)ObjectNum.NPC2 }));
        _questDate.Add((int)QuestNum.Second,
            new QuestData("끝났당", new string[] { "" }, new int[] { 0 }));
    }

    public int GetQuestDialogIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == _questDate[questId].NpcId[questActionIndex])
        {
            QuestInfo = GetQuestInfo();
            questActionIndex++;
        }
        if (questActionIndex == _questDate[questId].NpcId.Length)
        {
            NextQuest();
            QuestName = CheckQuest();
        }
        return _questDate[questId].QuestName;
    }
    
    public string CheckQuest()
    {
        return _questDate[questId].QuestName;
    }

    private void NextQuest()
    {
        questId += 100;
        questActionIndex = 0;
    }

    private string GetQuestInfo()
    {
        return _questDate[questId].QuestInfo[questActionIndex];
    }
}
