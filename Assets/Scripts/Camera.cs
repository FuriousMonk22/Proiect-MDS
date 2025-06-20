using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;  // Drag your Player GameObject here
    [SerializeField] private Vector3 offset;    // Optional: offset from the player

    void LateUpdate()
    {
        if (player != null)
        {
            //Vector3 temp = player.position;
            transform.position = player.position + offset;
        }
    }
}