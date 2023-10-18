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
public class QuestItem
{
    //public ItemData[] QuestItemDatas = new ItemData[8];
    int NpcIndex;
    int TalkIndex;
    ItemData QuestItemData;

    public QuestItem(int npcIndex, int talkIndex, ItemData item)
    {
        NpcIndex = npcIndex;
        TalkIndex = talkIndex;
        QuestItemData = item;
    }

    
}
