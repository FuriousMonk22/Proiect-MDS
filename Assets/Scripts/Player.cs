using UnityEngine;
using UnityEngine.SceneManagement; //pentru a schimba scenele
using System.Collections; //pentru a folosi coroutinele
using TMPro; //pentru a folosi TextMeshPro

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; //cat de repede se misca playerul
    public float jumpForce = 7f; //forta aplicata la salt
    public float acceleration = 10f; //cat de repede accelereaza playerul
    public GameObject gameOver; //textul de Game Over


    [Header("Jump Control")]
    public float lowGravityScale = 0.5f;   // gravitatie redusa cand tii spatiu apasat si personajul urca
    public float fallGravityScale = 2f;    // gravitatie mai mare cand personajul coboara
    public float defaultGravityScale = 1f; // valoarea normala a gravitatiei

    private Rigidbody2D plr; //corpul rigid al playerului
    private Animator anim;

    private bool isGrounded; //verifica daca playerul este pe pamant
    private float currentVelocityX; //viteza curenta pe axa X
    private bool jumpRequested = false; //verifica daca a fost cerut un salt

    [Header("Ground Check")]
    public Transform groundCheck; //transformarea care verifica daca playerul este pe pamant
    public float groundCheckRadius = 0.2f; //raza de verificare a pamantului
    public LayerMask groundLayer; //layerul pe care se verifica daca playerul este pe pamant

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    private bool isDead = false; //verifica daca playerul este mort

    [Header("Coins")]
    public int coins = 0;

    public Transform attackPoint;
    [Header("Attack Settings")]
    public float attackRange = 0.5f;       // cât de mare e zona în care lovește
    public int attackDamage = 20;         // cât damage dă
    public LayerMask enemyLayers;         // ce e considerat inamic
    public float attackRate = 1f;         // cât de des poți ataca
    private float nextAttackTime = 0f;    // timpul când poți ataca din nou

    private void Awake() //initializare
    {
        plr = GetComponent<Rigidbody2D>(); //prindem componenta Rigidbody2D a playerului
        anim = GetComponent<Animator>(); //prindem componenta Animator a playerului
        PlayerPrefs.SetInt("CurrentHealth", currentHealth);
    }

    private void Update() //input de la tastatura
    {
        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //verifica daca playerul este pe pamant si apasa spatiu
        {
            jumpRequested = true; //cerem un salt
        }

        if (Time.time >= nextAttackTime)
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack(); 
                nextAttackTime = Time.time + 1f / attackRate; //seteaza timpul urmatorului atac
            }
    }

    private void FixedUpdate() //logica fizica
    {
        if (isDead)
            return; //daca playerul este mort, nu mai face nimic

        healthBar.SetHealth((float)PlayerPrefs.GetInt("CurrentHealth") / (float)maxHealth);

        // verifica daca playerul este pe pamant
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Se citeset inputul pentru miscare
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.A))
            moveInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            moveInput = 1f;

        // Se aplica miscarea pe axa X
        float targetVelocityX = moveInput * moveSpeed;
        currentVelocityX = Mathf.Lerp(plr.linearVelocity.x, targetVelocityX, acceleration * Time.fixedDeltaTime);
        plr.linearVelocity = new Vector2(currentVelocityX, plr.linearVelocity.y);

        // Daca playerul este pe pamant si a cerut un salt, se aplica forta de salt
        if (jumpRequested)
        {
            plr.linearVelocity = new Vector2(plr.linearVelocity.x, jumpForce);
            anim.SetTrigger("jump"); // Trigger pentru animatia de salt
            anim.Play("jump", 0, 0f);
            isGrounded = false; // Seteaza grounded la false pentru a evita salturi multiple
            jumpRequested = false;
        }

        // Se aplica gravitatia in functie de starea playerului
        if (plr.linearVelocity.y > 0 && Input.GetKey(KeyCode.Space))
        {
            // Urcare: scade gravitatia pentru un salt mai lung
            plr.gravityScale = lowGravityScale;
        }
        else if (plr.linearVelocity.y < 0)
        {
            // Cazul de cadere: creste gravitatia pentru o cadere mai rapida
            plr.gravityScale = fallGravityScale;
        }
        else
        {
            // Cazul normal: seteaza gravitatia la valoarea normala
            plr.gravityScale = defaultGravityScale;
        }

        float horizontalInput = Input.GetAxis("Horizontal"); //inputul orizontal

        if (horizontalInput > 0.01f) //daca playerul se misca spre dreapta
        {
            transform.localScale = new Vector3(-1, 1, 1); ; //inversam sprite-ul pe axa X
        }
        else if (horizontalInput < -0.01f) //daca playerul se misca spre stanga
        {
            transform.localScale = Vector3.one; //inversam sprite-ul pe axa X
        }

        // Seteaza animatia de miscare in functie de viteza
        anim.SetBool("run", horizontalInput != 0); //daca playerul se misca, seteaza animatia de miscare

        anim.SetBool("grounded", isGrounded); //daca playerul sare, seteaza animatia de salt


        if (PlayerPrefs.GetInt("CurrentHealth") <= 0 && !isDead) //daca playerul nu mai are viata
        {   isDead = true; //seteaza playerul ca fiind mort
            anim.SetTrigger("Dead"); // Trigger pentru animatia de moarte
            plr.linearVelocity = Vector2.zero; //opreste playerul
            plr.gravityScale = defaultGravityScale; //seteaza gravitatia la valoarea normala
            StartCoroutine(DeathSequence()); //incepe secventa de moarte
        }
    }
    public void TakeDamage(int amount)
    {
        currentHealth = PlayerPrefs.GetInt("CurrentHealth"); //prindem viata curenta a playerului
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        PlayerPrefs.SetInt("CurrentHealth", currentHealth);

        // If using image-based health bar
        // healthBar.SetHealthImageFill((float)currentHealth / maxHealth);
    }

    public void AddCoin(int amount)
    {
        coins += amount;
    }

    private IEnumerator DeathSequence() //secventa de moarte
    {
        if (gameOver != null) //verifica daca textul de Game Over este setat
        {
            gameOver.SetActive(true); //activeaza textul de Game Over
        }
        
        yield return new WaitForSeconds(5f); //asteapta 5 secunde
        ReturnToMenu(); //returneaza la meniu
    }

    private void ReturnToMenu() //functie care returneaza la meniu
    {
        SceneManager.LoadScene("MainScene"); //schimba scena la MainMenu
    }

    void Attack()
    {
        anim.SetTrigger("attack"); // Trigger pentru animatia de atac

        //Gaseste toti inamicii in zona de atac
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name); //afiseaza in consola numele inamicului lovit
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage); //aplica damage inamicilor
            Vector3 spawnPos = enemy.transform.position + new Vector3(0, 1f, 0); // deasupra inamicului
        }
    }
}
