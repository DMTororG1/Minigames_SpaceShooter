using Nakama;
using UnityEngine;

namespace Game
{
    public class ProfilePopup : Menu
    {
        [SerializeField] private ProfilePanel _profilePanel = null;

        private GameConnection _connection;

        private void Awake()
        {
            base.SetBackButtonHandler(() => Hide());
        }

        public void Init(GameConnection connection, ProfileUpdatePanel profileUpdatePanel)
        {
            _connection = connection;
            _profilePanel.Init(connection, profileUpdatePanel);
        }

        public override void Show(bool isMuteButtonClick = false)
        {
            _profilePanel.Show(_connection.Account.User);
            base.Show(isMuteButtonClick);
        }

        public void Show(IApiUser user)
        {
            _profilePanel.Show(user);
            base.Show();
        }

        public void Show(string userId)
        {
            _profilePanel.ShowAsync(userId);
            base.Show();
        }
    }
}
