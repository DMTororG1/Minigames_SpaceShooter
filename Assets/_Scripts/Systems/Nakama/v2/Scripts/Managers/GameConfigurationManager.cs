using Game;
using UnityEngine;

namespace Game
{
    public class GameConfigurationManager : Singleton<GameConfigurationManager>
    {
        public GameConfiguration GameConfiguration { get { return gameConfiguration; } }

        [SerializeField] private GameConfiguration gameConfiguration = null;
    }
}
