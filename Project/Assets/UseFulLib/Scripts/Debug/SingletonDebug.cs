using UnityEngine;
using System.Collections;

public class SingletonDebug : SingletonClass<SingletonDebug> {

    public int text() {
        return 1;
    }
}