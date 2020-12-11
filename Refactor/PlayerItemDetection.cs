using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Refactor
{
    [RequireComponent(typeof(Inventory))]   //To make sure we have an Inventory attached
    public class PlayerItemDetection : MonoBehaviour    //We use Monobehavior's callbacks functions and more
    {
        Inventory inventory;    //The inventory we want to use

        private void Awake()    //When the gameObject is instantiated
        {
            inventory = GetComponent<Inventory>();  //We get the inventory component
        }

        //Detect collisions with anything
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out GroundStack stack))    //Check if it's a stack
            {
                if (stack.canBePicked && inventory.CanFit(stack.itemStack.itemData))    //Check if can be picked and fit in inventory
                {

                    inventory.AddItem(stack);   //Add the item to the inventory

                    Destroy(collision.gameObject);   //We destroy the stack on the ground because we don't want it anymore since we picked it up
                }

            }
        }

    }
}


