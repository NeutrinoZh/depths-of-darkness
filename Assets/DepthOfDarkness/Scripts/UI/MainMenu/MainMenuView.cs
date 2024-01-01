using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System;

namespace DD.MainMenu {
    public sealed class MainMenuView : MonoBehaviour, ILifecycleListener, IPage {
        //===================================//
        // EVENTS
        public Action OnClickPlay;
        public Action OnClickOnline;
        public Action OnClickCartLabel;

        //=================================================//
        // IPage

        void IPage.Activate() {
            mDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        void IPage.Unactivate() {
            mDocument.rootVisualElement.style.display = DisplayStyle.None;
        }

        //===================================//
        
        const string mPlayButtonName = "PlaySolo";
        const string mPlayOnlineButtonName = "PlayOnline";
        const string mCartLabelName = "CartLabel";

        const string mRootDivName = "RootDiv";
        const string mRootDivAnimateClass = "animate";

        //===================================//

        private UIDocument mDocument;
        
        private VisualElement mRootDiv;

        private VisualElement mCartLabel;
        private Button mCartButton;
        private Button mBtnPlay;
        private Button mBtnMultiplayer;

        //===================================//
        // ILifecycleListener

        void ILifecycleListener.OnInit() {
            // gettings 
            mDocument = GetComponent<UIDocument>();
            mBtnPlay = mDocument.rootVisualElement.Query<Button>(mPlayButtonName);
            mBtnMultiplayer = mDocument.rootVisualElement.Query<Button>(mPlayOnlineButtonName);
            
            mRootDiv = mDocument.rootVisualElement.Query(mRootDivName);
            mCartLabel = mDocument.rootVisualElement.Query(mCartLabelName);
            mCartButton = mCartLabel.Query<Button>();
            
            // subscribes
            mBtnPlay.clicked += OnClickPlayHandle;
            mCartButton.clicked += OnClickCartLabelHandle;
            mBtnMultiplayer.clicked += OnClickPlayOnlineHandle;
        }

        void ILifecycleListener.OnStart() {
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
            mBtnMultiplayer.clicked -= OnClickPlayOnlineHandle;
        }

        //===================================//

        private void OnClickCartLabelHandle() {
            OnClickCartLabel?.Invoke();
        }

        private void OnClickPlayHandle() {
            OnClickPlay?.Invoke();
        }

        private void OnClickPlayOnlineHandle() {
            OnClickOnline?.Invoke();
        }

        //===================================//

        public void StartUIAnimation() {
            mCartLabel.style.display = DisplayStyle.None;
            mRootDiv.AddToClassList(mRootDivAnimateClass);
        }
    }
}