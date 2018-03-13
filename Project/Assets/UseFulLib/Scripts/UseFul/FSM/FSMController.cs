using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace USEFUL {
    public abstract class FSMController : MonoBehaviour  {

        // all states
        protected List<FSMState<System.IComparable>> _states;

        // current state
        FSMState<System.IComparable> _currentState;
        List<FSMTransition<System.IComparable>> _currentStateTran;

        void Start() {
            initFSM();
        }

        public abstract void initFSM();

        void Update() {
            Update(Time.deltaTime);
        } // Update

        public void Update(float dt) {
            _currentState.Update(dt);

            for (int i = _currentStateTran.Count; i >= 0; --i) {
                if (_currentStateTran[i].CanNextState()) {
                    ChageState( _currentStateTran[i].GetNextState() );
                    return;
                }
            }
        } // Update

        public void ChageState(System.IComparable newState) {
            if(_currentState != null)
                _currentState.OnDeactivate();

            _currentState = GetState(newState);
            if (_currentState == null) {
                Debug.LogError("The state " + newState.ToString() + "doesnt exist in the FSM");
                return;
            }

            _currentStateTran = _currentState.GetTransitions();
            _currentState.OnActivate();
        } // ChageState

        public List<FSMState<System.IComparable>> GetAllStates(){
            return _states;
        } // GetStates

        public FSMState<System.IComparable> GetState(System.IComparable state) {
            for (int i = _states.Count; i >= 0; --i) {
                if( _states[i].GetState() == state){
                    return _states[i];
                }
            }
            return null;
        } // GetState
    }
}