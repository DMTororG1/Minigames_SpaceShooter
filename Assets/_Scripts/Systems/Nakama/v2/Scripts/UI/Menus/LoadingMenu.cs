using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PiratePanic
{
    public class LoadingMenu : Menu
    {
        [Space]
        [Header("Status Server")]
        [SerializeField] private TextMeshProUGUI statusText = null;
        [SerializeField] private Button refreshButton = null;

        private GameConnection _connection;

        public void Init(GameConnection connection)
        {
            _connection = connection;
        }

        public override void Show(bool isMuteButtonClick = false)
        {
            base.Show(isMuteButtonClick);
            statusText.text = "Offline";
            refreshButton.onClick.AddListener(Retry);
        }

        public void AwaitConnection()
        {
            Debug.Log("Online...");
            statusText.text = "Online";
            refreshButton.gameObject.SetActive(false);
        }

        public void ConnectionFailed()
        {
            statusText.text = "Offline";
            refreshButton.gameObject.SetActive(true);
            refreshButton.onClick.AddListener(Retry);
        }

        private void Retry()
        {
            Debug.Log("Retry...");
            statusText.text = "Retry...";
            AwaitConnection();
        }
    }
}
