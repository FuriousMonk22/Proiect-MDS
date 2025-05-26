using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportDestination;        // Unde teleportăm jucătorul
    public GameObject currentLevelRoot;          // GameObject care conține nivelul actual
    public GameObject nextLevelRoot;             // GameObject care conține următorul nivel

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleportează jucătorul
            other.transform.position = teleportDestination.position;

            // Dezactivează nivelul curent
            if (currentLevelRoot != null)
                currentLevelRoot.SetActive(false);

            // Activează nivelul următor (în caz că era dezactivat)
            if (nextLevelRoot != null)
                nextLevelRoot.SetActive(true);
        }
    }
}
