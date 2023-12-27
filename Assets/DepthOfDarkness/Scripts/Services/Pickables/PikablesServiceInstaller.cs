using UnityEngine;
using Zenject;

namespace DD.Game {
    public class PikablesServiceInstaller : MonoInstaller {
        //========================================================//

        private GameObservable mGameObservable = null;

        [Inject]
        public void Construct(GameObservable _observable) {
            mGameObservable = _observable;
        }

        public void Awake() {
            mGameObservable.AddListener(new PickablesRegister());            
        }

        //========================================================//

        public override void InstallBindings() {
            Container.Bind<PickablesRegister>().FromNew().AsSingle();
        }
    }
}