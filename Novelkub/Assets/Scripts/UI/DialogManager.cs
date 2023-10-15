using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Dictionary<int, string[]> _dialogData;
    private ObjectData _objectData;

    private void Awake()
    {
        _dialogData = new Dictionary<int, string[]>();
        GenerateData();
    }

    private void GenerateData()
    {
        _dialogData.Add((int)ObjectData.NPC1, new string[]{"나는 NPC1이야", "너는 NPC1에게 말을 걸고 있어"});
        _dialogData.Add((int)ObjectData.NPC2, new string[]{"나는 NPC2야", "너는 NPC2에게 말을 걸고 있어"});
        _dialogData.Add((int)ObjectData.Evidence1, new string[]{"나는 Evidence1이야"});
    }

    public string GetDialog(int objectId, int dialogIndex)
    {
        if (dialogIndex >= _dialogData[objectId].Length)
        {
            return null;
        }
        return _dialogData[objectId][dialogIndex];
    }
}
