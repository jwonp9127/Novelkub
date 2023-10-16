using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    // public GameObject[] questObject; 

    private Dictionary<int, QuestData> _questDate;

    private void Awake()
    {
        _questDate = new Dictionary<int, QuestData>();
        GenerateData();
    }

    private void GenerateData()
    {
        _questDate.Add((int)QuestNum.First,
                        new QuestData("모험의 시작", new int[]{(int)ObjectNum.NPC1, (int)ObjectNum.NPC2}));
        _questDate.Add((int)QuestNum.Second,
            new QuestData("끝났당", new int[]{0}));
    }

    public int GetQuestDialogIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == _questDate[questId].NpcId[questActionIndex])
        {
            questActionIndex++;
        }

        if (questActionIndex == _questDate[questId].NpcId.Length)
        {
            NextQuest();
        }
        return _questDate[questId].QuestName;
    }
    
    public string CheckQuest()
    {
        return _questDate[questId].QuestName;
    }

    public void NextQuest()
    {
        questId += 100;
        questActionIndex = 0;
    }
}
