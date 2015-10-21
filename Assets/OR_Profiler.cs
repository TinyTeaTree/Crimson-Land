using UnityEngine;
using System.Collections;

public class OR_Profiler : MonoBehaviour {

    public bool printDetails = false;
    double total = 0;

    void Update() {
        if (Input.GetKeyDown(KeyCode.KeypadDivide)) {
            debugness();
        }
    }

    [ContextMenu("Debug")]
    void debugness() {
        Debug.developerConsoleVisible = true;
        Debug.LogError("showing console");
    }

    [ContextMenu("Clean")]
    void Cleanup() {
        StartCoroutine(Clean());
    }

    [ContextMenu("Delete Globas and Clean")]
    void CleanupAndDelete() {
        Debug.Log(((double)Profiler.GetTotalAllocatedMemory() / 1024 / 1024).ToString("0.00") + " MB ");

        if (GlobalController.instance != null) {
            DestroyImmediate(GlobalController.instance.transform.parent.gameObject);
        }
        StartCoroutine(Clean());
    }

    IEnumerator Clean() {
        Debug.Log(((double)Profiler.GetTotalAllocatedMemory() / 1024 / 1024).ToString("0.00") + " MB ");

        yield return Resources.UnloadUnusedAssets();
        System.GC.Collect();
        yield return new WaitForSeconds(5);
        Debug.Log(((double)Profiler.GetTotalAllocatedMemory() / 1024 / 1024).ToString("0.00") + " MB ");


    }

    //	[ContextMenu("some")]
    //	
    //	void some ()
    //	{
    //		total = 0;
    //
    //		GetTypeSizeInMemory (typeof(Object));
    ////		GetTypeSizeInMemory2 (typeof(Object));
    //
    //
    //		
    //		Debug.Log ("Scaned Only  " + (total / Profiler.GetTotalAllocatedMemory () * 100) + "% of used memory");
    //		
    //	}

    [ContextMenu("All")]

    void tester() {
        total = 0;
        GetTypeSizeInMemory(typeof(Texture));
        GetTypeSizeInMemory(typeof(Material));
        GetTypeSizeInMemory(typeof(Mesh));
        GetTypeSizeInMemory(typeof(AudioClip));
        GetTypeSizeInMemory(typeof(Animator));
        GetTypeSizeInMemory(typeof(AnimationClip));
        GetTypeSizeInMemory(typeof(MonoBehaviour));
#if UNITY_WEBPLAYER
		GetTypeSizeInMemory (typeof(MovieTexture));
#endif
        Debug.Log("Scaned Only  " + (total / Profiler.GetTotalAllocatedMemory() * 100) + "% of used memory");

    }


    //	void GetTypeSizeInMemory2(System.Type sType){
    //		
    //		
    //		if (sType == typeof(AudioClip)) {
    //			//GetSizeInMemoryAudioClip ();
    //			
    //		} else {
    //			
    //			Object[] objects = Object.FindObjectsOfType (sType);
    //			double size = GetSizeInMemory (objects, false);
    //			string sizetring = (size / 1024 / 1024).ToString ("0.00") + " MB";
    //			int count = objects.Length;
    //			string percent = (size / (float)(Profiler.GetTotalAllocatedMemory ()) * 100).ToString ("0.00") + "% Out of => " + ((double)Profiler.GetTotalAllocatedMemory () / 1024 / 1024).ToString ("0.00") + " MB ";
    //			//print (percent);
    //			//		string precentstring = precent.ToString ("0.00");
    //			total +=size;
    //			Debug.Log (sType.ToString ().Replace ("UnityEngine.", "") + " " + count + " / " + sizetring + " = " + percent);
    //		}
    //	}


    void GetTypeSizeInMemory(System.Type sType) {

        float size = 0;
        Object[] objects;
        if (sType == typeof(AudioClip)) {
            objects = Resources.FindObjectsOfTypeAll(typeof(AudioClip));
            foreach (AudioClip clip in objects) {
                size += clip.samples * 4;
            }

        } else {

            objects = Resources.FindObjectsOfTypeAll(sType);
            size = (float)GetSizeInMemory(objects);
        }
        string sizetring = (size / 1024 / 1024).ToString("0.00") + " MB";
        int count = objects.Length;
        string percent = "<color=yellow>" + (size / (float)(Profiler.GetTotalAllocatedMemory()) * 100).ToString("0.00") + "%</color> =>" + ((double)Profiler.GetTotalAllocatedMemory() / 1024 / 1024).ToString("0.00") + " MB ";
        //print (percent);
        //		string precentstring = precent.ToString ("0.00");
        total += size;
        Debug.Log(sType.ToString().Replace("UnityEngine.", "") + " " + count + " / " + sizetring + " = " + percent);

    }

    double GetSizeInMemory(Object[] stuff) {
        double size = 0;
        foreach (Object i in stuff) {
            if (printDetails) {
                Debug.Log(i.name + " ===> <color=green>" + ((float)Profiler.GetRuntimeMemorySize(i) / 1024/1024f).ToString("0.00") + "</color> MB");
            }
            size += (Profiler.GetRuntimeMemorySize(i));
        }
        return size;
    }

}