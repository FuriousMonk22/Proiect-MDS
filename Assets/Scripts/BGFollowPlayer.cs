using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    [Tooltip("The target player to move towards")]
    public Transform playerTransform;

    [Tooltip("Starting position of this object")]
    public Vector3 initialPosition;

    [Tooltip("Fraction of the distance to move each frame (0-1)")]
    [Range(0f, 1f)]
    public float moveFraction = 0.1f;

    void Start()
    {
        transform.position = initialPosition;
    }

    void Update()
{
    if (playerTransform == null)
        return;

    // Calculate new position using Lerp
    Vector3 newPos = Vector3.Lerp(initialPosition, playerTransform.position, moveFraction);

    // Keep original Z position to prevent moving along Z axis
    newPos.z = initialPosition.z;

    transform.position = newPos;
}

}
