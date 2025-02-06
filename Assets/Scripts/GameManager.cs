using MoreMountains.TopDownEngine;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Ins;

        public InputSystemManager Input;

        private void Awake()
        {
            if (Ins == null) { Ins = this; }
        }

        void Start()
        {
            if (Input == null)
            {
                throw new System.Exception("Missing Input Handler");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}