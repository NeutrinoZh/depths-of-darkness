using UnityEngine;
using Zenject;

namespace DD {
    public class GameObservableAutoRunner : MonoBehaviour {
        private GameObservable mGameObservable;
        
        [Inject] 
        public void Construct(GameObservable _gameObservable) {
            mGameObservable = _gameObservable;
        }

        private void Start() {
            mGameObservable.Run();
        }
    }
}