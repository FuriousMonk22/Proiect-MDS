using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject wallBarrier;
    public GameObject fireballPrefab;
    public Transform firePoint; // punctul din care pleacă flacăra
    public float attackRange = 5f; // distanța de la care trage

    public float speed = 2f;
    public int damage = 10;
    public float attackCooldown = 2.5f; 
    public int health = 50;

    private Transform player;
    private float lastAttackTime;

    public GameObject coinPrefab; // Reference to the coin prefab
    public int coinCount = 3; // Number of coins to drop when the monster dies

    private Vector3 originalScale; // <-- Adăugat
    private Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalScale = transform.localScale; // <-- Salvăm scala originală
        anim = GetComponent<Animator>();
    }

    void ShootFireball(Vector2 direction)
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        fireball.transform.localScale = new Vector3(2f, 2f, 1f);
        fireball.GetComponent<Rigidbody2D>().linearVelocity = direction * 6f; // viteza flăcării
    }


    void Update()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            float distance = direction.magnitude;

            bool isInRange = distance <= attackRange;
            bool isTooClose = distance < 1f;

            // Dacă e prea aproape, nu mai trage, eventual fugi sau stai
            if (isInRange && !isTooClose && PlayerPrefs.GetInt("CurrentHealth") > 0)
            {
                anim.SetBool("walk", false);
                transform.position = transform.position; // stă pe loc

                // atacă la interval de timp
                if (Time.time > lastAttackTime + attackCooldown)
                {
                    anim.SetTrigger("attack");
                    ShootFireball(direction.normalized);
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                anim.SetBool("walk", true);
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            // Flip spre jucător
            Vector3 scale = originalScale;
            scale.x *= direction.x > 0 ? 1 : -1;
            transform.localScale = scale;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time > lastAttackTime + attackCooldown)
        {   
            // Atacă jucătorul
            Vector2 direction = collision.transform.position - transform.position;
            direction.Normalize(); // Normalizează direcția pentru a obține un vector unitate

            // Verifică dacă inamicul este în raza de atac
            if (direction.magnitude <= attackRange && PlayerPrefs.GetInt("CurrentHealth") > 0)
            {
                anim.SetTrigger("attack"); // Trigger pentru atac
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        anim.SetTrigger("gethit"); // Trigger pentru a lua damage
        if (health <= 0)
        {
            wallBarrier.SetActive(false);
            anim.SetTrigger("death"); // Trigger pentru moarte
            this.enabled = false; // Dezactivează scriptul inamicului
            Destroy(gameObject, 1.1f); // Distruge inamicul după 1 secunde
            for (int i = 0; i < coinCount; i++)
                {
                    // Spawn a coin at the monster's position with a random offset (to avoid overlapping coins)
                    Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                    Instantiate(coinPrefab, transform.position + randomOffset, Quaternion.identity);
                }
        }
    }

}