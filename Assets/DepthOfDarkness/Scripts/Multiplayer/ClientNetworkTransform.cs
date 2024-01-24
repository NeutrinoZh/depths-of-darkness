using Unity.Netcode.Components;

namespace DD.Multiplayer {
    public class ClientNetworkTransform : NetworkTransform {
        protected override bool OnIsServerAuthoritative() {
            return false;
        }
    }
}