using Mirror;
using Steamworks;
using System;
using TMPro;
using UnityEngine;

public class PlayerInfoDisplay : NetworkBehaviour
{

    [SerializeField] private TMP_Text _playerName;
    //[SerializeField] private SpriteRenderer _playerSprite;
    public string PlayerName => _playerName.text;

    [SyncVar(hook = nameof(_onConnectionIDUpdated))]
    public int ConnectionID;

    [SyncVar(hook = nameof(_onSteamIdUpdated))]
    private ulong _steamId;

    protected Callback<AvatarImageLoaded_t> avatarImageLoaded;

    #region Server

    public void SetSteamId(ulong steamId)
    {
        _steamId = steamId;
    }

    #endregion

    #region Client

    private void Awake()
    {
        _playerName.text = "";
    }
    public override void OnStartClient()
    {
        base.OnStartClient();

        avatarImageLoaded = Callback<AvatarImageLoaded_t>.Create(_onAvatarImageLoaded);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        //UIPlayersScreen.instance.RemovePlayer(ConnectionID);
    }

    protected void _onAvatarImageLoaded(AvatarImageLoaded_t callback)
    {
        if (callback.m_steamID.m_SteamID != _steamId) return;

        var texture = _getSteamImageAsTexture(callback.m_iImage);

       // UIPlayersScreen.instance.SetPlayerImage(ConnectionID, texture);
    }

    private void _onSteamIdUpdated(ulong oldId, ulong newId)
    {
        if (isLocalPlayer)
        {
           // MyNetworkManager.instance.ConnectionID = ConnectionID;
        }


        var cSteamId = new CSteamID(newId);

        var name = SteamFriends.GetFriendPersonaName(cSteamId);

        //UIPlayersScreen.instance.AddNewPlayer(ConnectionID, name);

        _playerName.text = name;
        //UIPlayersScreen.instance.SetPlayerName(ConnectionID, name);

        int imageID = SteamFriends.GetLargeFriendAvatar(cSteamId);

        if (imageID == -1) return;

        var texture = _getSteamImageAsTexture(imageID);

        //UIPlayersScreen.instance.SetPlayerImage(ConnectionID, texture);
    }

    private Texture2D _getSteamImageAsTexture(int iImage)
    {
        Texture2D texture = null;

        bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);

        if (isValid)
        {
            byte[] image = new byte[width * height * 4];

            isValid = SteamUtils.GetImageRGBA(iImage, image, (int)(width * height * 4));

            if (isValid)
            {
                texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
                texture.LoadRawTextureData(image);
                texture.Apply();
            }
        }

        return texture;
    }

    public Action OnConnectionIDUpdated;
    private void _onConnectionIDUpdated(int oldId, int newId)
    {
        OnConnectionIDUpdated?.Invoke();

        if (isLocalPlayer)
        {
           // MyNetworkManager.instance.ConnectionID = newId;
        }
    }

    #endregion


}
