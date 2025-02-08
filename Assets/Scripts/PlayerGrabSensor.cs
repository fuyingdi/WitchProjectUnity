using Assets.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabSensor : MonoBehaviour
{
    public string TagFilter;

    public Item CurrentChoose;

    [ReadOnly] public List<Item> CurrentHold;

    private void Start()
    {
        CurrentHold = new List<Item>();
    }

    private void Update()
    {
        
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