using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogUI;
    public TMP_Text objectName;
    public TMP_Text dialogText;
    public GameObject scanObject;
    public bool isAction;

    public void Interaction(GameObject obj)
    {
        dialogUI.SetActive(true);
        scanObject = obj;
        objectName.text = scanObject.name;
        dialogText.text = "이것의 이름은 " + scanObject.name + "이라고 한다.";
        isAction = true;
    }
}
