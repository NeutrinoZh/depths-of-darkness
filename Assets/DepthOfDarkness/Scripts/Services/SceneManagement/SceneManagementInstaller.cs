using UnityEngine;
using Zenject;

namespace DD {
    public class SceneManagementInstaller : MonoInstaller {
        [SerializeField] private SceneManagement mSceneManagement; 
        public override void InstallBindings() {
            var instance = Instantiate(mSceneManagement);
            Container.Bind<SceneManagement>().FromInstance(instance).AsSingle();
        }
    }
}