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

        void ILifecycleListener.OnStart() {
            mDocument = GetComponent<UIDocument>();
            mBtnPlay = mDocument.rootVisualElement.Query<Button>();
            
            mBtnPlay.clicked += OnClick;
        }

        void ILifecycleListener.OnFinish() {
            mBtnPlay.clicked -= OnClick;
        }

        //===================================//

        private void OnClick() {
            OnClickPlay?.Invoke();
        }
    }
}