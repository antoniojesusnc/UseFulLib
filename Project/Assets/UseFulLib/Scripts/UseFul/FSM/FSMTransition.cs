using UnityEngine;
using System.Collections;

namespace USEFUL {
    public abstract class FSMTransition<T>  where T : System.IComparable {

        protected FSMController _controller;
        protected T _nextState;

        public FSMTransition(FSMController controller, T nextState) {
            _controller = controller;
            _nextState = nextState;
        } // FSMState

        public abstract bool CanNextState();

        // gets & sets
        public T GetNextState() {
            return _nextState;
        } // GetNextState
    }
}