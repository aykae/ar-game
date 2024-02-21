namespace MyFirstARGame
{
    using UnityEngine;
    using Photon.Pun;
    using System.Collections.Specialized;

    /// <summary>
    /// Controls projectile behaviour. In our case it currently only changes the material of the projectile based on the player that owns it.
    /// </summary>
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Material[] projectileMaterials;

        private void Awake()
        {
            // Pick a material based on our player number so that we can distinguish between projectiles. We use the player number
            // but wrap around if we have more players than materials. This number was passed to us when the projectile was instantiated.
            // See ProjectileLauncher.cs for more details.
            var photonView = this.transform.GetComponent<PhotonView>();
            var playerId = Mathf.Max((int)photonView.InstantiationData[0], 0);
            if (this.projectileMaterials.Length > 0)
            {
                var material = this.projectileMaterials[playerId % this.projectileMaterials.Length];
                this.transform.GetComponent<Renderer>().material = material;
            }
            
            //Add active darts to globalObject 
            GlobalObject globalObject = GameObject.Find("GlobalObject").GetComponent<GlobalObject>();
            globalObject.darts.Add(gameObject);
            Debug.Log("Darts: " + globalObject.darts.Count);
        }

        private void OnCollisionEnter(Collision collision)
        {

            var networkCommunication = FindObjectOfType<NetworkCommunication>();
            // If we hit the dartboard, let's update our score.
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.otherCollider.gameObject.CompareTag("D1"))
                {
                    Debug.Log("Hit D1");
                    networkCommunication.IncrementScore(10);

                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (contact.otherCollider.gameObject.CompareTag("D2"))
                {
                    Debug.Log("Hit D2");
                    networkCommunication.IncrementScore(20);

                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (contact.otherCollider.gameObject.CompareTag("D3"))
                {
                    Debug.Log("Hit D3");
                    networkCommunication.IncrementScore(30);

                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (contact.otherCollider.gameObject.CompareTag("Spider0")) {
                    networkCommunication.IncrementDarts(3);

                    Destroy(contact.otherCollider.gameObject);

                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (contact.otherCollider.gameObject.CompareTag("Spider1")) {
                    networkCommunication.EnableDoublePoints();

                    Destroy(contact.otherCollider.gameObject);

                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else if (!contact.otherCollider.gameObject.CompareTag("Projectile")) // We hit another object that's not a dart, so let's destroy ourselves.
                {
                    Destroy(gameObject);
                }
            }

            if (networkCommunication.GetWinner() != null)
            {
                Debug.Log("Winner: " + networkCommunication.GetWinner());
            }
        }

        public void Update()
        {
        }


    }
}
