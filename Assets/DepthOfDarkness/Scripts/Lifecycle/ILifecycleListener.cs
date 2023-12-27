namespace DD {
    public interface ILifecycleListener {
        public void OnStart() {}
        public void OnUpdate() {}
        public void OnFixed() {}
        public void OnFinish() {}

        public void OnPause() {}
        public void OnResume() {} 
    }
}