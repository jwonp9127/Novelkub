using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private QuestManager _questManager;
    private Dictionary<int, string[]> _dialogData;
    private ObjectNum _objectNum;

    private void Awake()
    {
        _questManager = GetComponent<QuestManager>();
        _dialogData = new Dictionary<int, string[]>();
        GenerateDialogData();
        GenerateQuestDialogData();
    }

    private void GenerateDialogData()
    {
        _dialogData.Add((int)ObjectNum.NPC1, new string[]{"나는 NPC1이야",
                                                          "너는 NPC1에게 말을 걸고 있어"});
        _dialogData.Add((int)ObjectNum.NPC2, new string[]{"나는 NPC2야",
                                                          "너는 NPC2에게 말을 걸고 있어"});
        _dialogData.Add((int)ObjectNum.Evidence1, new string[]{"나는 Evidence1이야"});
    }

    private void GenerateQuestDialogData()
    {
        //해당 obj + quest + order
        _dialogData.Add((int)ObjectNum.NPC1 + (int)QuestNum.First,
                        new string[] { "어서 와.",              
                                       "이건 첫 번째 퀘스트야.",
                                       "두 번째 NPC에게 말을 걸어봐."});
        _dialogData.Add((int)ObjectNum.NPC2 + (int)QuestNum.First + 1,
                        new string[] { "왔어?.",              
                                       "맞아, 이건 첫 번째 퀘스트야.",
                                       "첫 번째 퀘스트가 끝이 났어."});
    }

    public string GetDialog(int objectId, int dialogIndex)
    {
        if (!_dialogData.ContainsKey(objectId))
        {
            if (!_dialogData.ContainsKey(objectId - objectId % 100))
            {
                return GetDialog(objectId - objectId % 1000, dialogIndex);
            }
            else
            {
                return GetDialog(objectId - objectId % 100, dialogIndex);
            }
        }
        if (dialogIndex == _dialogData[objectId].Length)
        {
            return null;
        }
        else
        {
            return _dialogData[objectId][dialogIndex];
        }
    }
}
