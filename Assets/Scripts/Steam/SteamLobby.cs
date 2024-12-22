using Mirror;
using Steamworks;
using UnityEngine;

public class SteamLobby : Singleton<SteamLobby>
{

    private NetworkManager _networkManager => PlayerManager.instance._networkManager;

    protected Callback<LobbyCreated_t> LobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> JoinRequest;
    protected Callback<LobbyEnter_t> LobbyEntered;

    public ulong CurrentLobbyID;
    private const string HostAddressKey = "HostAddress";

    public static CSteamID LobbyId { get; private set; }

    private int _maxMemberCount => _networkManager.maxConnections;

    protected override void Awake()
    {
        base.Awake();
        SteamAPI.Init();
    }

    private void Start()
    {
        if (!SteamManager.Initialized) return;

        print(SteamUser.GetSteamID() + " Connected");

        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, _maxMemberCount);
    }

    public void DisconnectLobby()
    {
        if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
            Debug.Log("Client disconnected from the server.");
        }
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK) return;
        Debug.Log("Lobby Created");

        LobbyId = new CSteamID(callback.m_ulSteamIDLobby);

        _networkManager.StartHost();

        SteamMatchmaking.SetLobbyData(LobbyId, HostAddressKey, SteamUser.GetSteamID().ToString());

        TextManager.instance.ShowText("Invite your friends via steam",4,Color.cyan);

        OpenInvitePanel();
    }

    private void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        Debug.Log("Request to join lobby");

        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active) return;

        //ServerUI.instance.Connect();
        TextManager.instance.ShowText("Connected", 2, Color.cyan);
        WindowManager.instance.CloseWindows();

        CurrentLobbyID = callback.m_ulSteamIDLobby;

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);

        _networkManager.networkAddress = hostAddress;
        _networkManager.StartClient();
    }

    void OnApplicationQuit()
    {
        SteamAPI.Shutdown();
    }

    private void OpenInvitePanel()
    {
        if (SteamManager.Initialized)
        {
            SteamFriends.ActivateGameOverlayInviteDialog(LobbyId);
        }
        else
        {
            Debug.LogError("Steam is not initialized.");
        }
    }
}
