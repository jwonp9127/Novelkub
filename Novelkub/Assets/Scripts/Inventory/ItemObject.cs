using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}
public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", item.displayName);
    }

    public void OnInteract()
    {
        Debug.Log("���Ծ���");
        if(!gameObject.CompareTag("Trash"))
        {
			Inventory.instance.AddItem(item);
		}
        Destroy(gameObject);
    }
}