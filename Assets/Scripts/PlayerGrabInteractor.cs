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

            GameManager.Ins.Input.InputActions.PlayerControls.Craft.performed += TryCraft;
            // TODO:
            //GameManager.Ins.Input.InputActions.PlayerControls.Throw.started += ThrowAim;
            //GameManager.Ins.Input.InputActions.PlayerControls.Throw.canceld += Throw;
        }

        private void Interact(InputAction.CallbackContext ctx)
        {
            // drop
            if (!IsHandEmpty() && !TableSensor.HasTarget)
            {
                DropItem();
                return;
            }

            // catch
            if (IsHandEmpty() && ItemSensor.HasTarget && !ItemSensor.Target.isOnTable)
            {
                CatchItem(ItemSensor.Target);
                return;
            }

            //  put on table
            if (!IsHandEmpty() && TableSensor.HasTarget && TableSensor.Target.IsEmpty)
            {
                PutHandItemOnTable(TableSensor.Target);
                return;
            }

            // put on table and combine
            if (!IsHandEmpty() && TableSensor.HasTarget && !TableSensor.Target.IsEmpty && TableSensor.Target.CurrentItem.Id == CurrentInHand.PairItemId)
            {
                PutOnTableAndCombine();
                return;
            } 

            // get from table
            if (IsHandEmpty() && TableSensor.HasTarget && !TableSensor.Target.IsEmpty)
            {
                TakeFromTable(TableSensor.Target);
                return;
            }
        }

        private void TakeFromTable(IItemHolder table)
        {
            CurrentInHand = table.TookItem();
        }

        private void PutOnTableAndCombine()
        {
            var newItemGo = Instantiate(CurrentInHand.TargetItemPrefab);
            var newItem = newItemGo.GetComponent<Item>();

            Destroy(CurrentInHand.gameObject);
            CurrentInHand = null;

            TableSensor.Target.ClearItem();
            TableSensor.Target.SpawnItem(newItem);
        }

        private void CatchItem(Item target)
        {
            CurrentInHand = target;
            target.transform.position = Hand.transform.position;
            CurrentInHand.OnPutOrCatch();
        }

        private void DropItem()
        {
            CurrentInHand.OnDrop();
            CurrentInHand = null;
        }

        private void PutHandItemOnTable(IItemHolder targetTable)
        {
            targetTable.PutItemOn(CurrentInHand);
            CurrentInHand = null;
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

        private bool IsHandEmpty() => CurrentInHand == null;
    }
}