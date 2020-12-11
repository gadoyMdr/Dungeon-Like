using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Refactor
{
    public class InventorySlot : MonoBehaviour
    {
        [HideInInspector]   //No need to see it in the inspector
        public bool isSelected; //Do we hold this item in our hand???

        [SerializeField]    //Need to see it in the inspector
        private TextMeshProUGUI countText;  //The text of how many item there's in that slot

        [SerializeField]    //Need to see it in the inspector
        private Image itemImage;    //The image of the item

        public ItemStack itemStack; //The actual item Stack

        SlotSelection slotSelection;

        public bool IsEmpty { get => itemStack.itemData == null; }  //Returns true if itemStack.itemDate there isnt

        private void Awake()
        {
            slotSelection = FindObjectOfType<SlotSelection>();
        }

        /// <summary>
        /// Called after Awake
        /// </summary>
        private void Start()
        {
            UpdateVisuals();    //We call UpdateVisuals at Start to remove all defaults texts ect
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">The amount we want to remove</param>
        public void RemoveAmount(int amount)
        {
            itemStack.RemoveAmount(amount, out int surplus);

            if (surplus > MagicNumbers.zero || itemStack.count == MagicNumbers.zero)    //there's a surplus (which means we tried to remove more than there was) 
                ClearInventorySlot();   //Clear the slot with the function ClearInventorySlot()
            else    //There's no surplus
                UpdateVisuals();    //We just update the visuals
        }

        /// <summary>
        /// It's a function
        /// </summary>
        /// <param name="amount">the amount to add</param>
        /// <param name="surplus">the surplus in case there's too much to add</param>
        /// <returns>Will return true if can add whole amount without surplus</returns>
        public bool AddAmount(ItemData itemData, int amount, out int surplus)
        {
            FindObjectOfType<SlotSelection>().RefreshCurrentInventorySlot();

            slotSelection.RefreshCurrentInventorySlot(); //We refresh
            itemStack.AddAmount(itemData, amount, out surplus); //We actualy add the amount
            UpdateVisuals();    //We call the function UpdateVisuals()

            return surplus == MagicNumbers.zero;    //We return true if there's no surplus
        }

        /// <summary>
        /// Function to update the slot's image and the count text
        /// </summary>
        void UpdateVisuals()
        {
            if (!IsEmpty)
            {
                countText.text = itemStack.count.ToString();    //We change the image
                itemImage.sprite = itemStack.itemData.sprite;   //We change the count
            }
            else
                ClearInventorySlot();
        }

        /// <summary>
        /// Function when we want to clear the slot
        /// </summary>
        public void ClearInventorySlot()
        {
            itemImage.sprite = null;    //We remove the image
            countText.text = MagicNumbers.emptyString;   //We set the text to 0
            itemStack.ClearStack(); //We clear the stack
        }
    }
}

