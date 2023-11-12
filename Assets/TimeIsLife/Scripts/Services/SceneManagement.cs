using UnityEngine;

namespace TIL {
    public enum SceneList {
        MAIN_MENU,
        GAME
    }
    
    public sealed class SceneManagement {
        public void LoadScene(SceneList _scene) {
            Debug.Log($"Load Scene: {_scene}");
        }
    }
}