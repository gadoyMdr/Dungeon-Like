using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundStack : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private Image image;

    [HideInInspector]
    public bool canBePicked = false;    //Used to check if can be picked

    public ItemStack itemStack;

    private float timeUntilCanBePicked = 2; 

    private bool firstInstantiation = false;

    private void Awake()
    {
        canvas.worldCamera = Camera.main;
    }

    private void Start()
    {
        if (firstInstantiation)
            SetStackData(itemStack.itemData, 1);

        StartCoroutine(CanBePickedCoolDown());  //if i don't put that we pick up the item right when we drop it
    }

    public void SetStackData(ItemData itemData, int amountToDrop)
    {
        itemStack.itemData = itemData;
        itemStack.count = amountToDrop;

        image.sprite = itemData.sprite;
    }

    IEnumerator CanBePickedCoolDown()
    {
        yield return new WaitForSecondsRealtime(timeUntilCanBePicked);
        canBePicked = true;
    }
}
