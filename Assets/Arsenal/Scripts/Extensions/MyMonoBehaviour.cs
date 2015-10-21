using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class MyMonoBehaviour : MonoBehaviour {

    public Vector3 pos {
        get { return transform.position; }
        set { transform.position = value; }
    }
    public Quaternion rot {
        get { return transform.rotation; }
        set { transform.rotation = value; }
    }
    public Vector3 scale {
        get { return transform.localScale; }
        set { transform.localScale = value; }
    }
    public Vector3 lPos {
        get { return transform.localPosition; }
        set { transform.localPosition = value; }
    }
    public Quaternion lRot {
        get { return transform.localRotation; }
        set { transform.localRotation = value; }
    }

    public void Invoke(System.Action task, float time) {
        StartCoroutine(InvokeCo(task, time));
    }
    IEnumerator InvokeCo(System.Action task, float time) {
        yield return new WaitForSeconds(time);
        if (task != null) {
            task();
        }
    }

    public I GetInterfaceComponent<I>() where I : class {
        return GetComponent(typeof(I)) as I;
    }

    public static List<GameObject> FindObjectsOfInterface<I>() where I : class {
        MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>();
        List<GameObject> list = new List<GameObject>();

        foreach (MonoBehaviour behaviour in monoBehaviours) {
            I component = behaviour.GetComponent(typeof(I)) as I;

            if (component != null) {
                if (list.Contains(behaviour.gameObject) == false) {
                    list.Add(behaviour.gameObject);
                }
            }
        }

        return list;
    }

    public static List<I> FindComponentsOfInterface<I>() where I : class {
        MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>();
        List<I> list = new List<I>();

        foreach (MonoBehaviour behaviour in monoBehaviours) {
            I component = behaviour.GetComponent(typeof(I)) as I;

            if (component != null) {
                if( !list.Contains(component) )
                    list.Add(component);
            }
        }

        return list;
    }

    public T GetSafeComponent<T>() where T : Component {
        T component = gameObject.GetComponent<T>();

        if (component == null) {
            Debug.LogError("Expected to find component of type " + typeof(T) + " but found none in ", gameObject);
        }

        return component;
    }
	
}



