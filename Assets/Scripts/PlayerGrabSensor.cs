using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabSensor : PlayerSensor
{
    public string TagFilter;

    [ReadOnly] public List<Item> CurrentHold;

    public override bool HasTarget => CurrentHold.Count > 0;

    public Item Target => CurrentHold[0];

    private void Start()
    {
        CurrentHold = new List<Item>();
    }

    private void Update()
    {
        CurrentHold.RemoveAll(item => item == null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(TagFilter.ToString()))
        {
            return;
        }
        else
        {
            CurrentHold.Add(other.gameObject.GetComponent<Item>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (TagFilter != null)
        {
            if (!other.CompareTag(TagFilter.ToString()))
            {
                return;
            }
            else
            {
                CurrentHold.Remove(other.gameObject.GetComponent<Item>());
            }
        }
    }
}

public class PlayerSensor : MonoBehaviour
{
    public virtual bool HasTarget { get; }

}