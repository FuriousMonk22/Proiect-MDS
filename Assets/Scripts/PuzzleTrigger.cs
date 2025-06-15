using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject puzzleUI;
    public Player playerScript;

    private void Start()
    {
        if (playerScript == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                playerScript = playerObject.GetComponent<Player>();
            }
            else
            {
                Debug.LogError("Nu am găsit un obiect cu tag-ul 'Player'!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PuzzleManagerUI.Instance != null && PuzzleManagerUI.Instance.puzzleCompleted)
            {
                Debug.Log("Puzzle-ul este deja completat. Nu mai afișăm UI-ul.");
                return;
            }

            Debug.Log("Activ puzzle UI...");
        
            if (puzzleUI != null)
                puzzleUI.SetActive(true);

            if (playerScript != null)
            {
                playerScript.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("playerScript este null în OnTriggerEnter2D!");
            }
        }
    }

}
