using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour {

    public static GlobalController instance;

    [HideInInspector]
    public EventRegistery eventRegistery;
    [HideInInspector]
    public SaveFile saveFile;
    [HideInInspector]
    public SystemFile systemFile;
    [HideInInspector]
    public DataFile dataFile;
    [HideInInspector]
    public TransitionManager transitionery;
    [HideInInspector]
    public GlobalView view;

    [HideInInspector]
    public int battleChoosen = -1;

#if UNITY_EDITOR
    public string devUsername;
#endif

    bool isSetToDestroy = false;
    void Awake() {
        if (instance != null && instance != this) {
            isSetToDestroy = true;
            Destroy(gameObject);
        } else {
            instance = this;
        }

        eventRegistery = GetComponentInChildren<EventRegistery>();
        saveFile = GetComponentInChildren<SaveFile>();
        systemFile = GetComponentInChildren<SystemFile>();
        dataFile = GetComponentInChildren<DataFile>();
        transitionery = GetComponentInChildren<TransitionManager>();
        view = GetComponentInChildren<GlobalView>();
    }

    void Start() {
        if (isSetToDestroy == false) {
            systemFile.Load();

#if UNITY_EDITOR
            if (Application.loadedLevelName != TagsLocations.loginMenu.sceneName) {
                saveFile.Load(devUsername);
            }
#endif
        }
    }

    void OnApplicationQuit() {
        SaveGame();
    }

#if UNITY_EDITOR
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ExitGame();
        }
    }
#endif

    public void SaveGame() {
        systemFile.Save();
        saveFile.Save();
    }

    public void ExitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif        
    }
}
