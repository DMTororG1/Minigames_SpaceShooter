using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    /// <summary>
    /// Single leaderboard record UI.
    /// </summary>
    public class LeaderboardEntry : MonoBehaviour
    {
        [SerializeField] private Text _rank = null;
        [SerializeField] private Text _username = null;
        [SerializeField] private Text _score = null;
        [SerializeField] private Button _profile = null;

        public void SetPlayer(string username, int rank, string score, Action onProfileClicked)
        {
            _username.text = username;
            _rank.text = rank + ".";
            _score.text = score;
            _profile.onClick.AddListener(() => onProfileClicked());
        }
    }
}
