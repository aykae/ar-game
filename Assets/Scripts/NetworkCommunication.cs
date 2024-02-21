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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void IncrementScore()
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetPlayerScore", RpcTarget.All, playerName, currentScore + 1);
        }

        public void IncrementScore(int increment)
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetPlayerScore", RpcTarget.All, playerName, currentScore + increment);
        }

        public void ResetScores()
        {
            return;
        }
        
        public int GetDarts()
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            return this.scoreboard.GetDarts(playerName);
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