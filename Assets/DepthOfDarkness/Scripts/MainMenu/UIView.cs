using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DD.MainMenu {
    public sealed class UIView : MonoBehaviour, ILifecycleListener {
        //===================================//
        // EVENTS
        public Action OnClickPlay;
        
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