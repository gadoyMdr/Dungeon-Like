using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Refactor
{
    // The inventory of our player
    public class Inventory : MonoBehaviour
    {
        List<InventorySlot> slots = new List<InventorySlot>();  //All the inventory's slots

        private void Awake()
        {
            slots = FindObjectsOfType<InventorySlot>().ToList();    //Find all slots in the scene
        }

        /// <summary>
        /// When we want to add a groundStack
        /// </summary>
        /// <param name="groundStack">Ground stack to add</param>
        public void AddItem(GroundStack groundStack) => AddItem(groundStack.itemStack.itemData.Clone(), groundStack.itemStack.count);


        /// <summary>
        /// When we want to add a certain quantity of an item
        /// </summary>
        /// <param name="itemData">Item to add</param>
        /// <param name="amount">Quantity</param>
        public void AddItem(ItemData itemData, int amount)
        {
            if (FindSimilarSlot(itemData, out InventorySlot slot))   //There's already the item in the inventory
            {
                if (!slot.AddAmount(itemData, amount, out int surplus))   //There's surplus, so we re-try to add the item
                    AddItem(itemData, surplus);
            }
            else    //The item isn't in the inventory
            {
                if (FindTotalyEmptySlot(out InventorySlot slot1))
                {
                    if (!slot1.AddAmount(itemData, amount, out int surplus))   //There's surplus, so we re-try to add the item
                        AddItem(itemData, surplus);
                }
            }
        }

        #region SEARCH ENGINE

        public bool CanFit(ItemData itemData) => FindTotalyEmptySlot(out _) || FindSimilarSlot(itemData, out _);

        bool FindTotalyEmptySlot(out InventorySlot slot)
        {
            slot = slots.Where(x => x.itemStack.count == 0).FirstOrDefault();   //get any empty slot

            return slot != null;
        }


        bool FindSimilarSlot(ItemData itemData, out InventorySlot slot)
        {


            slot = slots
                .Where(x => x.itemStack.itemData != null)   //Just to make sure we don't get a null reference
                .Where(x => x.itemStack.itemData.Equals(itemData)  //find same item
                && x.itemStack.count != x.itemStack.itemData.stackCount)?    //check if can fit another one in the slot
                .FirstOrDefault();

            return slot != null;
        }


        #endregion
    }
}


