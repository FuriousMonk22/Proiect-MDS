using UnityEngine;
using TMPro;

public class Enemy1 : BaseEnemy
{
    public GameObject popUpPrefab;
    public float attackRange = 1.5f;
    public float speed = 2f;
    public int damage = 15;
    public float attackCooldown = 1.5f;
    public int health = 60;

    private Transform player;
    private float lastAttackTime;

    public GameObject coinPrefab;
    public int coinCount = 5;

    private Vector3 originalScale;
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

        originalScale = transform.localScale;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null || PlayerPrefs.GetInt("CurrentHealth") <= 0)
            return;

        Vector2 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (distance > 10) {
            anim.SetBool("walk", false);
            return;
        }

        // Flip spre jucător
            Vector3 scale = originalScale;
        scale.x *= direction.x > 0 ? 1 : -1;
        transform.localScale = scale;

        if (distance <= attackRange)
        {
            anim.SetBool("walk", false);

            if (Time.time > lastAttackTime + attackCooldown)
            {
                anim.SetTrigger("attack");
                lastAttackTime = Time.time;

                // Damage-ul se aplica direct aici, fără Animation Event
                TryDealDamageToPlayer();
            }
        }
        else
        {
            anim.SetBool("walk", true);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void TryDealDamageToPlayer()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Player playerScript = player.GetComponent<Player>();
            if (playerScript != null && PlayerPrefs.GetInt("CurrentHealth") > 0)
            {
                Debug.Log("Enemy hit the player without animation event!");
                playerScript.TakeDamage(damage);
            }
        }
    }


    public override void TakeDamage(int amount)
    {
        health -= amount;
        if (anim != null)
            anim.SetTrigger("gethit");

        if (popUpPrefab != null)
        {
            GameObject popUp = Instantiate(popUpPrefab, transform.position, Quaternion.identity);
            popUp.GetComponentInChildren<TMP_Text>().text = amount.ToString();
        }

        if (health <= 0)
        {
            anim.SetTrigger("death");
            this.enabled = false;
            Destroy(gameObject, 1.1f);

            for (int i = 0; i < coinCount; i++)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                Instantiate(coinPrefab, transform.position + randomOffset, Quaternion.identity);
            }
        }
    }
}
