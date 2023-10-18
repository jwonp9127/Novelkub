using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public DialogManager DialogManager { get; private set; }
    public QuestManager QuestManager { get; private set; }
    
    [Header("DialogUI")]
    public GameObject dialogUI;
	public GameObject nameUI;
	public TMP_Text dialogName;
    public TMP_Text dialogText;

	[Header("QuestInfoUI")]
    public GameObject questInfoUI;
    public TMP_Text questNameText;
    public TMP_Text questInfoText;
    
    [Header("Etc")]
    public GameObject scanObject;
    public string dialogObject;
    public bool isAction;
    public int dialogIndex;
    
    private void Awake()
    {
        DialogManager = GetComponent<DialogManager>();
        QuestManager = GetComponent<QuestManager>();
    }

    private void Start()
    {
        Debug.Log(QuestManager.CheckQuest());
        questInfoUI.SetActive(true);
        dialogUI.SetActive(false);
        ShowQuestInfo("탐정의 등장", "밥 경관과의 대화를 통해 상황파악 하기");
    }

    public void Interaction(GameObject obj)
    {
        scanObject = obj;
        ObjectData scanObjectData = scanObject.GetComponent<ObjectData>();
        Dialog(scanObjectData.objectId, scanObjectData.isNPC);
    }

    private void Dialog(int objectId, bool isNPC)
    {
        int questDialogIndex = QuestManager.GetQuestDialogIndex();
        string dialogData = DialogManager.GetDialog(objectId + questDialogIndex, dialogIndex, out dialogObject);

        if (dialogData == null)
        {
            QuestManager.CheckQuest(objectId);
            ShowQuestInfo(QuestManager.QuestName, QuestManager.QuestInfo);
            ExitDialog(out dialogIndex);
            return;
        }
        
        if (isNPC)
        {
            dialogText.text = dialogData;

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

    private void OnDialog()
    {
        isAction = true;
        dialogUI.SetActive(true);
        ShowDialogName(dialogObject);
    }

    public void ExitDialog(out int index)
    {
        isAction = false;
        dialogUI.SetActive(false);
        index = 0;
    }

    private void ShowQuestInfo(string qName, string qInfo)
    {
        questNameText.text = qName;
        questInfoText.text = qInfo;
    }

    private void ShowDialogName(string dName)
    {
        if (dName == "player")
        {
            nameUI.SetActive(false);
        }
        else
        {
            nameUI.SetActive(true);
        }
        dialogName.text = dName;
    }
}
