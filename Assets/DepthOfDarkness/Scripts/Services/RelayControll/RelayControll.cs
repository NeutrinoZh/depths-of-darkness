

using System.Threading.Tasks;

using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;

namespace DD.Game.Multiplayer {
    public static class RelayControll {
        public static async Task<bool> StartClientWithRelay(string _roomCode) {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn) 
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: _roomCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
            return !string.IsNullOrEmpty(_roomCode) && NetworkManager.Singleton.StartClient();
        }

        public static async Task<string> StartHostWithRelay(int maxConnections=4) {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
            var roomCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            return NetworkManager.Singleton.StartHost() ? roomCode : null;
        }
    }
}