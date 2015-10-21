using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public LifeBar lifeBar, xpBar;
    AvatarBehaviour avatar;
    [HideInInspector]
    public WeaponUI weaponUI;
    [HideInInspector]
    public GameOverUI gameOver;
    [HideInInspector]
    public PauseUI pauseUI;

    void Awake() {
        avatar = GameObject.FindGameObjectWithTag(Tags.Avatar).GetComponent<AvatarBehaviour>();

        weaponUI = GetComponentInChildren<WeaponUI>();
        gameOver = GetComponentInChildren<GameOverUI>();
        pauseUI = GetComponentInChildren<PauseUI>();
    }

    void Start() {
        BarEffect lifeBarEffect = lifeBar.GetComponent<BarEffect>();
        GlobalController.instance.eventRegistery.AddRegisteration(EEventRegistrationType.AVATAR_HIT, lifeBarEffect.Effect, true, false);

        GlobalController.instance.eventRegistery.AddRegisteration(EEventRegistrationType.AVATAR_KILLED, gameOver.ShowGameOver, true, true);
        GlobalController.instance.eventRegistery.AddRegisteration(EEventRegistrationType.AVATAR_HIT, () => {
            lifeBar.SetLife(avatar.life);
        }, true, false);

        BarEffect xpBarEffect = xpBar.GetComponent<BarEffect>();
        GlobalController.instance.eventRegistery.AddRegisteration(EEventRegistrationType.KILL_SOMETHING, ()=>{
            xpBarEffect.Effect();
            UpdateXPBar();
        }, true, false);
    }

    public void SetWeapon(Gun gun) {
        weaponUI.SetWeapon(gun);
    }

    public void UpdateXPBar() {
        int myXp = GlobalController.instance.saveFile.character.xp;
        int overflowXp = GlobalController.instance.dataFile.levelData.GetOverflowXP(myXp);
        int myXpNeeded = GlobalController.instance.dataFile.levelData.GetLevelXP(myXp);
        xpBar.SetPropetries(myXpNeeded, overflowXp);
    }

}
