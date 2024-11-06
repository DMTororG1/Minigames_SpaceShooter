using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using Nakama;
using System;
using PiratePanic;

namespace Game
{
    public class Scene01MainMenuController : MonoBehaviour
    {
        [Header("Networking")]
        [SerializeField] private GameConnection _connection;

        [Header("LoadingServer")]
        [SerializeField] LoadingMenu _loadingMenu;

        //[Header("Game")]
        //[SerializeField] MultiplayerView _multiplayerView;
        //[SerializeField] Button _multiplayerButton;

        [Header("Leaderboard")]
        [SerializeField] LeaderboardView _leaderboardView;
        [SerializeField] Button _leaderboardsButton;

        //[Header("ProfilePopup")]
        //[SerializeField] ProfilePopup _profilePopup;
        //[SerializeField] ProfileUpdatePanel _profileUpdatePanel;

        //[Header("Profile")]
        //[SerializeField] ProfileView _profileView;
        //[SerializeField] Button _profileButton;

        private void Awake()
        {
            //_multiplayerButton.onClick.AddListener(() => _multiplayerView.Show());
            _leaderboardsButton.onClick.AddListener(() => _leaderboardView.Show());
            //_profileButton.onClick.AddListener(() => _profileView.Show());
        }

        private async void Start()
        {
            _loadingMenu.Show(true);

            if (_connection.Session == null)
            {
                string deviceId = GetDeviceId();

                if (!string.IsNullOrEmpty(deviceId))
                {
                    PlayerPrefs.SetString(GameConstants.DeviceIdKey, deviceId);
                }

                await InitializeGame(deviceId);
            }

            //_multiplayerView.Init(_connection);
            //_profilePopup.Init(_connection, _profileUpdatePanel);
            //_profileUpdatePanel.Init(_connection, GetDeviceId());
            _leaderboardView.Init(_connection);
            //_profileView.Init(_connection);
            //_loadingMenu.Hide(true);
        }

        private async Task InitializeGame(string deviceId)
        {
            var client = new Client("http", "localhost", 7350, "defaultkey", UnityWebRequestAdapter.Instance);
            client.Timeout = 5;

            var socket = client.NewSocket(useMainThread: true);

            string authToken = PlayerPrefs.GetString(GameConstants.AuthTokenKey, null);
            bool isAuthToken = !string.IsNullOrEmpty(authToken);

            string refreshToken = PlayerPrefs.GetString(GameConstants.RefreshTokenKey, null);

            ISession session = null;

            if (isAuthToken)
            {
                session = Session.Restore(authToken, refreshToken);

                if (session.HasExpired(DateTime.UtcNow.AddDays(1)))
                {
                    try
                    {
                        session = await client.SessionRefreshAsync(session);
                    }
                    catch (ApiResponseException)
                    {
                        session = await client.AuthenticateDeviceAsync(deviceId);
                        PlayerPrefs.SetString(GameConstants.RefreshTokenKey, session.RefreshToken);
                    }

                    PlayerPrefs.SetString(GameConstants.AuthTokenKey, session.AuthToken);
                }
            }
            else
            {
                session = await client.AuthenticateDeviceAsync(deviceId);
                PlayerPrefs.SetString(GameConstants.AuthTokenKey, session.AuthToken);
                PlayerPrefs.SetString(GameConstants.RefreshTokenKey, session.RefreshToken);
            }

            socket.Closed += () => Connect(socket, session);

            Connect(socket, session);

            IApiAccount account = null;

            try
            {
                account = await client.GetAccountAsync(session);
            }
            catch (ApiResponseException e)
            {
                Debug.LogError("Error getting user account: " + e.Message);
            }

            _connection.Init(client, socket, account, session);
            _loadingMenu.Init(_connection);
        }

        private string GetDeviceId()
        {
            string deviceId = PlayerPrefs.GetString(GameConstants.DeviceIdKey);

            if (string.IsNullOrWhiteSpace(deviceId))
            {
                deviceId = Guid.NewGuid().ToString();
            }

            return deviceId;
        }

        private async void OnApplicationQuit()
        {
            await _connection.Socket.CloseAsync();
        }

        private async void Connect(ISocket socket, ISession session)
        {
            try
            {
                if (!socket.IsConnected)
                {
                    await socket.ConnectAsync(session);
                }
            }
            catch (Exception e)
            {
                _loadingMenu.ConnectionFailed();
                Debug.LogWarning("Error connecting socket: " + e.Message);
            }
        }
    }
}
