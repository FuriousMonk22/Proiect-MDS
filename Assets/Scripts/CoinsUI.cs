using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public GameObject player; // Assign your player GameObject in the inspector

    private Player playerScript;

    void Start()
    {
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }
    }

    void Update()
    {
        if (playerScript != null && coinText != null)
        {
            coinText.text = playerScript.coins.ToString();
        }
    }
}
