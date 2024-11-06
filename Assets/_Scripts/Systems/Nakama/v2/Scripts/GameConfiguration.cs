using UnityEngine;

namespace Game
{
    [CreateAssetMenu(
       menuName = GameConstants.CreateAssetMenu_GameConfiguration)]
    public class GameConfiguration : ScriptableObject
    {
        public string SceneNameMainMenu { get { return _sceneNameMainMenu; } }
        public string SceneNameMultiplayer { get { return _sceneNameMultiplayer; } }

        public int StartingGold { get { return _startingGold; } }
        public int MaxGoldCount { get { return _maxGoldCount; } }
        public float GoldPerSecond { get { return _goldPerSecond; } }

        public bool IsAudioEnabled { get { return _isAudioEnabled; } }
        public float AudioVolume { get { return _audioVolume; } }

        [Header("Scenes")]
        [SerializeField] private string _sceneNameMainMenu = "Main";
        [SerializeField] private string _sceneNameMultiplayer = "SceneMultiplayer";

        [Header("Gameplay - Local Player")]

        [Range(1, 3)]
        [SerializeField] private int _startingGold = 3;

        [Range(3, 10)]
        [SerializeField] private int _maxGoldCount = 10;

        [Range(0.1f, 2f)]
        [SerializeField] private float _goldPerSecond = 0.5f;

        [Header("Audio")]
        [SerializeField] private bool _isAudioEnabled = true;

        [Range(0, 1f)]
        [SerializeField] float _audioVolume = 1;
    }
}
