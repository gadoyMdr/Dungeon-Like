using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item")]
public class ItemData : ScriptableObject, IClonable<ItemData>
{
    public new string name;
    public int stackCount;
    public Sprite sprite;
    public Item itemPrefab;
    public ParticleSystem particle;

    /// <summary>
    /// We override Equals so we can have a predictable behavior
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public override bool Equals(object other)
    {
        if ((other == null) || !GetType().Equals(other.GetType()))  //this class isnt the same type as other
            return false;   //return false 
        else    //This class is same type
        {
            ItemData otherItem = (ItemData)other;   //convert
            return (name == otherItem.name);    //we check the names
        }
    }

    /// <summary>
    /// Used by HasSets etc..
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        unchecked
        {
            int result = 0;
            result += (result * 397) ^ name.Length;
            result += (result * 397) ^ stackCount;
            return result;
        }
    }

    //Clone function to copy this class and not pass it as a reference
    public ItemData Clone()
    {
        return new ItemData() { name = this.name, stackCount = this.stackCount, sprite = this.sprite, itemPrefab = this.itemPrefab };
    }
}
