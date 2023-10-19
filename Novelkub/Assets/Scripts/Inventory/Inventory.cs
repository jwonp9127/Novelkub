using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;


    public GameObject inventoryInfo;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatNames;
    public TextMeshProUGUI selectedItemStatValues;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private int curEquipIndex;




    public static Inventory instance;


    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        slots = new ItemSlot[uiSlots.Length]; // 슬롯을 담는 배열 생성

        for (int i = 0; i < slots.Length; i++)  //각 슬롯에 번호를 붙여주고 
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();  //슬롯 클리어
        }
    }



    public void AddItem(ItemData item)
    {
        slots[item.itemNum].item = item;
        //ItemSlot slotToStackTo = slots[item.itemNum]; //바로 고유번호 붙이기
        UpdateUI(item.itemNum);
    }

    void UpdateUI(int itemNum) //고유번호의 아이템만 업로드 하도록
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[itemNum].item != null)
                uiSlots[itemNum].Set(slots[itemNum]);
            else
                uiSlots[itemNum].Clear();
        }
    }


    public void SelectItem(int index)
    {
        if (slots[index].item == null)
            return;
       // inventoryInfo.SetActive(true); //일단 임시로 적어둠
        selectedItem = slots[index];
        OnUseButton();
        //selectedItemIndex = index;

        //selectedItemName.text = selectedItem.item.displayName;
        //selectedItemDescription.text = selectedItem.item.description;

        //selectedItemStatNames.text = string.Empty;
        //selectedItemStatValues.text = string.Empty;

        //for (int i = 0; i < selectedItem.item.consumables.Length; i++)
        //{
        //    selectedItemStatNames.text += selectedItem.item.consumables[i].type.ToString() + "\n";
        //    selectedItemStatValues.text += selectedItem.item.consumables[i].value.ToString() + "\n";
        //}
        //useButton.SetActive(true);

        //dropButton.SetActive(true);
    }

    private void ClearSeletecItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;
        inventoryInfo.SetActive(false);
    }

    public void OnUseButton()
    {

        switch (selectedItem.item.itemNum)
        {
            case 0:
                Debug.Log("1번슬룻 아이템 사용");
                break;
            case 1:
                Debug.Log("2번슬룻 아이템 사용");
                break;
            case 2:
                Debug.Log("3번슬룻 아이템 사용");
                break;
            case 3:
                Debug.Log("4번슬룻 아이템 사용");
                break;
            case 4:
                Debug.Log("5번슬룻 아이템 사용");
                break;
            case 5:
                Debug.Log("6번슬룻 아이템 사용");
                break;
            case 6:
                Debug.Log("7번슬룻 아이템 사용");
                break;
            case 7:
                Debug.Log("8번슬룻 아이템 사용");
                break;


        }

       // RemoveSelectedItem(selectedItem.item.itemNum);
    }

    public void OnEquipButton()
    {

    }

    void UnEquip(int index)
    {

    }

    public void OnUnEquipButton()
    {

    }


    private void RemoveSelectedItem(int itemNum)
    {
        //slots[itemNum].item.icon = GetComponent<image>();
        // selectedItem.quantity--;                 
        selectedItem.item = null;
        //  slots[itemNum].item.icon
        ClearSeletecItemWindow();
        //텍스트를 하나 넣어서 사용이라고 뜨게하기

        UpdateUI(itemNum);
    }

    public void RemoveItem(ItemData item)
    {

    }

    public bool HasItems(ItemData item, int quantity)
    {
        return false;
    }


}