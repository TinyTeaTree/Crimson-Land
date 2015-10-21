using UnityEngine;
using System.Collections;

public class MainMenuController : MyMonoBehaviour {

    public CanvasGroup choiceStateGroup, battleStateGroup, characterGroup, gearGroup;
    CharacterChoice characterChoice;
    GearChoice gearChoice;
    BattlesChoice battlesChoice;

    int isTransitioning = 0;

    public float fadeOutTime, fadeInTime;

    void Start() {

        characterChoice = characterGroup.GetComponent<CharacterChoice>();
        gearChoice = gearGroup.GetComponent<GearChoice>();
        battlesChoice = battleStateGroup.GetComponent<BattlesChoice>();

        choiceStateGroup.interactable = choiceStateGroup.blocksRaycasts = true;
        choiceStateGroup.alpha = 1;
        battleStateGroup.alpha = 0;
        battleStateGroup.interactable = battleStateGroup.blocksRaycasts = false;
        characterGroup.alpha = 0;
        characterGroup.interactable = characterGroup.blocksRaycasts = false;
        gearGroup.alpha = 0;
        gearGroup.interactable = gearGroup.blocksRaycasts = false;

    }

    public void PressedBattleground() {
        if (isTransitioning == 0) {
            StartCoroutine(fadeOut(choiceStateGroup));
            Invoke(() => {
                battlesChoice.SetUp();
                StartCoroutine(fadeIn(battleStateGroup));
            }, 0.1f);
        }
    }

    public void PressedExitBattleground() {
        if (isTransitioning == 0) {
            StartCoroutine(fadeOut(battleStateGroup));
            Invoke(() => {
                StartCoroutine(fadeIn(choiceStateGroup));
            }, 0.1f);
        }
    }

    public void PressedCharacter() {
        if (isTransitioning == 0) {
            StartCoroutine(fadeOut(choiceStateGroup));
            Invoke(() => {
                characterChoice.SetUp();
                StartCoroutine(fadeIn(characterGroup));
            }, 0.1f);
        }
    }

    public void PressedExitCharacter() {
        if (isTransitioning == 0) {
            StartCoroutine(fadeOut(characterGroup));
            Invoke(() => {
                StartCoroutine(fadeIn(choiceStateGroup));
            }, 0.1f);
        }
    }

    public void PressedGear() {
        if (isTransitioning == 0) {
            StartCoroutine(fadeOut(choiceStateGroup));
            Invoke(() => {
                gearChoice.SetUp();
                StartCoroutine(fadeIn(gearGroup));
            }, 0.1f);
        }
    }

    public void PressedExitGear() {
        if (isTransitioning == 0) {
            StartCoroutine(fadeOut(gearGroup));
            Invoke(() => {
                StartCoroutine(fadeIn(choiceStateGroup));
            }, 0.1f);
        }
    }

    public void GoToBattleTraining(int choosenBattle) {
        GlobalController.instance.battleChoosen = choosenBattle;
        GlobalController.instance.transitionery.GoTo(TagsLocations.battleground);
    }

    IEnumerator fadeIn(CanvasGroup group) {
        isTransitioning++;
        float timePassed = 0;

        while (timePassed < fadeInTime) {

            float ratio = timePassed / fadeInTime;

            group.alpha = ratio;

            timePassed += Time.deltaTime;
            yield return null;
        }

        group.alpha = 1;
        group.interactable = group.blocksRaycasts = true;
        isTransitioning--;
    }

    IEnumerator fadeOut(CanvasGroup group) {
        isTransitioning++;
        float timePassed = 0;
        group.interactable = group.blocksRaycasts = false;

        while (timePassed < fadeOutTime) {

            float ratio = 1 - timePassed / fadeOutTime;

            group.alpha = ratio;

            timePassed += Time.deltaTime;
            yield return null;
        }

        group.alpha = 0;
        isTransitioning--;
    }

}
