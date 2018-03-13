using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace USEFUL {
    public class PoolGameObject {

        private Dictionary<string, PoolGameObjectInfo> _elements;
        private Transform _container;

        public PoolGameObject() {
            _elements = new Dictionary<string, PoolGameObjectInfo>();
            _container = new GameObject("PoolContainer").transform;
        } // initPoolSystem

        public void InitPoolGameObject(GameObject prefab, string namePrefab, int initialAmount, bool replace = false) {
            PoolGameObjectInfo poolInfo;
            if (_elements.TryGetValue(namePrefab, out poolInfo)) {
                if (prefab == poolInfo.prefab) {
                    Debug.LogWarning("The pool was already created");
                    if (poolInfo.elements.Count < initialAmount) {
                        GameObject go;
                        for (int i = initialAmount - poolInfo.elements.Count - 1; i >= 0; --i) {
                            go = GameObject.Instantiate<GameObject>(prefab);
                            go.transform.SetParent(_container);
                            go.transform.localScale = Vector3.one;
                            poolInfo.elements.Add(go);
                        }
                    }
                } else {
                    if (!replace) {
                        Debug.LogWarning("Name prefab used for another prefab. Choose another namePrefab to save or set replace = true for replaceOld prefab");
                    } else {
                        _elements.Remove(namePrefab);
                    }
                }
                return;
            }

            poolInfo = new PoolGameObjectInfo();
            List<GameObject> list = new List<GameObject>();
            GameObject tempGo;
            for (int i = initialAmount - 1; i >= 0; --i) {
                tempGo = GameObject.Instantiate<GameObject>(prefab);
                tempGo.transform.SetParent(_container);
                list.Add(tempGo);
            }

            poolInfo.prefab = prefab;
            poolInfo.elements = list;
            _elements.Add(namePrefab, poolInfo);
        } // InitPoolGameObject
        
        public GameObject GetGameObject(string namePrefab) {
            PoolGameObjectInfo poolInfo;
            int numberElements = 0;
            if (_elements.TryGetValue(namePrefab, out poolInfo)) {
                numberElements = poolInfo.elements.Count;
                if (numberElements <= 0) {
                    Debug.LogWarning("Pool " + namePrefab + " Empty, Increasing the size in 1 elements");
                    //for (int i = 5; i >= 0; --i) {
                        ++numberElements;
                        GameObject tempGo = GameObject.Instantiate<GameObject>(poolInfo.prefab);
                        tempGo.transform.SetParent(_container);
                        poolInfo.elements.Add(tempGo);
                    //}
                }
                GameObject result = poolInfo.elements[numberElements - 1];
                poolInfo.elements.RemoveAt(numberElements - 1);
                return result;
            }

            Debug.LogWarning("Pool with name " + namePrefab + " Not exist in the PoolGameObject");
            return null;
        } // GetGameObject

        public void FreeGameObject(string namePrefab, GameObject gameObjectToFree) {
            PoolGameObjectInfo poolInfo;
            if (_elements.TryGetValue(namePrefab, out poolInfo)) {
                poolInfo.elements.Add(gameObjectToFree);
            } else {
                Debug.LogWarning("Pool with name " + namePrefab + " Not exist in the PoolGameObject");
            }
        } // FreeObject

        private struct PoolGameObjectInfo {
            public GameObject prefab;
            public List<GameObject> elements;
        } // PoolGameObjectInfo 
    }

    
}