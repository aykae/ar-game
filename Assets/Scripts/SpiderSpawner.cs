using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class SpiderSpawner : MonoBehaviour
    {
        public GameObject spider0; // Assign your spider prefab here in the Inspector
        public GameObject spider1; // Assign your spider prefab here in the Inspector

        void Start()
        {
            SpawnObjectAtRandomPositionOnDartboard();
        }

        void SpawnObjectAtRandomPositionOnDartboard()
        {
            // Generate a random position within the specified bounds
            Vector3 randomPosition = GenerateRandomPositionWithinBounds();
            
            // Adjust the Z position as per your requirement
            randomPosition.z = 1.7f; // Hardcoded Z position

            // Instantiate the spider with the required rotation and scale
            GameObject spawnedSpider;
            if (Random.Range(0, 2) == 0)
            {
                spawnedSpider = Instantiate(spider0, randomPosition, Quaternion.Euler(-90, 0, 0));
            }
            else
            {
                spawnedSpider = Instantiate(spider1, randomPosition, Quaternion.Euler(-90, 0, 0));
            }
            spawnedSpider.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }

        Vector3 GenerateRandomPositionWithinBounds()
        {
            // Define the center and radius of the circular area on the dartboard
            Vector2 center = new Vector2((0.17f + -0.5f) / 2, (1.47f + 0.7f) / 2);
            float radius = Mathf.Min(Mathf.Abs(0.17f - -0.5f), Mathf.Abs(1.47f - 0.7f)) / 2;

            // Generate a random angle
            float angle = Random.Range(0, Mathf.PI * 2);

            // Calculate X and Y using the angle and radius, ensuring the spider spawns within the circle
            float x = center.x + Mathf.Cos(angle) * radius;
            float y = center.y + Mathf.Sin(angle) * radius;

            return new Vector3(x, y, 0); // Return the calculated position
        }
    }
}
