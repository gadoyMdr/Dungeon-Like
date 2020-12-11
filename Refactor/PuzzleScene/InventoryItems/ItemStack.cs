using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    public ItemData itemData;
    public int count;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount">The Amount to remove</param>
    /// <param name="surplus">The Amount of item that couldn't be removed from the stack</param>
    /// <returns>Returns true if could removed the integrity of the amount passed in parameter</returns>
    public bool RemoveAmount(int amount, out int surplus)
    {
        surplus = count - amount < 0 ? Mathf.Abs(count - amount) : 0;
        count = Mathf.Max(0, count - amount);
        return surplus == 0;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount">The Amount to remove</param>
    /// <param name="surplus">The Amount of item that couldn't be removed from the stack</param>
    /// <returns>Returns true if could add the integrity of the amount passed in parameter</returns>
    public bool AddAmount(ItemData itemData, int amount, out int surplus)
    {
        if (this.itemData == null)
            this.itemData = itemData;

        surplus = count + amount > this.itemData.stackCount ? Mathf.Abs(count + amount - this.itemData.stackCount) : 0;
        count = Mathf.Min(this.itemData.stackCount, count + amount);
        return surplus == 0;
    }

    /// <summary>
    /// Clear the stack
    /// </summary>
    public void ClearStack()
    {
        itemData = null;
        count = 0;
    }
}
