using UnityEngine;
using TMPro;


[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Boss : BaseEnemy
{
    public float acceleration = 10f;
    public float maxSpeed = 4f;
    public float attackInterval = 5f;
    public float attackInterval2 = 7.5f;
    public GameObject attackPrefab; // assign your prefab here
    public GameObject rainPrefab; // assign your prefab here
    public float attackSpawnDistance = 1f;
    public float attackProjectileSpeed = 5f; // Speed of the projectile

    private Transform playerTransform;
    private Rigidbody2D rb;
    private Animator anim;
    private float attackTimer = 5f;
    private float attackTimer2 = 7.5f;

    public int rainProjectileCount = 5;             // How many projectiles to spawn
    public float rainSpacing = 20f;                 // Space between them on X
    public float rainHeightOffset = 100f;           // Height above the boss

    public int health = 500;

    public GameObject coinPrefab; // Reference to the coin prefab
    public int coinCount = 100; // Number of coins to drop when the monster dies

    public GameObject popUpPrefab;
    public GameObject winner;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackTimer = attackInterval;
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
        attackTimer -= Time.deltaTime;
        attackTimer2 -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            attackTimer = attackInterval;
            Attack();
        }
        if (attackTimer2 <= 0f)
        {
            attackTimer2 = attackInterval2;
            Attack2();
        }
    }

    void FixedUpdate()
    {
        if (playerTransform == null) return;

        float xDirection = playerTransform.position.x - transform.position.x;
        Vector2 direction = new Vector2(xDirection, 0).normalized;

        rb.AddForce(direction * acceleration);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);

        bool isRunning = rb.linearVelocity.sqrMagnitude > 0.1f;
        anim.SetBool("run", isRunning);

        if (rb.linearVelocity.x > 0.1f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (rb.linearVelocity.x < -0.1f)
            transform.localScale = new Vector3(1, 1, 1);
    }

    void Attack()
    {
        anim.SetTrigger("attack"); // Assumes "attack" is a trigger in your Animator
        SpawnAttackObject();
    }

    void Attack2() {
    if (attackPrefab == null) return;

    float baseX = transform.position.x;
    float spawnY = transform.position.y + rainHeightOffset;

    for (int i = 0; i < rainProjectileCount; i++)
    {
        // Calculate spawn position
        float offsetX = (i - rainProjectileCount / 2f) * rainSpacing;
        Vector3 spawnPos = new Vector3(baseX + offsetX, spawnY, 0);

        // Instantiate projectile
        GameObject projectile = Instantiate(rainPrefab, spawnPos, Quaternion.identity);

        // Face downward (optional: flip if your sprite needs it)
        projectile.transform.localScale = new Vector3(
            Mathf.Abs(projectile.transform.localScale.x),  // Reset X scale
            Mathf.Abs(projectile.transform.localScale.y),  // Reset Y scale
            projectile.transform.localScale.z
        );

        // Give it downward velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, -attackProjectileSpeed);
        }
    }
    }

    // This should be called by an animation event at the right frame in the attack animation
    public void SpawnAttackObject()
    {
        if (attackPrefab == null) return;

        // Determine direction based on localScale.x (sprite flip)
        float direction = transform.localScale.x > 0 ? -1f : 1f;

        // Spawn position is in front of the boss
        Vector3 spawnPosition = transform.position + new Vector3(direction * attackSpawnDistance, 0.8f, 0);

        // Instantiate the prefab
        GameObject projectile = Instantiate(attackPrefab, spawnPosition, Quaternion.identity);

        // Flip the prefab's sprite based on direction
        Vector3 scale = projectile.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction; // Flip only X based on direction
        projectile.transform.localScale = scale;

        // Apply velocity if it has a Rigidbody2D
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(direction * attackProjectileSpeed, 0f);
        }
    }

    public override void TakeDamage(int amount)
    {
        health -= amount; // Scade viata inamicului

        GameObject popUp = Instantiate(popUpPrefab, transform.position, Quaternion.identity);
        popUp.GetComponentInChildren<TMP_Text>().text = amount.ToString();

        if (health <= 0)
        {
            anim.SetBool("isDeath", true); // Trigger pentru moarte
            this.enabled = false; // Dezactiveaza scriptul inamicului
            Destroy(gameObject, 1.1f); // Distruge inamicul după 1.1 secunda
            for (int i = 0; i < coinCount; i++)
            {
                // Spawn a coin at the monster's position with a random offset (to avoid overlapping coins)
                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f) + 0.2f, 0);
                Instantiate(coinPrefab, transform.position + randomOffset, Quaternion.identity);
            }
            winner.SetActive(true);
        }
    }
}
