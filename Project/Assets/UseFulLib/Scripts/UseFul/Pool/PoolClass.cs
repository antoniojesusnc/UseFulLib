using System.Collections;
using System.Collections.Generic;
using System;

namespace USEFUL {
    public class PoolClass {

        Dictionary<System.Type, IList> _elements;

        public PoolClass() {
            _elements = new Dictionary<System.Type, IList>();
        } // initPoolSystem

        public void InitPoolType<T>(int initialAmount){
            if(!isClassAllowed(typeof(T))) return;


            IList list;
            if (_elements.TryGetValue(typeof(T), out list)) {
                UnityEngine.Debug.LogWarning("The pool was already created");
                if (list.Count < initialAmount) {
                    for (int i = initialAmount - list.Count - 1; i >= 0; --i) {
                        list.Add(Activator.CreateInstance<T>());
                    }
                }
                return;
            }

            list = new List<T>(initialAmount);
            for (int i = initialAmount - 1; i >= 0; --i) {
                list.Add(Activator.CreateInstance<T>() );
            }

            _elements.Add(typeof(T), list);
        } // initPool

        public T GetObject<T>() {
            if (!isClassAllowed(typeof(T))) return default(T);

            IList list;
            int numberElements = 0;
            if (_elements.TryGetValue(typeof(T), out list)) {
                numberElements = list.Count;
                if (numberElements <= 0) {
                    UnityEngine.Debug.LogWarning("Pool " + typeof(T).ToString() + " Empty, Increasing the size in 5 elements");
                    //for (int i = 5; i >= 0; --i) {
                    ++numberElements;
                    list.Add(Activator.CreateInstance<T>());
                    //}
                }
                T result = (T)list[numberElements - 1];
                list.RemoveAt(numberElements - 1);
                return result;
            }

            UnityEngine.Debug.LogWarning("Pool " + typeof(T).ToString() + " Not exist in the Pool");
            return default(T);
        } // getObject

        public void FreeObject<T>(T objectToFree) {
            if (!isClassAllowed(typeof(T))) return;

            IList list;
            if (_elements.TryGetValue(typeof(T), out list)) {
                list.Add(objectToFree);
            } else {
                UnityEngine.Debug.LogWarning("Pool " + typeof(T).ToString() + " Not exist in the Pool");
            }
        } // FreeObject

        private bool isClassAllowed(Type type) {
            if (type.IsSubclassOf(typeof(UnityEngine.MonoBehaviour))) {
                UnityEngine.Debug.LogWarning("Class not Allowed, use class that not extend from MonoBehavior. If the class extend MonoBehaviour, use PoolGameObect instead of PoolClass");
                return false;
            } else {
                return true;
            }
        } // isClassAllowed
    }
}