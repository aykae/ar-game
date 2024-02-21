using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace MyFirstARGame
{
    public class GlobalObject : MonoBehaviour
    {
        [SerializeField]
        private Scoreboard scoreboard;

        public bool isGameOver = false;
        public bool isNewGame = false;
        public List<GameObject> darts;

        // Start is called before the first frame update
        void Start()
        {
            isGameOver = false;
            isNewGame = false;
        }

        // Update is called once per frame
        void Update()
        {

            if (!isGameOver && isNewGame)
            {
                foreach (GameObject dart in darts)
                {
                    Destroy(dart);
                }

                isNewGame = false;
                
            }
        
        }

        private void OnGUI()
        {
            var networkCommunication = FindObjectOfType<NetworkCommunication>();
            if (networkCommunication == null)
            {
                return;
            }

            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.normal.textColor = Color.yellow;
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            centeredStyle.fontSize = 100;
            //centeredStyle.padding = new RectOffset(0, 0, 0, 700);


            if (networkCommunication.GetWinner() != null)
            {
                isGameOver = true;

                if (networkCommunication.GetWinner() == $"Player {PhotonNetwork.LocalPlayer.ActorNumber}")
                {
                    GUILayout.Label("You Won!", centeredStyle);
                }
                else
                {
                    GUILayout.Label("You Lost.", centeredStyle);
                }
                centeredStyle.fontSize = 50;
                GUILayout.Label("Tap to play again.", centeredStyle);
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

    }
}
