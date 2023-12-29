using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace DD.Game {
    public class Pickable : MonoBehaviour, ILifecycleListener {

        //===========================================================//
        private PickablesRegister mPickableRegister;

        private SpriteRenderer mSpriteRenderer;
        private Shader mDefaultShader;

        public SpriteRenderer Renderer => mSpriteRenderer;
        public Shader DefaultShader => mDefaultShader;

        [Inject]
        public void Construct(PickablesRegister _register) {
            mPickableRegister = _register;
        } 

        void ILifecycleListener.OnStart() {
            mSpriteRenderer = GetComponent<SpriteRenderer>();
            Assert.AreNotEqual(mSpriteRenderer, null);

            mDefaultShader = mSpriteRenderer.material.shader;

            mPickableRegister.AddPickable(this);
        }

        void ILifecycleListener.OnFinish() {
            mPickableRegister.RemovePickable(this);
        }
    }
}