namespace TIL {
    public interface ILifecycleListener {
        public void OnStartGame() {}
        public void OnUpdateGame() {}
        public void OnFinishGame() {}

        public void OnPause() {}
        public void OnResume() {} 
    }
}