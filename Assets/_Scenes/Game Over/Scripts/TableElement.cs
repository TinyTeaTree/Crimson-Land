using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TableElement : MonoBehaviour {

    public Text nameText, scoreText, placementText;

    public void SetProperties(string name, int score, int placement) {
        nameText.text = name + ":";
        scoreText.text = score.ToString();
        placementText.text = "#" + placement.ToString();
    }

}
