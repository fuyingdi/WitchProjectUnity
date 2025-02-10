using UnityEditor;
using UnityEngine;

public interface IItemHolder
{
    public Transform TopPoint { get; }
    public bool IsEmpty { get => CurrentItem == null; }
    public Item CurrentItem { get; set; }

    // -------

    public void PutItemOn(Item item)
    {
        CurrentItem = item;
        CurrentItem.transform.position = TopPoint.transform.position;
        CurrentItem.isOnTable = true;

        CurrentItem.OnPutOrCatch();
    }

    public Item TookItem()
    {
        if (CurrentItem != null)
        {
            var res = CurrentItem;
            CurrentItem.isOnTable = false;
            CurrentItem = null;

            return res;
        }
        else throw new System.Exception("Take Item From Empty Table!!!");
    }

    public void SpawnItem(Item item)
    {
        if (!IsEmpty)
        {
            throw new System.Exception("Spawn Item on Not Empty Table!!!");
        }
        else
        {
            CurrentItem = item;
            item.transform.position = TopPoint.transform.position;
            item.isOnTable = true;
            item.OnPutOrCatch();
        }
    }

    public void ClearItem()
    {
        if (IsEmpty)
        {
            throw new System.Exception("Clear Item From Empty Table !!!");
        }

        else
        {
            Object.Destroy(CurrentItem.gameObject);
            CurrentItem = null;

        }
    }

}