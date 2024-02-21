using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyFirstARGame
{
    internal class Scoreboard : MonoBehaviour
    {
        private Dictionary<string, int> scores;
        private Dictionary<string, int> darts;

        private int initDarts = 10;

        public int winningScore = 100;
        private string winner;

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

            if (this.scores[playerName] >= winningScore)
            {
                this.SetWinner(playerName);
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
        
        public void SetWinner(string playerName)
        {
            this.winner = playerName;
        }

        public string GetWinner()
        {
            return this.winner;
        }

        public void ResetGame()
        {
/*            foreach (var score in this.scores)
            {
                this.scores[score.Key] = 0;
            }
*/
            for (int i = 0; i < this.scores.Count; i++)
            {
                this.scores[this.scores.Keys.ElementAt(i)] = 0;
            }

            for (int i = 0; i < this.darts.Count; i++)
            {
                this.darts[this.darts.Keys.ElementAt(i)] = initDarts;
            }

            this.winner = null;
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            foreach (var score in this.scores)
            {
                GUILayout.Label($"{score.Key}: {score.Value}\n{this.GetDarts(score.Key)}\n", new GUIStyle
                {
                    normal = new GUIStyleState
                    {
                        textColor = Color.yellow
                    },
                    fontSize = 55
                });
            }
            
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
