using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Refactor
{
    /// <summary>
    /// The class that takes care of equipping and unequipping the current item
    /// </summary>
    public class ItemEquipManager : MonoBehaviour
    {
        public Action<ItemData> ItemChanged;

        public static Item currentItem;    //The item we currently have in between our hands, or maybe you just hold it with one hand

        SlotSelection slotSelection;    //Need this so we can subscribe to whenever a slot is changed, and equip its item

        private void Awake()    //Callback function when the gameObject is being instantiated
        {
            slotSelection = FindObjectOfType<SlotSelection>();
        }

        private void OnEnable()
        {
            slotSelection.SlotSelected += Equip;    //We subscribe
            PlayerInput.UseItem += TryUseItem;
            PlayerInput.SecondUseItem += TrySecondUseItem;
        }

        private void OnDisable()
        {
            slotSelection.SlotSelected -= Equip; //We unSubscribe
            PlayerInput.UseItem -= TryUseItem;
            PlayerInput.SecondUseItem -= TrySecondUseItem;
        }

        public void Equip(ItemData itemData)
        {
            UnEquipCurrentItem();
            if(itemData != null)
            {
                currentItem = Instantiate(itemData.itemPrefab, new Vector3(transform.position.x, transform.position.y + 1.2f, 0), Quaternion.identity, transform);
                ItemChanged?.Invoke(currentItem.itemData);
            }
                
        }

        public void UnEquipCurrentItem()
        {
            if(currentItem != null)
            {
                ItemChanged?.Invoke(null);
                Destroy(currentItem.gameObject);
            }
                
        }

        //Do second action
        void TrySecondUseItem()
        {
            if (currentItem != null)
                if (currentItem.TryGetComponent(out SecondAction secondAction))
                    secondAction.DoSecondAction();
                
                    
        }

        //Do main action
        void TryUseItem()
        {
            if(currentItem != null)
            if (currentItem.TryGetComponent(out MainAction mainAction))
                mainAction.Use(new object[] { GetComponent<Health>() });
        }
    }
}

