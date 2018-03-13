using UnityEngine;
using System.Collections;

public class MonoBehaviourSingletonDebug : SingletonMonoBehaviour<MonoBehaviourSingletonDebug> {

    public int text() {
        return 2;
    }
}