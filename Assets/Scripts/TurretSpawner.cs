using UnityEngine;

public class PrefabRespawner : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject prefabToSpawn;
    public Vector3 spawnRotation = Vector3.zero;

    [Header("Respawn Settings")]
    public float speed = 5f;
    public float respawnDelay = 2f;
    public float startDelay = 0.5f; // 🔸 NEW: delay before first shot

    void Start()
    {
        StartCoroutine(RespawnLoop());
    }

    public void SpawnPrefab()
{
    Quaternion rotation = Quaternion.Euler(spawnRotation);
    GameObject instance = Instantiate(prefabToSpawn, transform.position, rotation);

    // Convert Z angle to direction vector
    float angleRad = spawnRotation.z * Mathf.Deg2Rad;
    Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

    Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        rb.linearVelocity = direction.normalized * speed;
    }
}

    private System.Collections.IEnumerator RespawnLoop()
    {
        // 🔸 Wait before first shot
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            SpawnPrefab();
            yield return new WaitForSeconds(respawnDelay);
        }
    }
}
