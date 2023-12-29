using Zenject;

namespace DD.Game {
    public class PikablesServiceInstaller : MonoInstaller {
        //========================================================//

        private GameObservable mGameObservable = null;
        private PickablesRegister mRegister = null;

        [Inject]
        public void Construct(GameObservable _observable) {
            mGameObservable = _observable;
            mRegister = new PickablesRegister();
        }

        public void Awake() {
            mGameObservable.AddListener(mRegister);            
        }

        //========================================================//

        public override void InstallBindings() {
            Container.Bind<PickablesRegister>().FromInstance(mRegister).AsSingle();
        }
    }
}