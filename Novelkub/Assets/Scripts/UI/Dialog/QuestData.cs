public enum QuestNum
{
    First = 100,
    Second = 200,
    Third = 300,
    Forth = 400,
    Fifth = 500,
    Sixth = 600,
    Seventh = 700
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
