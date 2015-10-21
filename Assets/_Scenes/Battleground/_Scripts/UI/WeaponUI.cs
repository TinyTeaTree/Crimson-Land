using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WeaponUI : MonoBehaviour {

    public Text weaponNameText;
    public Image weaponIcon;

    public RectTransform bulletPlace, layoutPlace, reloadPlace;
    float magicNumber = 56f;
    float minHeight;

    public GameObject[] bulletIcons;
    int counter = 0;

    void Start() {
        minHeight = layoutPlace.rect.height;
        reloadPlace.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
    }

    void Update() {
        if (bulletPlace.rect.height > magicNumber) {
            layoutPlace.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, minHeight + bulletPlace.rect.height - magicNumber);
        } else {
            layoutPlace.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, minHeight);
        }
    }

    public void ReloadUI(float time) {
        StartCoroutine(reloadCo(time));
    }

    public void SetWeapon(Gun gun) {
        weaponNameText.text = gun.name;
        weaponIcon.sprite = gun.gunIcon;
    }

    public void SetNumOfBulletIcons(int num) {
        if (num > counter) {
            makeMoreBullets(num - counter);
        } else if (num < counter) {
            eraseBullets(counter - num);
        }
    }

    void makeMoreBullets(int num) {
        for (int i=counter; i<counter+num; ++i) {
            bulletIcons[i].SetActive(true);
        }
        counter = counter+num;
    }

    void eraseBullets(int num) {
        for (int i=counter-1; i>= counter-num; --i) {
            bulletIcons[i].SetActive(false);
        }
        counter -= num;
    }

    IEnumerator reloadCo(float time) {
        reloadPlace.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, layoutPlace.rect.width);

        float timePassed = 0;
        while (timePassed < time) {
            float ratio = timePassed / time;

            reloadPlace.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (1.0f - ratio) * layoutPlace.rect.width);

            timePassed += Time.deltaTime;
            yield return null;
        }
    }
	
}
