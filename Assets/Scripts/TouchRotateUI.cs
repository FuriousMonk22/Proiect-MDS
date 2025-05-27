using UnityEngine;
using UnityEngine.UI;

public class TouchRotateUI : MonoBehaviour
{
    public void RotatePiece()
    {
        if (!PuzzleManagerUI.Instance.puzzleCompleted)
            transform.Rotate(0f, 0f, 90f);
    }

    // Dacă vrei să meargă și pe click din editor:
    private void OnMouseDown()
    {
        RotatePiece();
    }
}
