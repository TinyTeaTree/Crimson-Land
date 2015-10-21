using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoginMenuController : MonoBehaviour {

    public GameObject startButton, deleteUserButton;

    public InputField inputField;
    public UserCreateElement userElement;
    public Color normalImageColor, selectedImageColor;
    Image currentlySelectedImage = null;
    UserData currentlySelectedUser;

    List<UserCreateElement> usersInTable = new List<UserCreateElement>();

    void Start() {
        startButton.SetActive(false);
        deleteUserButton.SetActive(false);

        StartCoroutine(waitForProfileFragmentToLoad());
    }

    public void GoToMainMenu() {
        if (currentlySelectedUser == null) {
            Debug.LogError("ERROR START: no selected user, and yet start was pressed");
            return;
        }

        GlobalController.instance.saveFile.Reset();
        GlobalController.instance.saveFile.Load(currentlySelectedUser.username);
        GlobalController.instance.transitionery.GoTo(TagsLocations.mainMenu);
    }

    public void PressedCreateUser() {

        string username = inputField.text;

        if (string.IsNullOrEmpty(username)) {
            //todo add error message that field is empty
            return;
        }

        if (GlobalController.instance.systemFile.usersFragment.HasUsername(username)) {
            //todo add erro message that already such username exists
            return;
        }

        if (string.IsNullOrEmpty(username) == false) {
            createNewUserElement(username);
            GlobalController.instance.saveFile.Reset();
            GlobalController.instance.saveFile.profile.id = username;
            GlobalController.instance.saveFile.Save();

            GlobalController.instance.systemFile.usersFragment.AddUsername(username);
        }
    }

    public void PressedUserSelected(string username, Image image) {
        List<UserData> userList = GlobalController.instance.systemFile.usersFragment.GetUsernames();

        UserData choosenUser = null;
        for (int i=0; i<userList.Count; ++i) {
            if (userList[i].username == username) {
                choosenUser = userList[i];
                break;
            }
        }

        if (choosenUser != null) {
            if (choosenUser == currentlySelectedUser) {
                currentlySelectedImage.color = normalImageColor;
                currentlySelectedImage = null;
                currentlySelectedUser = null;
                startButton.gameObject.SetActive(false);
                deleteUserButton.gameObject.SetActive(false);
                return;
            }

            startButton.gameObject.SetActive(true);
            deleteUserButton.gameObject.SetActive(true);
            image.color = selectedImageColor;
            if (currentlySelectedImage != null) {
                currentlySelectedImage.color = normalImageColor;
            }
            currentlySelectedImage = image;
            currentlySelectedUser = choosenUser;
        } else {
            Debug.LogError("Couldn't find such user");
        }
    }

    public void PressedDeleteUser() {
        if (currentlySelectedUser == null) {
            Debug.LogError("ERROR DELETE USER: can't reach here without selected user");
            return;
        }

        startButton.gameObject.SetActive(false);
        deleteUserButton.gameObject.SetActive(false);
        Destroy(currentlySelectedImage.gameObject);
        GlobalController.instance.systemFile.usersFragment.DeleteUsername(currentlySelectedUser.username);
        GlobalController.instance.saveFile.DeleteUser(currentlySelectedUser.username);
        currentlySelectedUser = null;
        currentlySelectedImage = null;
    }
    IEnumerator waitForProfileFragmentToLoad() {
        while (GlobalController.instance.systemFile.usersFragment.isInitialized == false) {
            yield return null;
        }

        List<UserData> userList = GlobalController.instance.systemFile.usersFragment.GetUsernames();
        for (int i=0; i<userList.Count; ++i) {
            createNewUserElement(userList[i].username);
        }
    }

    void createNewUserElement(string username) {
        UserCreateElement newUserElement = Instantiate(userElement);
        usersInTable.Add(newUserElement);
        newUserElement.transform.SetParent(userElement.transform.parent);
        newUserElement.transform.ResetTransform();
        newUserElement.gameObject.SetActive(true);
        newUserElement.nameText.text = username;
    }

}