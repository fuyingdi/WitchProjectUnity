using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTableSensor : MonoBehaviour
{
    public string TagFilter;

    [ReadOnly] public List<IItemHolder> CurrentHold;

    private void Start()
    {
        CurrentHold = new List<IItemHolder>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(TagFilter.ToString()))
        {
            return;
        }
        else
        {
            var i = other.GetComponent<IItemHolder>();
            if (i != null)
            {
                CurrentHold.Add(i);
            }
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
                var i = other.GetComponent<IItemHolder>();
                if (i != null)
                {
                    CurrentHold.Remove(i);
                }
            }
        }
    }
}