using System.Threading.Tasks;

using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;

namespace DD.Multiplayer {
    public static class RelayControll {
        const bool c_local = true;

        public static async Task<bool> StartClientWithRelay(string _roomCode) {
            if (c_local)
                return NetworkManager.Singleton.StartClient();

#pragma warning disable CS0162 
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: _roomCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
            return !string.IsNullOrEmpty(_roomCode) && NetworkManager.Singleton.StartClient();
#pragma warning restore CS0162 
        }

        public static async Task<string> StartHostWithRelay(int maxConnections = 4) {
            if (c_local)
                return NetworkManager.Singleton.StartHost() ? "local" : null;

#pragma warning disable CS0162 
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
            var roomCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            return NetworkManager.Singleton.StartHost() ? roomCode : null;
#pragma warning restore CS0162 
        }
    }
}