using UnityEngine;

public class PuzzleManagerUI : MonoBehaviour
{
    public static PuzzleManagerUI Instance;
    public GameObject puzzlePanel;
    public GameObject[] pieces;
    public GameObject player;
    public bool puzzleCompleted = false;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!puzzleCompleted && CheckIfSolved())
        {
            Debug.Log("Puzzle rezolvat!");   
            puzzleCompleted = true;
            puzzlePanel.SetActive(false); // ascunde UI-ul puzzleului

            player.SetActive(true);

        }
    }

    bool CheckIfSolved()
    {
        Debug.Log("Verific puzzle-ul...");
        foreach (GameObject piece in pieces)
        {
            float z = piece.transform.eulerAngles.z;
            if (Mathf.Abs(z % 360) > 1f && Mathf.Abs(z % 360) < 359f)
                return false;
        }
        return true;
    }
}
