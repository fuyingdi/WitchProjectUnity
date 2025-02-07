using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Item : MonoBehaviour
    {
        public Rigidbody Rb;
        public Collider Collider;
        public bool isOnTable;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

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
    }
}