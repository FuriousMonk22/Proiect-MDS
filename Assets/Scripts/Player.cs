using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; //cat de repede se misca playerul
    public float jumpForce = 7f; //forta aplicata la salt
    public float acceleration = 10f; //cat de repede accelereaza playerul

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


    private void Awake() //initializare
    {
        plr = GetComponent<Rigidbody2D>(); //prindem componenta Rigidbody2D a playerului
        anim = GetComponent<Animator>(); //prindem componenta Animator a playerului 
    }

    private void Update() //input de la tastatura
    {
        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //verifica daca playerul este pe pamant si apasa spatiu
        {
            jumpRequested = true; //cerem un salt
        }
    }

    private void FixedUpdate() //logica fizica
    {
        healthBar.SetHealth((float)currentHealth / (float)maxHealth);

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
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // If using image-based health bar
        // healthBar.SetHealthImageFill((float)currentHealth / maxHealth);
    }
}
