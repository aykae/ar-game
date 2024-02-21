namespace MyFirstARGame
{
    using Photon.Pun;
    using UnityEngine;

    /// <summary>
    /// You can use this class to make RPC calls between the clients. It is already spawned on each client with networking capabilities.
    /// </summary>
    public class NetworkCommunication : MonoBehaviourPun
    {
        [SerializeField]
        private Scoreboard scoreboard;
        private bool firstGuiDraw;
        private bool isDoubling;

        // Start is called before the first frame update
        void Start()
        {
            firstGuiDraw = false;
            isDoubling = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!firstGuiDraw && PhotonNetwork.LocalPlayer.ActorNumber != null)
            {
                firstGuiDraw = true;
                var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
                this.photonView.RPC("Network_SetPlayerScore", RpcTarget.All, playerName, 0);
            }
        }

        public void IncrementScore()
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetPlayerScore", RpcTarget.All, playerName, currentScore + 1);
        }

        public void IncrementScore(int increment)
        {
            int factor = 1;
            if (isDoubling)
            {
                factor = 2;
                DisableDoublePoints();
            }
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetPlayerScore", RpcTarget.All, playerName, currentScore + (increment * factor));
        }

        public void ResetGame()
        {
            this.scoreboard.ResetGame();
        }
        
        public int GetDarts()
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            return this.scoreboard.GetDarts(playerName);
        }

        public void IncrementDarts(int increment)
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var currentDarts = this.scoreboard.GetDarts(playerName);
            this.photonView.RPC("Network_SetPlayerDarts", RpcTarget.All, playerName, currentDarts + increment);
        }

        public void DecrementDarts()
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var currentDarts = this.scoreboard.GetDarts(playerName);
            if (currentDarts > 0)
            {
                this.photonView.RPC("Network_SetPlayerDarts", RpcTarget.All, playerName, currentDarts - 1);
            }
        }

        public string GetWinner()
        {
            return this.scoreboard.GetWinner();
        }

        public void EnableDoublePoints()
        {
            isDoubling = true;
        }

        public void DisableDoublePoints()
        {
            isDoubling = false;
        }

        [PunRPC]
        public void Network_SetPlayerScore(string playerName, int newScore)
        {
            Debug.Log($"Player {playerName} scored!");
            this.scoreboard.SetScore(playerName, newScore);
        }

        [PunRPC]
        public void Network_SetPlayerDarts(string playerName, int newDarts)
        {
            Debug.Log($"Player {playerName} scored!");
            this.scoreboard.SetDarts(playerName, newDarts);
        }

        public void UpdateForNewPlayer(Photon.Realtime.Player player)
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";

            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetPlayerScore", player, playerName, currentScore);

            this.scoreboard.InitDarts(playerName); 
            var currentDarts = this.scoreboard.GetDarts(playerName);
            this.photonView.RPC("Network_SetPlayerDarts", player, playerName, currentDarts);
        }

    }

}