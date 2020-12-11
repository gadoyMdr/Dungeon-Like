using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Refactor {
    public class PlayerInput : MonoBehaviour 
    {
        [SerializeField]
        private DropItem dropItem;

        public static Action UseItem;
        public static Action SecondUseItem;
        public static Action mainItemActionEvent;
        public static Action<int> slotChangedEvent;

        public SlotSelection slotSelection;

        private Controls _controls;
        private Board _board;

        private void Awake()
        {
            dropItem = FindObjectOfType<DropItem>();
            _board = FindObjectOfType<Board>();
            _controls = new Controls();

            _controls.Game.MoveUp.performed += _ => _board.TryMovePusher(Vector2Int.up);
            _controls.Game.MoveDown.performed += _ => _board.TryMovePusher(Vector2Int.down);
            _controls.Game.MoveLeft.performed += _ => _board.TryMovePusher(Vector2Int.left);
            _controls.Game.MoveRight.performed += _ => _board.TryMovePusher(Vector2Int.right);
        }

        // Start is called before the first frame update
        void Start()
        {
            ThingsToDoWhenGameStarts();
        }

        private void OnEnable()
        {
            _controls.Game.Enable();
        }

        private void OnDisable()
        {
            _controls.Game.Disable();
        }

        void ThingsToDoWhenGameStarts()
        {
            slotSelection = FindObjectOfType<SlotSelection>();
            slotChangedEvent?.Invoke(0);
        }

        

        void Update()
        {
            CheckHotKeys();
            CheckDropButton();
            CheckMouseScroll();
            CheckUseItem();
        }

        #region CHECKS

        void CheckHotKeys()
        {
            //Keyboard input
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
                slotChangedEvent?.Invoke(0);
            if (Keyboard.current.digit2Key.wasPressedThisFrame)
                slotChangedEvent?.Invoke(1);
            if (Keyboard.current.digit3Key.wasPressedThisFrame)
                slotChangedEvent?.Invoke(2);
            if (Keyboard.current.digit4Key.wasPressedThisFrame)
                slotChangedEvent?.Invoke(3);
            if (Keyboard.current.digit5Key.wasPressedThisFrame)
                slotChangedEvent?.Invoke(4);
            if (Keyboard.current.digit6Key.wasPressedThisFrame)
                slotChangedEvent?.Invoke(5);
            if (Keyboard.current.digit7Key.wasPressedThisFrame)
                slotChangedEvent?.Invoke(6);
            if (Keyboard.current.digit8Key.wasPressedThisFrame)
                slotChangedEvent?.Invoke(7);
        }

        void CheckMouseScroll()
        {
            //Mouse input

            if (Mouse.current.scroll.ReadValue().y > 0)
            {
                int tempSlotNumber = slotSelection.currentSlotSelected.transform.GetSiblingIndex() - 1;
                if (tempSlotNumber < 0) tempSlotNumber = slotSelection.slotCount - 1;
                slotChangedEvent?.Invoke(tempSlotNumber);
            }
            else if (Mouse.current.scroll.ReadValue().y < 0)
            {
                int tempSlotNumber = slotSelection.currentSlotSelected.transform.GetSiblingIndex() + 1;
                if (tempSlotNumber > slotSelection.slotCount - 1) tempSlotNumber = 0;
                slotChangedEvent?.Invoke(tempSlotNumber);
            }
        }

        void CheckDropButton()
        {
            //Drop input
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                InventorySlot slotToDrop = slotSelection.currentSlotSelected;

                if (slotToDrop != null)
                {
                    if (!slotToDrop.IsEmpty)
                    {
                        //Drop just one
                        dropItem.Drop(gameObject.transform, 1, null, slotToDrop);
                    }
                }
            }
        }

        void CheckUseItem()
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
                UseItem?.Invoke();
            if (Mouse.current.leftButton.wasPressedThisFrame)
                SecondUseItem?.Invoke();
        }

        #endregion
    }
}
