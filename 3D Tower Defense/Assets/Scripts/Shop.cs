using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public static Shop instance;

    private Previewer previewer;
    private Player player;
    private ShopItem currentItem;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = Player.instance;
        previewer = Previewer.instance;
    }

    public void AttemptItemBuy(ShopItem itemToBuy)
    {
        if (player.GetCoins() < itemToBuy.cost)
            return;

        currentItem = itemToBuy;
        previewer.PreviewTower(itemToBuy.item);
    }

    public void ComfirmItemPurchase()
    {
        player.AddCoins(-currentItem.cost);
    }

}
