using Nakama;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace Game
{
    public class ProfileView : Menu
    {
        [SerializeField] private TextMeshProUGUI username;
        private GameConnection _connection;

        private void Awake()
        {
            _backButton.onClick.AddListener(() => Hide());
        }

        public void Init(GameConnection connection)
        {
            _connection = connection;
        }

        public override void Show(bool isMuteButtonClick = false)
        {
            ShowProfile(_connection.Account.User);
        }

        private async void ShowProfile(IApiUser user)
        {
            try
            {
                IApiAccount results = await _connection.Client.GetAccountAsync(_connection.Session);
                if (results.User != null)
                {
                    username.text = results.User.Username;

                }
                else
                {
                    Debug.LogWarning("Couldn't find user with id: " + user.Id);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("An error has occured while retrieving user info: " + e);
            }
        }
    }
}