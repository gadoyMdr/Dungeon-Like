using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Canvas canvas;
    public Image itemImage;
    public ItemData itemData;

    private void Awake()
    {
        itemImage.sprite = itemData.sprite;
    }

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }
}
