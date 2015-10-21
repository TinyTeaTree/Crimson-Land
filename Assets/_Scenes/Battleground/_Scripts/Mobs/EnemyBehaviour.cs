using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MyMonoBehaviour {

    Transform target;
    BattlegroundController controller;
    EnemyMove movement;

    public int life = 10;
    public float attackDelay = 5;
    public int damage;
    public int xpReward;
    public float startDelay = 3f;

    /// <summary>
    /// after colliding and attacking the avatar, the same wont happen for at least attackDelay amount of time
    /// </summary>
    public bool isAttackCooldown = false;
    IEnemyMove mover;

    bool isKilled = false;

    MobSpawnerManager manager;

    void Awake() {
        movement = GetComponent<EnemyMove>();
    }

    void Start() {
        GameObject obj = GameObject.FindGameObjectWithTag(Tags.BattlegroundController);
        if (obj == null) {
            Debug.LogError("ERROR: coulndt find tag Battleground Controller");
        } else {
            controller = obj.GetComponent<BattlegroundController>();
            setUp();

            Invoke(() => {
                movement.SetUp(target);
            }, startDelay);          
        }
    }

    public void SetProperties(MobSpawnerManager manager) {
        this.manager = manager;
    }

    void setUp() {
        target = controller.avatar.transform;
        transform.SetParent(controller.mobParent);
        transform.SetY(0);
    }

    public void GetHit(Bullet bullet) {
        life -= bullet.damage;
        if (life <= 0) {
            mobKilled();
        }
    }

    public void PerformAttack() {
        movement.PerformAttack(attackDelay);
        StartCoroutine(attackCooldownCo());
    }

    IEnumerator attackCooldownCo() {
        isAttackCooldown = true;
        yield return new WaitForSeconds(attackDelay);
        isAttackCooldown = false;
    }

    void mobKilled() {
        if (isKilled == false) {
            isKilled = true;
            GlobalController.instance.saveFile.character.EarnXP(xpReward);
            manager.RemoveMobReference(this);
            Destroy(gameObject);
            GlobalController.instance.eventRegistery.ActivateEvent(EEventRegistrationType.KILL_SOMETHING);
        }
    }

}
