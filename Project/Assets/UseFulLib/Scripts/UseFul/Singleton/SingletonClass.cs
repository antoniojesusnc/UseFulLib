using UnityEngine;

using System;


public class SingletonClass<T> where T : class {

    private static T _instance = null;

    // private members
    public static T GetPtr() {
        // check if already exist
        if (_instance == null) {
            // if not, create a instance
            _instance = Activator.CreateInstance<T>();
        }
        return _instance;
    } // getPtr
}