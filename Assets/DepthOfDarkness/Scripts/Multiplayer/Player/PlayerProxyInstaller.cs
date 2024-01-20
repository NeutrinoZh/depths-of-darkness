using DD.Game;

using UnityEngine;

using Zenject;

namespace DD.Multiplayer {
    public class PlayerProxyInstaller : MonoInstaller {
        [SerializeField] private PlayerProxy m_playerProxy;

        public override void InstallBindings() {
            Container.Bind<PlayerProxy>().FromInstance(m_playerProxy).AsSingle();
        }
    }
}