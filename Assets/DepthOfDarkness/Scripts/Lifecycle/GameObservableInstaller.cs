using Zenject;
using UnityEngine;

namespace DD {
    public class GameObservableInstaller : MonoInstaller {
        [SerializeField] private GameObservable observable;

        public override void InstallBindings() {
            Container.Bind<GameObservable>().FromInstance(observable).AsSingle();
        }
    }
}