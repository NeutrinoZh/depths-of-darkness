using Unity.Netcode;

using UnityEngine;

using Zenject;

namespace DD.Multiplayer {
    public class ZenjectAutoInjector : NetworkBehaviour {
        private void Start() {
            ProjectContext.Instance.Container.Resolve<SceneContextRegistry>()
                                .GetContainerForScene(gameObject.scene)
                                .InjectGameObject(gameObject);
        }
    }
}