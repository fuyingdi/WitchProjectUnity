using UnityEngine;

public class Table : MonoBehaviour, IItemHolder
{
    public Transform TopPoint { get => _topPoint; set => _topPoint = value; }
    public bool IsEmpty { get => CurrentItem == null; }
    public Item CurrentItem { get; set; }

    [SerializeField]
    private Transform _topPoint;


    public Item OnTookItem()
    {
        throw new System.NotImplementedException();
    }
}
