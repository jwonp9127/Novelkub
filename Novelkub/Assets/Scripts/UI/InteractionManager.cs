using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public DialogManager DialogManager { get; private set; }
    public GameObject dialogUI;
    public TMP_Text objectName;
    public TMP_Text dialogText;
    public GameObject scanObject;
    public bool isAction;
    public int dialogIndex;

    private void Awake()
    {
        DialogManager = GetComponent<DialogManager>();
    }

    public void Interaction(GameObject obj)
    {
        scanObject = obj;
        objectName.text = scanObject.name;
        InteractableObjectData scanObjectData = scanObject.GetComponent<InteractableObjectData>();
        Dialog(scanObjectData.objectId, scanObjectData.isNPC);
    }

    public void Dialog(int objectId, bool isNPC)
    {
        string dialogData = DialogManager.GetDialog(objectId, dialogIndex);

        if (dialogData == null)
        {
            ExitDialog(out dialogIndex);
            return;
        }
        
        if (isNPC)
        {
            dialogText.text = dialogData;
        }
        else
        {
            dialogText.text = dialogData;
        }
        EnterDialog();
        dialogIndex++;
    }

    public void EnterDialog()
    {
        isAction = true;
        dialogUI.SetActive(true);
    }

    public void ExitDialog(out int index)
    {
        isAction = false;
        dialogUI.SetActive(false);
        index = 0;
    }
}
