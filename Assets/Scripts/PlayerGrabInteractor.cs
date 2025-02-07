using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class PlayerGrabInteractor : MonoBehaviour
    {
        public PlayerGrabSensor Sensor;
        public Transform Hand;
        [Readonly]
        public GameObject CurrentInHand;

        private void Start()
        {
            GameManager.Ins.Input.InputActions.PlayerControls.Interact.started += Interact;

            // TODO:
            //GameManager.Ins.Input.InputActions.PlayerControls.Throw.started += ThrowAim;
            //GameManager.Ins.Input.InputActions.PlayerControls.Throw.canceld += Throw;
        }

        private void Interact(InputAction.CallbackContext ctx)
        {
            // drop
            if (CurrentInHand != null)
            {
                CurrentInHand = null;
                return;
            }

            // catch
            if (Sensor.CurrentHold.Count > 0)
            {
                var target = Sensor.CurrentHold[0];
                CurrentInHand = target;
                target.transform.position = Hand.transform.position;
            }

            // TODO: put on table
            if (CurrentInHand != null)
            {

            }
        }

        private void ThrowAim(InputAction.CallbackContext ctx)
        {
            // Lock Move

            // Show Projectile Trail
        }

        private void Throw(InputAction.CallbackContext ctx)
        {
            // UnLock Move

            // AddForceTo Target
        }

        private void Update()
        {
            if (CurrentInHand != null)
            {
                CurrentInHand.transform.position = Hand.transform.position;
            }
        }
    }
}