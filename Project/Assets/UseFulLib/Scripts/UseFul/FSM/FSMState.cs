using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace USEFUL {

    public abstract class FSMState<T> where T : System.IComparable{

        protected FSMController _controller;
        private List<FSMTransition<T>> _transitions;
        private T _state;

        public FSMState(FSMController controller, T state) {
            _transitions = new List<FSMTransition<T>>();
            _controller = controller;
            _state = state;
        } // FSMState

        public virtual void OnActivate() {

        } // OnActivate

        public virtual void Update(float dt) {

        } // Update

        public virtual void OnDeactivate() {

        } // OnDeactivate


    // Transition methods
        public void AddTransition(FSMTransition<T> newTransition){
            _transitions.Add(newTransition);
        } // AddTransition

        public void RemoveTransition(FSMTransition<T> removeTransition) {
            _transitions.Remove(removeTransition);
        } // RemoveTransition

        public void RemoveTransition(T removeTransition) {
            for (int i = _transitions.Count; i >= 0; --i) {
                if (_transitions[i].GetNextState().CompareTo(removeTransition) == 0){
                    _transitions.RemoveAt(i);
                    return;
                }
            }
        } // RemoveTransition

        // gets & sets
        public List<FSMTransition<T>> GetTransitions() {
            return _transitions;
        } //ChangeState   

        public T GetState() {
            return _state;
        } // GetState


    }
}