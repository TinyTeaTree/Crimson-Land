using UnityEngine;
using System.Collections;

public class Gun : MyMonoBehaviour {

    public Sprite gunIcon;
    public Transform bulletSpawnAnchor;

    [SerializeField]
    GameObject bulletFab;

    protected Transform bulletParent;

    public float reloadingTime;
    public int bulletsPerReload;
    public float feedbackForce;

    public int price;

    int _currentBulletsInCartridge;
    int currentBulletsInCartridge {
        get { return _currentBulletsInCartridge; }
        set { 
            _currentBulletsInCartridge = value;
            controller.uiManager.weaponUI.SetNumOfBulletIcons(_currentBulletsInCartridge);
        }
    }

    bool _isReloading = false;
    public bool isReloading {
        get { return _isReloading; }
    }
    bool remState = false;
    bool isShooting = false;

    BattlegroundController controller;
    GunFeedback feedback;

    void Awake() {
        controller = GameObject.FindGameObjectWithTag(Tags.BattlegroundController).GetComponent<BattlegroundController>();
       
        if (controller.avatar.gun != null) {
            Destroy(controller.avatar.gun.gameObject);
        }
        controller.avatar.gun = this;
        transform.SetParent(controller.avatar.gunPivot);
        transform.ResetTransform();
        bulletParent = controller.bulletParent;

        currentBulletsInCartridge = bulletsPerReload;
        feedback = transform.parent.GetComponent<GunFeedback>();
    }


    protected virtual GameObject spawnBullet() {
        GameObject bullet = Instantiate(bulletFab) as GameObject;
        bullet.transform.SetParent(bulletParent);
        bullet.transform.SetTransform(bulletSpawnAnchor);

        feedback.PushBack(feedbackForce);

        if (bulletsPerReload > 0) {
            if (currentBulletsInCartridge > 0) {
                --currentBulletsInCartridge;
                if (currentBulletsInCartridge == 0) {
                    Reload();
                }
            }
        }//else its inifinte bullets per reload

        return bullet;
    }

    public void InnerPressedShoot() {
        if (isShooting == false) {
            if (_isReloading == false) {
                PressedShoot();
            } else {
                remState = true;
            }

            isShooting = true;
        }
    }

    public void InnerUnpressedShoot() {
        if (isShooting == true) {
            if (_isReloading == false) {
                UnpressedShoot();
            } else {
                remState = false;
            }

            isShooting = false;
        }
    }

    public virtual void PressedShoot() { }

    public virtual void UnpressedShoot() { }

    public void Reload() {
        UnpressedShoot();
        _isReloading = true;
        remState = true;
        controller.uiManager.weaponUI.ReloadUI(reloadingTime);
        Invoke(() => {
            currentBulletsInCartridge = bulletsPerReload;
            _isReloading = false;
            if (remState == true) {
                isShooting = false;
                InnerPressedShoot();
            } else {
                isShooting = true;
                InnerUnpressedShoot();
            }
        }, reloadingTime);
    }

}
