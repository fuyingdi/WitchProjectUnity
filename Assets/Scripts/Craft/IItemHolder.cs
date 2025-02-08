using Assets.Scripts;

public interface IItemHolder
{
    public void OnPutItem(Item item);
    public Item OnTookItem();
}