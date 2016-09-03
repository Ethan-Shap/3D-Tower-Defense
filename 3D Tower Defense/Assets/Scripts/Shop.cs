using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public static Shop instance;

    public GameObject cancelPurchaseButton;
    public GameObject comfirmPurchaseButton;

    private Previewer previewer;
    private Player player;
    private GameManager gameManager;
    private ShopItem currentItem;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = Player.instance;
        previewer = Previewer.instance;
        gameManager = GameManager.instance;
        cancelPurchaseButton.SetActive(false);
        comfirmPurchaseButton.SetActive(false);
    }

    public void AttemptItemBuy(ShopItem itemToBuy)
    {
        if (player.Coins < itemToBuy.cost)
            return;

        cancelPurchaseButton.SetActive(true);
        comfirmPurchaseButton.SetActive(true);

        currentItem = itemToBuy;

        if(currentItem.item.GetComponent<Tower>())
            previewer.PreviewTower(itemToBuy.item);
    }

    public void CancelItemPurchase()
    {
        if (currentItem.item.GetComponent<Tower>())
        {
            previewer.ExitPreview();
        }
        cancelPurchaseButton.SetActive(false);
        comfirmPurchaseButton.SetActive(false);
    }

    public void ComfirmItemPurchase()
    {
        if (currentItem.item.GetComponent<Tower>())
        {
            previewer.Previewing = false;
            gameManager.selectedTower.GetComponent<Tower>().TriggerBuildAnimation();
        }

        cancelPurchaseButton.SetActive(false);
        comfirmPurchaseButton.SetActive(false);

        player.Coins += -currentItem.cost;
    }

}
