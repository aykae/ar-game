using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    internal class Scoreboard : MonoBehaviour
    {
        private Dictionary<string, int> scores;
        private Dictionary<string, int> darts;

        private int initDarts = 10;
        
        private void Start()
        {
            this.scores = new Dictionary<string, int>();
            this.darts = new Dictionary<string, int>();
        }

        public void SetScore(string playerName, int score)
        {
            if (this.scores.ContainsKey(playerName))
            {
                this.scores[playerName] = score;
            }
            else
            {
                this.scores.Add(playerName, score);
            }
        }

        public int GetScore(string playerName)
        { 
            if (this.scores.ContainsKey(playerName))
            {
                return this.scores[playerName];
            }
            else
            {
                return 0;
            }
        }

        public void InitDarts(string playerName)
        { 
            if (this.darts.ContainsKey(playerName))
            {
                this.darts[playerName] = initDarts;
            }
            else
            {
                this.darts.Add(playerName, initDarts);
            }
        }

        public void SetDarts(string playerName, int darts)
        {
            if (this.darts.ContainsKey(playerName))
            {
                this.darts[playerName] = darts;
            }
            else
            {
                this.darts.Add(playerName, darts);
            }
        }

        public int GetDarts(string playerName)
        { 
            if (this.darts.ContainsKey(playerName))
            {
                return this.darts[playerName];
            }
            else
            {
                return initDarts;
            }
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            foreach (var score in this.scores)
            {
                GUILayout.Label($"{score.Key}: {score.Value}\n{this.GetDarts(score.Key)}", new GUIStyle
                {
                    normal = new GUIStyleState
                    {
                        textColor = Color.black
                    },
                    fontSize = 30
                });
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndArea();


        }
    }
}
