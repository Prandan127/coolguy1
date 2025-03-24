using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopSlot : MonoBehaviour
{
    public ItemSO itemSO;
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Image itemImage;

    [SerializeField] private ShopManager shopManager;
    public int price;

    public void Initialize(ItemSO newItemSO, int price)
    {
        itemSO = newItemSO;
        itemImage.sprite = itemSO.icon;
        itemNameText.text = itemSO.name;
        this.price = price;
        priceText.text = price.ToString(); 
    }

    public void OnBuyButtonOnClicked()
    {
        shopManager.TryBuyItem(itemSO, price);
    }
}
