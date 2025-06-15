using UnityEngine;

public class HorizontalParallaxScroller : MonoBehaviour
{
    public GameObject player;                  // Reference to the player object
    public float parallaxFactor = 0.1f;        // Amount the background follows the player on X axis (e.g., 10%)
    public float verticalFollowFactor = 0.9f;  // Vertical movement factor (if you want the background to follow vertically too)
    public float repeatDistance = 10f;         // The distance after which the tile switches (in terms of background width)

    private Transform[] tiles = new Transform[3];  // 3 tiles: Left, Center, Right
    private float spriteWidth;
    private int centerIndex = 1;                // The center tile's index
    private Vector3 lastPlayerPos;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;   // Get the width of the sprite for shifting

        // Set up initial tiles
        tiles[1] = transform;  // center tile
        tiles[0] = Instantiate(gameObject, transform.position - Vector3.right * spriteWidth, Quaternion.identity, transform.parent).transform;  // left tile
        tiles[2] = Instantiate(gameObject, transform.position + Vector3.right * spriteWidth, Quaternion.identity, transform.parent).transform;  // right tile

        // Destroy the new HorizontalParallaxScroller components attached to the new tiles
        Destroy(tiles[0].GetComponent<HorizontalParallaxScroller>());
        Destroy(tiles[2].GetComponent<HorizontalParallaxScroller>());

        // Initial player position
        lastPlayerPos = player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 playerPos = player.transform.position;

        // Calculate the movement delta based on player's position and parallax factor
        float deltaX = (playerPos.x - lastPlayerPos.x) * parallaxFactor;
        float deltaY = (playerPos.y - lastPlayerPos.y) * verticalFollowFactor;

        // Apply the movement to all tiles
        for (int i = 0; i < 3; i++)
        {
            tiles[i].position += new Vector3(deltaX, deltaY, 0);
        }

        // Update the last position of the player
        lastPlayerPos = playerPos;

        // Check and switch tiles when the distance between the player and the center tile is large enough
        CheckTileSwitch();
    }

    // Switch tiles if they have moved a certain distance
    void CheckTileSwitch()
    {
        // Get the distance between the player and the center tile
        float dist = Mathf.Abs(tiles[centerIndex].position.x - player.transform.position.x);

        // If the distance is larger than the repeatDistance, shift tiles
        if (dist >= repeatDistance)
        {
            if (player.transform.position.x > tiles[centerIndex].position.x)
            {
                // Player is moving to the right, so shift the background to the left
                ShiftLeft();
            }
            else
            {
                // Player is moving to the left, so shift the background to the right
                ShiftRight();
            }
        }
    }

    // Shift tiles to the left
    void ShiftLeft()
    {
        // Move the left tile to the right of the right tile
        Transform left = tiles[0];
        tiles[0] = tiles[1];
        tiles[1] = tiles[2];
        tiles[2] = left;

        // Reposition the right tile to the right of the center tile
        tiles[2].position = tiles[1].position + new Vector3(spriteWidth, 0, 0);
    }

    // Shift tiles to the right
    void ShiftRight()
    {
        // Move the right tile to the left of the left tile
        Transform right = tiles[2];
        tiles[2] = tiles[1];
        tiles[1] = tiles[0];
        tiles[0] = right;

        // Reposition the left tile to the left of the center tile
        tiles[0].position = tiles[1].position - new Vector3(spriteWidth, 0, 0);
    }
}
