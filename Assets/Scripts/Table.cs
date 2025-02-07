using Assets.Scripts;
using UnityEngine;
public class Table : MonoBehaviour
{
    public Transform TopPoint;
    public bool IsEmpty;
    public Item CurrentItem;

    private void Start()
    {
        IsEmpty = true;
    }
}
