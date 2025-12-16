using UnityEngine;

public class EagleGenerator : MonoBehaviour
{
    public GameObject[] eaglePrefabs;

    public Transform pointA;
    public Transform pointB;

    public float spawnInterval = 30f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEagle();
            timer = 0f;
        }
    }

    public void SpawnEagle()
    {
        if (eaglePrefabs.Length == 0 || pointA == null || pointB == null)
            return;

        int randomIndex = Random.Range(0, eaglePrefabs.Length);
        GameObject eagle = Instantiate(
            eaglePrefabs[randomIndex],
            pointA.position,
            Quaternion.identity
        );

        Eagle eagleScript = eagle.GetComponent<Eagle>();
        if (eagleScript != null)
        {
            eagleScript.pointA = pointA;
            eagleScript.pointB = pointB;
        }
    }
}
