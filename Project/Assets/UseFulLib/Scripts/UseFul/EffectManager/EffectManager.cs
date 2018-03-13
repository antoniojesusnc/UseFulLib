using System;
using System.Collections;
using System.Collections.Generic;


namespace USEFUL {

    public class EffectManager : SingletonClass<EffectManager> {

        PoolClass _pool;

        Dictionary<UnityEngine.Component, List<GenericEffect>> _effectsByComponent;
        List<GenericEffect> _currentEffects;

        List<GenericEffect> _effectToDelete;

        public EffectManager() {
            _pool = new PoolClass();
        }

        public void init() {

            UnityEngine.Transform temp = null;
            EffectManager.GetPtr().CreateNewEffect<GenericEffect>().Init(temp);


            _effectsByComponent = new Dictionary<UnityEngine.Component, List<GenericEffect>>();
            _currentEffects = new List<GenericEffect>();
            _effectToDelete = new List<GenericEffect>();

            //EffectManager.GetPtr().NewEffect(GenericEffect, temp )).Init();
        }

        public T CreateNewEffect<T>() {
            return _pool.GetObject<T>();
        } // CreateNewEffect

        internal void RegisterEffect(UnityEngine.Component component, GenericEffect effect) {
            List<GenericEffect> list;
            if (!_effectsByComponent.TryGetValue(component, out list)) {
                list.Add(effect);
                return;
            }
            list = new List<GenericEffect>();
            list.Add(effect);
            _effectsByComponent.Add(component, list);
            _currentEffects.Add(effect);
        } // RegisterEffect

        // Effects Flow
        public void Update(float dt) {
            for (int i = _currentEffects.Count - 1; i >= 0; --i) {

                _currentEffects[i].Update(dt);

                if (_currentEffects[i].IsFinish()) {
                    _effectToDelete.Add(_currentEffects[i]);
                }
            }


            for (int i = _effectToDelete.Count - 1; i >= 0; --i) {
                RemoveEffect(_effectToDelete[i]);
            }

        } // Update

        public void RemoveEffect(GenericEffect effectToRemove, bool finishIfActive = true) {
            if (finishIfActive)
                effectToRemove.Finish();
            if (!effectToRemove.isInfinite()) {
                List<GenericEffect> list = _effectsByComponent[effectToRemove.GetComponent()];
                list.Remove(effectToRemove);
                if (list.Count <= 0)
                    _effectsByComponent.Remove(effectToRemove.GetComponent());

                _currentEffects.Remove(effectToRemove);
                _effectToDelete.Remove(effectToRemove);
            }
        } // RemoveEffect


    }
}