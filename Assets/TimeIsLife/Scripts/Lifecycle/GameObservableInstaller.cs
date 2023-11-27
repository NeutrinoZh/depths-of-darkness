using Zenject;
using UnityEngine;

namespace TIL {
    public class GameObservableInstaller : MonoInstaller {
        [SerializeField] private GameObservable observable;

        public override void InstallBindings() {
            Container.Bind<GameObservable>().FromInstance(observable).AsSingle();
        }
    }
}