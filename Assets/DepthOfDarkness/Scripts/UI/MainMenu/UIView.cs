using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace DD.MainMenu {
    public sealed class UIView : MonoBehaviour, ILifecycleListener {
        //===================================//
        // EVENTS
        public Action OnClickPlay;
        public Action OnClickCartLabel;

        //===================================//
        
        const string mCartLabelName = "CartLabel";
        const string mRootDivName = "RootDiv";

        const string mRootDivAnimateClass = "animate";

        //===================================//

        private UIDocument mDocument;
        private VisualElement mRootDiv;

        private VisualElement mCartLabel;
        private Button mCartButton;
        private Button mBtnPlay;

        //===================================//
        // ILifecycleListener

        void ILifecycleListener.OnStart() {
            // gettings 
            mDocument = GetComponent<UIDocument>();
            mBtnPlay = mDocument.rootVisualElement.Query<Button>();
            mRootDiv = mDocument.rootVisualElement.Query(mRootDivName);
            mCartLabel = mDocument.rootVisualElement.Query(mCartLabelName);
            mCartButton = mCartLabel.Query<Button>();
            
            // subscribes
            mBtnPlay.clicked += OnClickPlayHandle;
            mCartButton.clicked += OnClickCartLabelHandle;

            // animations
            DOTween.To(
                () => mCartLabel.style.opacity.value,
                x => mCartLabel.style.opacity = x,
                1, 1
            ).SetLoops(-1, LoopType.Yoyo);
        }

        void ILifecycleListener.OnFinish() {
            mBtnPlay.clicked -= OnClickPlayHandle;
            mCartButton.clicked -= OnClickCartLabelHandle;
        }

        //===================================//

        private void OnClickCartLabelHandle() {
            OnClickCartLabel?.Invoke();
        }

        private void OnClickPlayHandle() {
            OnClickPlay?.Invoke();
        }

        //===================================//

        public void StartUIAnimation() {
            mCartLabel.style.display = DisplayStyle.None;
            mRootDiv.AddToClassList(mRootDivAnimateClass);
        }
    }
}