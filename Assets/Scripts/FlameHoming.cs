using UnityEngine;

public class HomingFlame : Flame
{
    public float homingSpeed = 5f;
    public float homingStrength = 0.5f;

    private Transform playerTransform;

    void Start()
    {
        base.Start();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    protected override void CustomUpdate()
    {
        if (playerTransform != null)
        {
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
            Vector2 currentVelocity = rb.linearVelocity.normalized;
            Vector2 newDirection = Vector2.Lerp(currentVelocity, directionToPlayer, homingStrength * Time.fixedDeltaTime).normalized;
            transform.Rotate(0f, 0f, 45 * Time.deltaTime);

            rb.linearVelocity = newDirection * homingSpeed;
        }
    }
}
