using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;

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
    }

    public int GetQuestDialogIndex(int id)
    {
        return questId;
    }
}
