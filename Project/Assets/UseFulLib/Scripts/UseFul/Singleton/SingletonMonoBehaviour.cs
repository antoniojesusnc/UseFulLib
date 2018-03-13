using UnityEngine;
using System.Collections;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour{

    // private members
    private static T _instance = null;

    public static T GetPtr() {
        if (_instance == null) {
            // firts try to find all object type T
            var temp = FindObjectsOfType<T>();
            if (temp.Length == 0) {
                // if the code dont have, we create a new GameObjet with this component
                GameObject go = new GameObject(typeof(T).ToString());
                _instance = go.AddComponent<T>();

            } else if (temp.Length >= 1){
                // if more that one, assing and return
                _instance = temp[0];
                if (temp.Length > 1) {
                    // if there are more that one
                    Debug.LogWarning("// Multiples instances of " + typeof(T));
                }
            }
        }
        return _instance;
    } // getPtr
	
}