using UnityEngine;
using System.Collections;
using System.IO;

public class DataFromCSV {

    public DataFromCSV(string path) {
        LoadFromFile(path);
    } // DataFromCSV

    private void LoadFromFile(string path) {
        
        string fullPath = Application.streamingAssetsPath+path;

        Debug.LogWarning(fullPath);

        if(!File.Exists(fullPath)){
            Debug.LogWarning(path +" Not Exist");
            return;
        }
        string[] lines = System.IO.File.ReadAllLines(fullPath);
        

        Debug.Log(lines.ToString());
    } // LoadFromFile
}