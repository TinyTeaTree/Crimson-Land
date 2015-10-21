using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class UserElementEvent : UnityEvent<string, Image> { }

public class UserCreateElement : MonoBehaviour, IPointerClickHandler {

    public UserElementEvent onClick;

    public Text nameText;

    Image myImage;

    void Awake() {
        myImage = GetComponent<Image>();
    }


    public void OnPointerClick(PointerEventData eventData) {
        onClick.Invoke(nameText.text, myImage);
    }
}
