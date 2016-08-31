using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public static Shop instance;

    private static Previewer previewer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        previewer = Previewer.instance;
    }

    public void AttemptItemBuy(ShopItem itemToBuy)
    {
        if (Player.instance.coins < itemToBuy.cost)
            return;

        previewer.PreviewTower(itemToBuy.item);
    }

}
