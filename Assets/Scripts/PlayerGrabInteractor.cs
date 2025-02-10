using HutongGames.PlayMaker;
using Sirenix.OdinInspector;
using UnityEditor.UIElements;
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

            GameManager.Ins.Input.InputActions.PlayerControls.Craft.performed += TryCraft;
            // TODO:
            //GameManager.Ins.Input.InputActions.PlayerControls.Throw.started += ThrowAim;
            //GameManager.Ins.Input.InputActions.PlayerControls.Throw.canceld += Throw;
        }

        private void Interact(InputAction.CallbackContext ctx)
        {
            // drop
            if (CurrentInHand != null && TableSensor.CurrentHold.Count == 0)
            {
                Debug.Log(1);
                CurrentInHand.GetComponent<Rigidbody>().isKinematic = false;
                CurrentInHand.GetComponent<Collider>().isTrigger = false;
                CurrentInHand = null;
                return;
            }

            // catch
            if (CurrentInHand == null && ItemSensor.CurrentHold.Count > 0 && !ItemSensor.CurrentHold[0].isOnTable)
            {
                Debug.Log(2);
                var target = ItemSensor.CurrentHold[0];
                CurrentInHand = target;
                target.transform.position = Hand.transform.position;
                CurrentInHand.OnPutOrCatch();
                return;
            }

            //  put on table
            if (CurrentInHand != null
                && TableSensor.CurrentHold.Count > 0 && TableSensor.CurrentHold[0].IsEmpty)
            {
                Debug.Log(3);

                var targetTable = TableSensor.CurrentHold[0];
                var targetPoint = targetTable.TopPoint;
                if (!targetTable.IsEmpty) return;

                CurrentInHand.transform.position = targetTable.TopPoint.position;

                CurrentInHand.isOnTable = true;

                targetTable.CurrentItem = CurrentInHand;

                CurrentInHand.OnPutOrCatch();
                CurrentInHand = null;

                return;
            }

            // put on table and combine
            if (CurrentInHand != null
                && TableSensor.CurrentHold.Count > 0
                && !TableSensor.CurrentHold[0].IsEmpty
                && TableSensor.CurrentHold[0].CurrentItem.Id == CurrentInHand.PairItemId)
            {
                Debug.Log(4);

                var itemGo = Instantiate(CurrentInHand.TargetItemPrefab);
                var targetTable = TableSensor.CurrentHold[0];

                Destroy(CurrentInHand.gameObject);
                CurrentInHand = null;

                Destroy(targetTable.CurrentItem.gameObject);

                targetTable.CurrentItem = itemGo.GetComponent<Item>();
                targetTable.CurrentItem.OnPutOrCatch();
                targetTable.CurrentItem.transform.position = targetTable.TopPoint.position;
                targetTable.CurrentItem.isOnTable = true;
                return;
            }


            // get from table
            if (CurrentInHand == null
                && TableSensor.CurrentHold.Count > 0 && !TableSensor.CurrentHold[0].IsEmpty)
            {
                Debug.Log(5);

                var targetTable = TableSensor.CurrentHold[0];

                CurrentInHand = targetTable.CurrentItem;
                targetTable.CurrentItem = null;

                CurrentInHand.transform.position = Hand.transform.position;
                CurrentInHand.isOnTable = false;
                return;
            }
        }

        private void TryCraft(InputAction.CallbackContext ctx)
        {
            if (!ValidateCraft()) return;

            StartCraft();
        }

        private bool ValidateCraft()
        {
            if (CurrentInHand != null) return false;
            if (TableSensor.CurrentHold[0].IsEmpty) return false;

            var item = TableSensor.CurrentHold[0].CurrentItem;
            var table = TableSensor.CurrentHold[0];

            if (table is not CraftTable) return false;

            var craftTalbe = table as CraftTable;
            if (craftTalbe.SupportCraftType == item.CraftType)
                return true;
            else return false;
        }

        private void StartCraft()
        {
            var craftTable = TableSensor.CurrentHold[0] as CraftTable;
            var item = craftTable.CurrentItem;

            craftTable.TryStartProcessing();
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