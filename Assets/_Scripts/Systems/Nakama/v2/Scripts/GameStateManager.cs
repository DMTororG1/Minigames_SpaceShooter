using Nakama;
using System.Linq;
using UnityEngine;
using System.Text;
using PiratePanic;

namespace Game
{
    public class GameStateManager
    {
        private GameConnection _connection;

        public GameStateManager(GameConnection connection)
        {
            _connection = connection;

            _connection.Socket.ReceivedMatchPresence += OnMatchPresence;
            _connection.Socket.ReceivedMatchState += ReceiveMatchStateMessage;
        }

        private void OnMatchPresence(IMatchPresenceEvent e)
        {
            if (e.Leaves.Count() > 0)
            {
                Debug.LogWarning($"OnMatchPresence() User(s) left the game");
            }
        }

        private void ReceiveMatchStateMessage(IMatchState matchState)
        {
            string messageJson = Encoding.UTF8.GetString(matchState.State);
            if(string.IsNullOrEmpty(messageJson) )
            {
                return;
            }

            ReceiveMatchStateHandle(matchState.OpCode, messageJson);
        }

        public void ReceiveMatchStateHandle(long opCode, string messageJson)
        {
            switch ((MatchMessageType)opCode)
            {
                case MatchMessageType.StartingHand:
                    break;

            }
        }
    }
}