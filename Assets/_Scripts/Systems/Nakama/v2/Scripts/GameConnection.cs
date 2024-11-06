using Nakama;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GameConnection", menuName = GameConstants.CreateAssetMenu_GameConnection)]
    public class GameConnection : ScriptableObject
    {
        private IClient _client;
        private ISocket _socket;
        public ISession Session { get; set; }
        public IApiAccount Account { get; set; }
        public IClient Client => _client;
        public ISocket Socket => _socket;
        public MultiplayerConnection MultiplayerConnection { get; set; }
        public void Init(IClient client, ISocket socket, IApiAccount account, ISession session)
        {
            _client = client;
            _socket = socket;
            Account = account;
            Session = session;
        }
    }
}