using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItem : MonoBehaviour {

    public GameObject item;
    public int cost = 25;

    private Text priceText;
    private Shop shop;

	// Use this for initialization
	void Start ()
    {
        if (!item)
            throw new System.NullReferenceException("No item for player to buy on shop item " + this.gameObject.name);

        shop = Shop.instance;
        priceText = GetComponentInChildren<Text>();

        priceText.text = cost.ToString();
	}
	
	public void Clicked()
    {
        shop.AttemptItemBuy(this);
    }

}
