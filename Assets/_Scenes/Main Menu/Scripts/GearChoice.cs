using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GearChoice : MonoBehaviour {

    public Text goldAvailable;
    

    public RectTransform table;
    public GunTableElement elementOriginal;

    List<GunTableElement> tableList = new List<GunTableElement>();
    string goldPre;

    void Start() {
        goldPre = goldAvailable.text;
        elementOriginal.gameObject.SetActive(false);
    }

    public void SetUp() {
        goldAvailable.text = goldPre + GlobalController.instance.saveFile.treasury.gold;

        if (tableList.Count == 0) {
            Gun[] guns = GlobalController.instance.dataFile.gunsData.guns;
            for (int i=0; i<guns.Length; ++i) {
                int cachedIndex = i;
                GunTableElement newElement = Instantiate(elementOriginal);
                newElement.SetProperties(guns[i], GlobalController.instance.saveFile.weapons.HasWeapon(i), () => {
                    GlobalController.instance.saveFile.treasury.SpendGold(guns[cachedIndex].price);
                    GlobalController.instance.saveFile.weapons.AddWeapon(cachedIndex);
                    SetUp();
                });
                newElement.transform.SetParent(table);
                newElement.transform.ResetTransform();
                newElement.gameObject.SetActive(true);
                tableList.Add(newElement);
            }
        }
    }



}
