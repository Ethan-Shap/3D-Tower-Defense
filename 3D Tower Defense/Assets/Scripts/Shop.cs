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

    /// <summary>
    /// Attempt to buy an item from the shop, if the player has enough money
    /// </summary>
    /// <param name="itemToBuy"></param>
    public void AttemptItemBuy(ShopItem itemToBuy)
    {
        if (player.Coins < itemToBuy.cost)
            return;

        // Enables the cancel and comfirm purchase buttons for the user
        cancelPurchaseButton.SetActive(true);
        comfirmPurchaseButton.SetActive(true);

        currentItem = itemToBuy;

        // If the current item is a tower, show preview
        if(currentItem.item.GetComponent<Tower>())
            previewer.PreviewTower(itemToBuy.item);
    }

    /// <summary>
    /// Cancels item purchase, takes no money from the user
    /// </summary>
    public void CancelItemPurchase()
    {
        // Exits preview if the current item is a tower
        if (currentItem.item.GetComponent<Tower>())
        {
            previewer.ExitPreview();
        }
        cancelPurchaseButton.SetActive(false);
        comfirmPurchaseButton.SetActive(false);
    }

    public void ComfirmItemPurchase()
    {
        if (currentItem.item.GetComponent<Tower>() && previewer.overlapping)
        {
            cancelPurchaseButton.SetActive(false);
            comfirmPurchaseButton.SetActive(false);
            previewer.Previewing = false;
            gameManager.selectedTower.GetComponent<Tower>().TriggerBuildAnimation();
            player.Coins += -currentItem.cost;
        } else
        {
            player.Coins += -currentItem.cost;
            cancelPurchaseButton.SetActive(false);
            comfirmPurchaseButton.SetActive(false);
        }
    }

}
