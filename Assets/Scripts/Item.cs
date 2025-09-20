using MoreMountains.Tools;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [ReadOnly] public Rigidbody Rb;
    [ReadOnly] public Collider Collider;
    public bool isOnTable;

    [BoxGroup("加工")] public float ProgressTime;
    [BoxGroup("加工")][ReadOnly] public float ProgressTimer;
    [BoxGroup("加工")] public GameObject CraftTarget;
    [BoxGroup("加工")] public CraftTypeData CraftTypeData;

    [BoxGroup("合成")] public int PairItemId;
    [BoxGroup("合成")] public int Id;
    [BoxGroup("合成")] public GameObject TargetItemPrefab;
    

    public void OnPutOrCatch()
    {
        transform.rotation = Quaternion.identity;
        Collider.isTrigger = true;
        Rb.velocity = Vector3.zero;
        Rb.isKinematic = true;
    }

    public void OnDrop()
    {
        Rb.isKinematic = false;
        Collider.isTrigger = false;
    }

    private void OnValidate()
    {
        if (Collider == null)
        {
            Collider = GetComponent<Collider>();
        }
        if (Rb == null)
        {
            Rb = GetComponent<Rigidbody>();
        }
    }
}
