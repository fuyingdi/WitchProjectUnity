using UnityEngine;

public interface IItemHolder
{
    public void OnPutItem(Item item)
    {
        CurrentItem = item;
    }
    public Item OnTookItem();

    public Transform TopPoint { get;}
    public bool IsEmpty { get => CurrentItem == null; }
    public Item CurrentItem { get; set; }

}