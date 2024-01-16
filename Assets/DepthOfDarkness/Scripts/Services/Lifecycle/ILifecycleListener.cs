namespace DD {
    public interface ILifecycleListener {
        public void OnInit() { }
        public void OnStart() { }
        public void OnUpdate() { }
        public void OnFinish() { }
    }
}