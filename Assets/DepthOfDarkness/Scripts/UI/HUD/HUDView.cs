using UnityEngine;
using UnityEngine.UIElements;

namespace DD.Game {
    public class HUDView : MonoBehaviour, ILifecycleListener {
        [SerializeField] private PlayerState mPlayerState;
        
        private UIDocument mDocument;
        private Label mOreCount;

        void ILifecycleListener.OnStart() {
            mDocument = GetComponent<UIDocument>();
            mOreCount = mDocument.rootVisualElement.Query<Label>();

            mPlayerState.OnChangeOreCountEvent += UpdateOreCountHandle;
            UpdateOreCountHandle();
        }

        void ILifecycleListener.OnFinish() {
            mPlayerState.OnChangeOreCountEvent -= UpdateOreCountHandle;
        }

        private void UpdateOreCountHandle() {
            mOreCount.text = mPlayerState.OreCount.ToString();
        }
    }
}