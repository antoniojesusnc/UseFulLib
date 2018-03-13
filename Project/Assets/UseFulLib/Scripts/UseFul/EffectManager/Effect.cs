using UnityEngine;
using System.Collections;

namespace USEFUL {

    public class GenericEffect {


        public delegate void EffectDelegate();

        public EffectDelegate OnInit;
        public EffectDelegate OnUpdate;
        public EffectDelegate OnFinish;

        protected Component _component;

        protected float _timeStamp;
        protected float _duration;
        protected bool _isInfinite;

        // create New Inits with more parameters
        public virtual void Init(Component component, float duration = 0, 
            EffectDelegate OnInitEffect = null,
            EffectDelegate OnUpdateEffect = null,
            EffectDelegate OnFinishEffect = null) {

            EffectManager.GetPtr().RegisterEffect(component, this);

            OnInit += OnInitEffect;
            OnUpdate += OnUpdateEffect;
            OnFinish += OnFinishEffect;

            _component = component;
            _duration = duration;


            if (duration == 0)
                _isInfinite = true;

            if (OnInit != null) OnInit();
        } // Init

        public virtual void Update(float dt) {
            _timeStamp += _timeStamp;

            if (OnUpdate != null) OnUpdate();
        } // Update

        public virtual void Finish() {
            if (_isInfinite) {
                Init(_component, _duration);
                return;
            }

            if (OnFinish != null) OnFinish();
        } // Finish

        // gets & sets

        public bool IsFinish() {
            return _isInfinite ? false : _timeStamp >= _duration;
        } // IsFinish

        public bool isInfinite() {
            return _isInfinite;
        } // isInfinite


        public Component GetComponent() {
            return _component;
        } // GetComponent
    } // GenericEffect
}