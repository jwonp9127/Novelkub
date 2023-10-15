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
    public int[] NpcId;

    public QuestData(string name, int[] npc)
    {
        QuestName = name;
        NpcId = npc;
    }
}
