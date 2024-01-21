using UnityEngine;

using Zenject;

namespace DD.Game {
    public class PikablesServiceInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<PickablesRegister>().AsSingle();
        }
    }
}