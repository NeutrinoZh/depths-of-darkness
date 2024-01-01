using UnityEngine;
using UnityEngine.UIElements;

using System;

namespace DD.MainMenu {
    public class MultiplayerMenuView : MonoBehaviour, ILifecycleListener, IPage {
        
        //=============================================//

        public Action OnClickHost = null;
        public Action OnClickJoin = null;
        public Action OnClickBack = null;

        //=================================================//
        // IPage

        void IPage.Activate() {
            mDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        void IPage.Unactivate() {
            mDocument.rootVisualElement.style.display = DisplayStyle.None;
        }

        //=============================================//

        const string mHostButtonName = "Host";
        const string mJoinButtonName = "Join";
        const string mBackButtonName = "Back"; 

        //=============================================//

        private UIDocument mDocument; 

        private Button mBtnHost;
        private Button mBtnJoin;
        private Button mBtnBack;   
    
        //=============================================//

        void ILifecycleListener.OnInit() {
            mDocument = GetComponent<UIDocument>();

            mBtnHost = mDocument.rootVisualElement.Query<Button>(mHostButtonName);
            mBtnJoin = mDocument.rootVisualElement.Query<Button>(mJoinButtonName);
            mBtnBack = mDocument.rootVisualElement.Query<Button>(mBackButtonName);

            mBtnHost.clicked += OnHostHandle;
            mBtnJoin.clicked += OnJoinHandle;
            mBtnBack.clicked += OnBackHandle;

            (this as IPage).Unactivate();
        }       

        void ILifecycleListener.OnFinish() {
            mBtnHost.clicked -= OnHostHandle;
            mBtnJoin.clicked -= OnJoinHandle;
            mBtnBack.clicked -= OnBackHandle;
        }

        //===========================================// 

        private void OnHostHandle() {
            OnClickHost?.Invoke();
        }

        private void OnJoinHandle() {
            OnClickJoin?.Invoke();
        }

        private void OnBackHandle() {
            OnClickBack?.Invoke();
        }

        //====================================//
    }
}