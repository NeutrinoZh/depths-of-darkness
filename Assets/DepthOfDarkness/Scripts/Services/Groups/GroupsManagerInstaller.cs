using UnityEngine;

using Zenject;

namespace DD.Game {
    public class GroupInstaller : MonoInstaller {
        [SerializeField] private GroupManager m_groupManager;

        public override void InstallBindings() {
            Container.Bind<GroupManager>().FromInstance(m_groupManager).AsSingle();
        }
    }
}