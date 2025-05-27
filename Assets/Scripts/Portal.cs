using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportDestination;        // Unde teleportam jucătorul
    public GameObject currentLevelRoot;          // GameObject care contine nivelul actual
    public GameObject nextLevelRoot;             // GameObject care contine urmatorul nivel

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleporteaza jucătorul
            other.transform.position = teleportDestination.position;

            // Dezactiveaza nivelul curent
            if (currentLevelRoot != null)
                currentLevelRoot.SetActive(false);

            // Activeaza nivelul urmator (în caz ca era dezactivat)
            if (nextLevelRoot != null)
                nextLevelRoot.SetActive(true);
        }
    }
}
