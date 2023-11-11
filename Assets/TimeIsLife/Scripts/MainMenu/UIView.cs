using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace TIL.MainMenu {
    public sealed class UIView : MonoBehaviour, ILifecycleListener {
        //===================================//
        // EVENTS
        public UnityEvent OnClickPlay;
        
        //===================================//

        private UIDocument mDocument;

        private Button mBtnPlay;

        //===================================//
        // ILifecycleListener

        public void OnStartGame() {
            mDocument = GetComponent<UIDocument>();
            mBtnPlay = mDocument.rootVisualElement.Query<Button>();
            
            mBtnPlay.clicked += OnClick;
        }

        public void OnFinishGame() {
            mBtnPlay.clicked -= OnClick;
        }

        //===================================//

        private void OnClick() {
            OnClickPlay?.Invoke();
        }
    }
}