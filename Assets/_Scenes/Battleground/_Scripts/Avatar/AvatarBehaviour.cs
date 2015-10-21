using UnityEngine;
using System.Collections;

public class AvatarBehaviour : MonoBehaviour {

    public int maxLife;
    [Header("Inspection")]
    public int life;

    [HideInInspector]
    public Gun gun;

    public Transform gunPivot;

    [HideInInspector]
    public AvatarLocomotion locomotion;

    bool isKilled = false;
    BattlegroundController controller;

    CharacterFragment characterFragment;

    void Awake() {
        locomotion = GetComponent<AvatarLocomotion>();
        controller = GameObject.FindGameObjectWithTag(Tags.BattlegroundController).GetComponent<BattlegroundController>();
    }

    void Start() {
        characterFragment = GlobalController.instance.saveFile.character;
        StartCoroutine(waitForCharacterFragment());
    }

    public void ChangeWeapon(Gun newGun) {
        gun = Instantiate(newGun);
    }

    public void GetHit(EnemyBehaviour enemy) {
        life -= enemy.damage;
        controller.worldUI.PresentDamageText(enemy.damage, transform.position);
        GlobalController.instance.eventRegistery.ActivateEvent(EEventRegistrationType.AVATAR_HIT);
        if (life <= 0) {
            avatarKilled();
        }
    }

    public void PressedShoot() {
        if (gun != null) {
            gun.InnerPressedShoot();
        }
    }

    public void UnpressedShoot() {
        if (gun != null) {
            gun.InnerUnpressedShoot();
        }
    }

    public void PressedReload() {
        if (gun != null) {
            gun.Reload();
        }
    }

    void avatarKilled() {
        if (isKilled == false) {
            GlobalController.instance.eventRegistery.ActivateEvent(EEventRegistrationType.AVATAR_KILLED);
            locomotion.enabled = false;
            GetComponent<AvatarLocorotation>().enabled = false;
            GetComponent<Animator>().SetTrigger("Die");
            isKilled = true;
        }
    }

    IEnumerator waitForCharacterFragment() {
        while (characterFragment.isInitialized == false) {
            yield return null;
        }

        life = maxLife = characterFragment.life;
        locomotion.speed = characterFragment.speed;
        controller.uiManager.lifeBar.SetPropetries(maxLife, life);


        controller.uiManager.UpdateXPBar();
    }

}
