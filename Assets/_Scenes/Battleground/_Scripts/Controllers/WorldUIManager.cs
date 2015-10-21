using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WorldUIManager : MyMonoBehaviour {

    public RectTransform damageTextParent;
    public RectTransform originalDamageText;

    List<RectTransform> poolOfTexts = new List<RectTransform>();

    public void PresentDamageText(int damage, Vector3 position) {
        position.y = transform.position.y * 2;

        RectTransform newText = null;
        if (poolOfTexts.Count > 0) {
            newText = poolOfTexts[poolOfTexts.Count - 1];
            poolOfTexts.RemoveAt(poolOfTexts.Count - 1);
        } else {
            newText = Instantiate(originalDamageText);
            newText.transform.SetParent(damageTextParent);

        }


        newText.gameObject.SetActive(true);
        newText.transform.localScale = Vector3.one;
        newText.transform.localRotation = Quaternion.identity;
        newText.position = position;
        newText.GetComponentInChildren<Text>().text = damage.ToString();
        newText.GetComponent<Animator>().SetTrigger("Flow");

        Invoke(() => {
            //newText.gameObject.SetActive(false);
            poolOfTexts.Add(newText);
        }, 5f);
    }


}
