using UnityEngine;
using UnityEngine.UIElements;

using System;

namespace DD.MainMenu {
    public class JoinMenuView : MonoBehaviour, ILifecycleListener, IPage {

        //=====================================================//

        public Action OnClickJoin = null;
        public Action OnClickBack = null;

        public string RoomCode {
            get => mRoomCodeField.text;
        }

        
        //=================================================//
        // IPage

        void IPage.Activate() {
            mDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        void IPage.Unactivate() {
            mDocument.rootVisualElement.style.display = DisplayStyle.None;
        }

        //=====================================================//

        const string mRoomCodeFieldName = "RoomCode";
        const string mJoinButtonName = "Join";
        const string mBackButtonName = "Back";

        //=====================================================//
        
        private UIDocument mDocument;

        private TextInputBaseField<string> mRoomCodeField;
        private Button mBtnJoin;
        private Button mBtnBack;

        //=====================================================//

        void ILifecycleListener.OnInit() {
            mDocument = GetComponent<UIDocument>();

            mRoomCodeField = mDocument.rootVisualElement.Query<TextInputBaseField<string>>(mRoomCodeFieldName);
            mBtnJoin = mDocument.rootVisualElement.Query<Button>(mJoinButtonName);
            mBtnBack = mDocument.rootVisualElement.Query<Button>(mBackButtonName);

            mBtnJoin.clicked += OnJoinHandle;
            mBtnBack.clicked += OnBackHandle;

            (this as IPage).Unactivate();
        }

        void ILifecycleListener.OnFinish() {
            mBtnJoin.clicked -= OnJoinHandle;
            mBtnBack.clicked -= OnBackHandle;
        }

        //====================================================//
    
        private void OnJoinHandle() {
            OnClickJoin?.Invoke();
        }

        private void OnBackHandle() {
            OnClickBack?.Invoke();
        }

        //=====================================================//
    }
}