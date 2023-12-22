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

        public void OnStart() {
            mDocument = GetComponent<UIDocument>();
            mBtnPlay = mDocument.rootVisualElement.Query<Button>();
            
            mBtnPlay.clicked += OnClick;
        }

        public void OnFinish() {
            mBtnPlay.clicked -= OnClick;
        }

        //===================================//

        private void OnClick() {
            OnClickPlay?.Invoke();
        }
    }
}