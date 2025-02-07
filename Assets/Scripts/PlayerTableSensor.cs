using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTableSensor : MonoBehaviour
{
    public string TagFilter;

    [ReadOnly] public List<GameObject> CurrentHold;

    private void Start()
    {
        CurrentHold = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(TagFilter.ToString()))
        {
            return;
        }
        else
        {
            CurrentHold.Add(other.gameObject);
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
                CurrentHold.Remove(other.gameObject);
            }
        }
    }
}