using Nakama;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MultiplayerView : Menu
    {
        private IMatchmakerTicket _ticket;
        private GameConnection _connection;

        public void Init(GameConnection connection)
        {
            _connection = connection;
            _backButton.onClick.AddListener(() => Hide());
        }

        public async override void Show(bool isMuteButtonClick = false)
        {
            _connection.Socket.ReceivedMatchmakerMatched += OnMatchmakerMatched;

            try
            {
                _ticket = await _connection.Socket.AddMatchmakerAsync(
                    query: "*",
                    minCount: 2,
                    maxCount: 2,
                    stringProperties: null,
                    numericProperties: null);

            }
            catch (Exception e)
            {
                Debug.LogWarning("An error has occured while joining the matchmaker: " + e);
            }

        }

        public async override void Hide(bool isMuteSoundManager = false)
        {
            try
            {
                await _connection.Socket.RemoveMatchmakerAsync(_ticket);
            }
            catch (Exception e)
            {
                Debug.LogWarning("An error has occured while removing from matchmaker: " + e);
            }

            _connection.Socket.ReceivedMatchmakerMatched -= OnMatchmakerMatched;
            _ticket = null;
        }

        private void OnMatchmakerMatched(IMatchmakerMatched matched)
        {
            _connection.MultiplayerConnection = new MultiplayerConnection(matched);
            _connection.Socket.ReceivedMatchmakerMatched -= OnMatchmakerMatched;

            SceneManager.LoadScene(GameConfigurationManager.Instance.GameConfiguration.SceneNameMultiplayer);

        }
    }
}
