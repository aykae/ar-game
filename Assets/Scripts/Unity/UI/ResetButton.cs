using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;

namespace MyFirstARGame
{
    public class ResetButton : MonoBehaviour
    {
        [SerializeField]
        GameObject m_ResetButton;
        public GameObject resetButton
        {
            get => m_ResetButton;
            set => m_ResetButton = value;
        }

        void Start()
        {
            if (Application.CanStreamedLevelBeLoaded("Menu"))
                m_ResetButton.SetActive(true);
        }

        void Update()
        {
            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
                ResetButtonPressed();
        }

        public void ResetButtonPressed()
        {
            var networkCommunication = FindObjectOfType<NetworkCommunication>();
            networkCommunication.IncrementScore();
            Debug.Log("ResetButtonPressed() was called.");
        }
    }
}
