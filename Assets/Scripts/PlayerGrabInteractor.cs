using HutongGames.PlayMaker;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class PlayerGrabInteractor : MonoBehaviour
    {
        public PlayerGrabSensor ItemSensor;
        public PlayerTableSensor TableSensor;
        public Transform Hand;
        [ReadOnly]
        public Item CurrentInHand;

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
            if (CurrentInHand != null && TableSensor.CurrentHold.Count == 0)
            {
                //Debug.Log(1);
                CurrentInHand.GetComponent<Rigidbody>().isKinematic = false;
                CurrentInHand.GetComponent<Collider>().isTrigger = false;
                CurrentInHand = null;
                return;
            }

            // catch
            if (CurrentInHand == null && ItemSensor.CurrentHold.Count > 0 && !ItemSensor.CurrentHold[0].isOnTable)
            {
                //Debug.Log(2);
                var target = ItemSensor.CurrentHold[0];
                CurrentInHand = target;
                target.transform.position = Hand.transform.position;
                CurrentInHand.OnPutOrCatch();
                return;
            }

            //  put on table
            if (CurrentInHand != null
                && TableSensor.CurrentHold.Count > 0 && TableSensor.CurrentHold[0].GetComponent<Table>().IsEmpty)
            {
                //Debug.Log(3);

                var targetTable = TableSensor.CurrentHold[0].GetComponent<Table>();
                var targetPoint = targetTable.TopPoint;
                if (!targetTable.IsEmpty) return;

                CurrentInHand.transform.position = targetTable.TopPoint.position;

                CurrentInHand.isOnTable = true;

                targetTable.IsEmpty = false;
                targetTable.CurrentItem = CurrentInHand;

                CurrentInHand.OnPutOrCatch();
                CurrentInHand = null;

                return;
            }

            // get from table
            if (CurrentInHand == null
                && TableSensor.CurrentHold.Count > 0 && !TableSensor.CurrentHold[0].GetComponent<Table>().IsEmpty)
            {
                //Debug.Log(4);

                var targetTable = TableSensor.CurrentHold[0].GetComponent<Table>();

                CurrentInHand = targetTable.CurrentItem;
                targetTable.IsEmpty = true;
                targetTable.CurrentItem = null;

                CurrentInHand.transform.position = Hand.transform.position;
                CurrentInHand.isOnTable = false;
                return;
            }
        }

        private void Craft(InputAction.CallbackContext ctx)
        {

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