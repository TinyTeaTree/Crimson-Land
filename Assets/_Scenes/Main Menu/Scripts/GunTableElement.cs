using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GunTableElement : MonoBehaviour {

    public Image gunIcon;
    public Text gunName;
    public Text gunPrice;

    public Button buyButton;
    System.Action buyAction;

    public void SetProperties(Gun gun, bool isOwned, System.Action buyAction) {
        this.buyAction = buyAction;
        gunIcon.sprite = gun.gunIcon;
        gunName.text = gun.name;
        gunPrice.text = gun.price + " Gold";

        if( GlobalController.instance.saveFile.treasury.gold > gun.price && isOwned == false)
            buyButton.interactable = true;
        else {
            buyButton.interactable = false;
        }
    }

    public void BuyPressed() {
        buyButton.interactable = false;
        if (buyAction != null) {
            buyAction();
        }
    }

}
