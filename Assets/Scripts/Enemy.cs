using UnityEngine;
using TMPro;

public class Enemy : BaseEnemy
{
    public GameObject wallBarrier;
    public GameObject fireballPrefab;
    
    public GameObject popUpPrefab;

    public Transform firePoint; // punctul din care pleaca flacara
    public float attackRange = 5f; // distanta de la care trage

    public float speed = 2f;
    public int damage = 10;
    public float attackCooldown = 2.5f;
    public int health = 50;

    private Transform player;
    private float lastAttackTime;

    public GameObject coinPrefab; // Reference to the coin prefab
    public int coinCount = 3; // Number of coins to drop when the monster dies

    private Vector3 originalScale; // Adaugat
    private Animator anim;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player nu a fost găsit în scenă de Enemy.cs");
        }
        originalScale = transform.localScale; // Salvam scala originala
        anim = GetComponent<Animator>();
    }

    void ShootFireball(Vector2 direction)
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        fireball.transform.localScale = new Vector3(2f, 2f, 1f);
        fireball.GetComponent<Rigidbody2D>().linearVelocity = direction * 6f; // viteza flăcarii
    }


    void Update()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            float distance = direction.magnitude;

            bool isInRange = distance <= attackRange;
            bool isTooClose = distance < 1f;

            // Daca e prea aproape, nu mai trage, eventual fugi sau stai
            if (isInRange && !isTooClose && PlayerPrefs.GetInt("CurrentHealth") > 0)
            {
                anim.SetBool("walk1", false);
                transform.position = transform.position; // sta pe loc

                // ataca la interval de timp
                if (Time.time > lastAttackTime + attackCooldown)
                {
                    anim.SetTrigger("attack2");
                    ShootFireball(direction.normalized);
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                anim.SetBool("walk1", true);
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
        if (collision.gameObject.CompareTag("Player") && Time.time > lastAttackTime + attackCooldown) // Verifica daca a trecut timpul de atac
        {
            // Ataca jucatorul
            Vector2 direction = collision.transform.position - transform.position;
            direction.Normalize(); // Normalizeaza directia pentru a obtine un vector unitate

            // Verifica daca inamicul este in raza de atac
            if (direction.magnitude <= attackRange && PlayerPrefs.GetInt("CurrentHealth") > 0)
            {
                anim.SetTrigger("attack2"); // Trigger pentru atac
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }


    public override void TakeDamage(int amount)
    {
        health -= amount; // Scade viata inamicului
        anim.SetTrigger("gethit1"); // Trigger pentru a lua damage

        GameObject popUp = Instantiate(popUpPrefab, transform.position, Quaternion.identity);
        popUp.GetComponentInChildren<TMP_Text>().text = amount.ToString();
        
        if (health <= 0)
        {
            wallBarrier.SetActive(false);
            anim.SetTrigger("death1"); // Trigger pentru moarte
            this.enabled = false; // Dezactiveaza scriptul inamicului
            Destroy(gameObject, 1.1f); // Distruge inamicul după 1.1 secunda
            for (int i = 0; i < coinCount; i++)
            {
                // Spawn a coin at the monster's position with a random offset (to avoid overlapping coins)
                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                Instantiate(coinPrefab, transform.position + randomOffset, Quaternion.identity);
            }
        }
    }
    
    

}