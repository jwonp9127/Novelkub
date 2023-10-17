using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestNum
{
    First = 100,
    Second = 200,
    Third = 300
}
public class QuestData
{
    public string QuestName;
    public string[] QuestInfo;
    public int[] NpcId;

    public QuestData(string name, string[] info, int[] npc)
    {
        QuestName = name;
        QuestInfo = info;
        NpcId = npc;
    }
}
