using Zenject;

namespace DD {
    public class SceneManagementInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<SceneManagement>().FromNew().AsSingle();
        }
    }
}