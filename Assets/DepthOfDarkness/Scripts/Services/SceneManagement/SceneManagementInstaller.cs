using UnityEngine;

using Zenject;

namespace DD {
    public class SceneManagementInstaller : MonoInstaller {
        [SerializeField] private SceneManagement m_sceneManagement;
        public override void InstallBindings() {
            var instance = Instantiate(m_sceneManagement);
            Container.Bind<SceneManagement>().FromInstance(instance).AsSingle();
        }
    }
}