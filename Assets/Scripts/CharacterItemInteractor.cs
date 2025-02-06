using HutongGames.PlayMaker;
using System.Collections;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class CharacterItemInteractor : MonoBehaviour
    {
        public Sensor Sensor;
        public Transform Hand;
        [Readonly]
        public GameObject CurrentInHand;

        private void Start()
        {
            GameManager.Ins.Input.InputActions.PlayerControls.Interact.started += Interact;
        }

        private void Interact(InputAction.CallbackContext ctx)
        {
            if (CurrentInHand != null)
            {
                CurrentInHand = null;
                return;
            }

            if(Sensor.CurrentHold.Count> 0)
            {
                var target = Sensor.CurrentHold[0];
                CurrentInHand = target;
                target.transform.position = Hand.transform.position;
            }
        }

        private void Update()
        {
            if(CurrentInHand != null)
            {
                CurrentInHand.transform.position = Hand.transform.position;
            }
        }
    }
}