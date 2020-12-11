using System;
using UnityEngine;

namespace Scripts.Refactor
{
    /// <summary>
    /// Slot selection system
    /// </summary>
    public class SlotSelection : MonoBehaviour
    {
        public Action<ItemData> SlotSelected;  //Will emit a signal every time a slot is selected with the slot's item

        //The "slot selected" image
        [SerializeField] private GameObject selectedSlotImage;

        [HideInInspector]
        public InventorySlot currentSlotSelected;   //The current selected slot

        [HideInInspector]
        public int slotCount;   //Will be used just to make sure the player doesnt try to access a slot that doesnt exists

        [HideInInspector]
        public Transform mainBarTransform;

        public int lastSelectedSlotID;

        GameObject instantiatedSelectedSlotImage;


        private void Awake()
        {

            mainBarTransform = GameObject.FindGameObjectWithTag("MainBar").transform;
            slotCount = mainBarTransform.childCount;
            instantiatedSelectedSlotImage = Instantiate(selectedSlotImage, mainBarTransform.GetChild(0).transform);
        }

        private void OnEnable()
        {
            PlayerInput.slotChangedEvent += ChangeSelectedSlot; //Subscribe
        }

        private void OnDisable()
        {
            PlayerInput.slotChangedEvent -= ChangeSelectedSlot;//UnSubscribe
        }

        public void ChangeSelectedSlot(int newSlot)
        {
            if (newSlot < mainBarTransform.childCount)
            {
                lastSelectedSlotID = newSlot;

                //Set former currentSlot to not selected
                if (currentSlotSelected != null)
                    currentSlotSelected.isSelected = false;
                

                //Set new currentSlot to selected
                currentSlotSelected = mainBarTransform.GetChild(newSlot).GetComponent<InventorySlot>();

                currentSlotSelected.isSelected = true;  //We tell the inventorySlot it's now selected

                //Initiate and place the selected sprite
                instantiatedSelectedSlotImage.transform.SetParent(currentSlotSelected.transform);
                instantiatedSelectedSlotImage.GetComponent<RectTransform>().localPosition = Vector3.zero;

                
                SlotSelected?.Invoke(currentSlotSelected.itemStack.itemData);  //We fire the action so everybody who's subscribed get notified
            }
        }

        public void RefreshCurrentInventorySlot()
        {
            FindObjectOfType<ItemEquipManager>().UnEquipCurrentItem();  //UnEquip Item
            ChangeSelectedSlot(lastSelectedSlotID); //Re select the currentSlot in case there are > 1 itemCount
        }
    }
}

