using UnityEngine;
using Zenject;

namespace DD.Game {
    public class WorldInstaller : MonoInstaller {
        [SerializeField] private WorldManager mWolrdManager;

        public override void InstallBindings() {
            Container.Bind<WorldManager>().FromInstance(mWolrdManager).AsSingle();
        }
    }
}