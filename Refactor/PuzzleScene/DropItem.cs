using UnityEngine;


namespace Scripts.Refactor
{
    public class DropItem : MonoBehaviour
    {
        [SerializeField]
        GroundStack groundStack;

        readonly float dropDistance = 1.5f;
        
        public void Drop(Transform transform, int amountToDrop, ItemData itemData = null, InventorySlot inventorySlot = null)
        {


            Vector3 dropPos = dropDistance * transform.forward + transform.position;


            if (itemData == null)
                itemData = inventorySlot.itemStack.itemData;

            GroundStack newStack = Instantiate(groundStack);

            newStack.transform.position = new Vector3(dropPos.x, dropPos.y, -MagicNumbers.one);
            newStack.SetStackData(itemData, amountToDrop);
            newStack.gameObject.name = "Stack : " + itemData.name;


            if (inventorySlot != null)
                inventorySlot.RemoveAmount(amountToDrop);


        }
    }
}

