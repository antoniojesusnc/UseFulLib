using UnityEngine;
using System.Collections;
using USEFUL;

public class DebugInput : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log(SingletonDebug.GetPtr().text());
        } // Q Down

        if (Input.GetKeyDown(KeyCode.W)) {
            Debug.Log(MonoBehaviourSingletonDebug.GetPtr().text());
        } // Q Down

        if (Input.GetKeyDown(KeyCode.E)) {
            new DataFromCSV("/Excels/buildings.csv");
        } // Q Down

        if (Input.GetKeyDown(KeyCode.A)) {
            PoolClass pool = new PoolClass();
            pool.InitPoolType<DebugInput>(1);

            var temp2 = pool.GetObject<pepe>();
            var temp3 = pool.GetObject<pepe>();

            pool.FreeObject<pepe>(temp2);
            temp3 = pool.GetObject<pepe>();
        } // Q Down

        if (Input.GetKeyDown(KeyCode.S)) {
            PoolGameObject pool = new PoolGameObject();
            pool.InitPoolGameObject(gameObject,"DebugInput", 1);

            var temp2 = pool.GetGameObject("DebugInput");
            var temp3 = pool.GetGameObject("DebugInput");

            pool.FreeGameObject("DebugInput", temp2);
        } // Q Down
	}

    private class pepe{
        int no = 5;
        public pepe() {
            no = 8;
        }
    }
}