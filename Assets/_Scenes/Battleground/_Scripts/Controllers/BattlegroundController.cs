using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// holds the necesseties of the mobs when they get created 
/// </summary>
public class BattlegroundController : MyMonoBehaviour {

    [HideInInspector]
    public UIManager uiManager;
    [HideInInspector]
    public WorldUIManager worldUI;
    [HideInInspector]
    public PauseManager pauseManager;
    [HideInInspector]
    public MobSpawnerManager mobSpawner;
    [HideInInspector]
    public AvatarBehaviour avatar;
    [HideInInspector]
    public TerrainHolder terrain;

    public Transform mobParent;
    public Transform bulletParent;
    public Gun[] availableGuns;  
    public int startingTerrain;
    public int startingGunIndex;
    public float gameOverDelay;
    public float startSpawnDelay;   
    [HideInInspector]
    public bool isDead = false;

    BattleInfo choosenBattle;

#if UNITY_EDITOR
    public int developmentBattle;
#endif

    int currentGunIndex = -1;

    void Awake() {
        uiManager = FindObjectOfType<UIManager>();
        worldUI = FindObjectOfType<WorldUIManager>();
        pauseManager = GetComponentInChildren<PauseManager>();
        avatar = GameObject.FindGameObjectWithTag(Tags.Avatar).GetComponent<AvatarBehaviour>();
    }

    void Start() {
        loadChoosenBattleScene();

        GlobalController.instance.eventRegistery.AddRegisteration(EEventRegistrationType.AVATAR_KILLED, () => {
            isDead = true;
            Invoke(gameOver, gameOverDelay);
        }, true, true);

        GlobalController.instance.eventRegistery.AddRegisteration(EEventRegistrationType.KILL_SOMETHING, () => {
            if (mobSpawner.HasKilledEverything() == true) {
                Invoke(gameWin, gameOverDelay);
            }
        }, true, false);

        if (startingGunIndex >= 0) {
            PressedWeaponChange(startingGunIndex);
        }

        Invoke(mobSpawner.StartSpawning, startSpawnDelay);


    }


    public void PressedShoot(bool pressState) {
        if (pauseManager.isPause || pauseManager.isInMiddle) {
            return;
        }

        if (pressState == true && isDead == false) {
            avatar.PressedShoot();
        } else {
            avatar.UnpressedShoot();
        }
    }
    public void PressedWeaponChange(int num) {
        num--;
        if (num > availableGuns.Length) {
            Debug.LogError("ERROR: requested nonexistant gun type " + num);
            return;
        }

        if (isDead == true) {
            return;
        }

        if (currentGunIndex == num) {
            //we already have this gun
            return;
        }

        if (avatar.gun != null && avatar.gun.isReloading == true) {
            return;
        }


        if (GlobalController.instance.saveFile.weapons.HasWeapon(num) == true) {

            avatar.ChangeWeapon(availableGuns[num]);
            uiManager.SetWeapon(availableGuns[num]);

            currentGunIndex = num;

        }
    }
    public void PressedPause() {
        pauseManager.Pause();
    }
    public void PressedResume() {
        pauseManager.Unpause();
    }
    public void PressedExit() {
        GlobalController.instance.ExitGame();
    }

    public void SetUpTerrain(TerrainHolder terrain) {
        this.terrain = terrain;

        IList<ITerrainRequester> terrainYearners = InterfaceHelper.FindObjects<ITerrainRequester>();
        for (int i=0; i<terrainYearners.Count; ++i) {
            terrainYearners[i].SetTerrain(this.terrain);
        }
    }


    void gameOver() {
        //GlobalController.instance.scoreElement = new ScoreElement(GlobalController.instance.saveFile.profileFragment.username, scoreKeeper.currentScore);
        //GlobalController.instance.transitionery.GoTo(TagsLocations.gameOver);
    }

    void gameWin() {
        if (GlobalController.instance.saveFile.battles.data.battles.Count <= GlobalController.instance.battleChoosen + 1 || GlobalController.instance.saveFile.battles.data.battles[GlobalController.instance.battleChoosen + 1].isUnlocked == false) {
            GlobalController.instance.saveFile.battles.UnlockNextBattle();
        }
        GlobalController.instance.transitionery.GoTo(TagsLocations.mainMenu);
    }



    void loadChoosenBattleScene() {

#if UNITY_EDITOR
        if (GlobalController.instance.battleChoosen >= 0) {
            choosenBattle = GlobalController.instance.dataFile.battlesData.battles[GlobalController.instance.battleChoosen];
        } else {
            Debug.LogWarning("DEVELOPMENT battle choosen");
            choosenBattle = GlobalController.instance.dataFile.battlesData.battles[developmentBattle];
        }
#else
            choosenBattle = GlobalController.instance.dataFile.battlesData.battles[GlobalController.instance.battleChoosen];
#endif

        Instantiate(choosenBattle.terrain);
        mobSpawner = Instantiate(choosenBattle.mobSpawner);
        mobSpawner.transform.SetParent(transform);
    }

}