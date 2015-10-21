using UnityEngine;
using System.Collections;

public class ProfileInfo : MonoBehaviour {

    [Header("Application Data")]
    public string companyName = "";
    public string networkReachability = "";
    public string persistentDataPath = "";
    public string temporaryCachePath = "";
    public string platform = "";
    public string productName = "";
    public string systemLanguage = "";
    public string unityVersion = "";
    public string version = "";
    public int targetFrameRate = -1;

    [Header("Profile Data")]
    public long monoHeapSize;
    public long monoUsedSize;
    public long totalAllocatedMemory;
    public long totalReservedMemory;
    public long totalUnusedReservedMemory;

    [Header("System.GC Data")]
    public long totalMemory;

    [Header("Others")]
    public int fps;

    int frames;
    float delta;

    void OnLevelWasLoaded() {
        loadApplicationData();
        loadProfileData();
        loadGCData();
    }

    void Start() {
        loadApplicationData();
        loadProfileData();
        loadGCData();

        delta = Time.time;
        frames = 0;
    }

    void Update() {
        frames++;
        if (delta + 1 < Time.time) {
            delta = Time.time;
            fps = frames;
            frames = 0;
        }
    }

    void loadApplicationData() {
        companyName = Application.companyName;
        networkReachability = Application.internetReachability.ToString();
        persistentDataPath = Application.persistentDataPath;
        platform = Application.platform.ToString();
        productName = Application.productName;
        systemLanguage = Application.systemLanguage.ToString();
        targetFrameRate = Application.targetFrameRate;
        temporaryCachePath = Application.temporaryCachePath;
        unityVersion = Application.unityVersion;
        version = Application.version;     
    }

    void loadProfileData() {
        monoHeapSize = Profiler.GetMonoHeapSize();
        monoUsedSize = Profiler.GetMonoUsedSize();
        totalAllocatedMemory = Profiler.GetTotalAllocatedMemory();
        totalReservedMemory = Profiler.GetTotalReservedMemory();
        totalUnusedReservedMemory = Profiler.GetTotalUnusedReservedMemory();      
    }

    void loadGCData() {
        totalMemory = System.GC.GetTotalMemory(true);
    }

}