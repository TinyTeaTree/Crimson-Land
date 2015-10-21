using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// This class will provide the facilities for the save file to save stuff into persistant storage.
/// </summary>
public class PersistanceDriver : MonoBehaviour {

    public string ending;

    public void SaveFragment(string folderPath, string fragmentName, object fragment) {
        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Application.persistentDataPath, folderPath));

        BinaryFormatter formater = new BinaryFormatter();
        FileStream stream = File.Create(getPath(folderPath, fragmentName));
        formater.Serialize(stream, fragment);
        stream.Close();
    }

    public object LoadFragment(string folderPath, string fragmentName) {
        if( Directory.Exists(System.IO.Path.Combine(Application.persistentDataPath, folderPath))){
            if (File.Exists(getPath(folderPath, fragmentName)) == true) {
                BinaryFormatter formater = new BinaryFormatter();
                FileStream stream = File.Open(getPath(folderPath, fragmentName), FileMode.Open);
                object returnObject = formater.Deserialize(stream);
                stream.Close();
                return returnObject;
            } else {
                Debug.LogWarning("File does not exist " + getPath(folderPath, fragmentName));
                return null;
            }
        }else{
            Debug.LogWarning("Folder does not exist " + System.IO.Path.Combine(Application.persistentDataPath, folderPath));
            return null;
        }
    }

    public void DeleteFragment(string folderName, string fragmentName) {
        File.Delete(getPath(folderName, fragmentName));
    }

    public void DelteFolder(string folderPath) {
        Directory.Delete(System.IO.Path.Combine(Application.persistentDataPath, folderPath), false);
    }

    string getPath(string folderName, string fileName) {
        string folderPath = System.IO.Path.Combine(Application.persistentDataPath, folderName);
        return System.IO.Path.Combine(folderPath, fileName + "." + ending);
    }

}
