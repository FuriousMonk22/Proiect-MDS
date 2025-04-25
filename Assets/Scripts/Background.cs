using UnityEngine;

public class HorizontalParallaxScroller : MonoBehaviour
{
    public GameObject cam;
    public float parallaxFactor = 0.5f;
    public float verticalFollowFactor = 0.9f;

    private Transform[] tiles = new Transform[3];
    private float spriteWidth;
    private int centerIndex = 1;
    private Vector3 lastCamPos;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;

        tiles[1] = transform; // center tile
        tiles[0] = Instantiate(gameObject, transform.position - Vector3.right * spriteWidth, Quaternion.identity, transform.parent).transform;
        tiles[2] = Instantiate(gameObject, transform.position + Vector3.right * spriteWidth, Quaternion.identity, transform.parent).transform;

        Destroy(tiles[0].GetComponent<HorizontalParallaxScroller>());
        Destroy(tiles[2].GetComponent<HorizontalParallaxScroller>());

        lastCamPos = cam.transform.position;
    }

    void LateUpdate()
    {
        Vector3 camPos = cam.transform.position;

        float deltaX = (camPos.x - lastCamPos.x) * parallaxFactor;
        float deltaY = (camPos.y - lastCamPos.y) * verticalFollowFactor;

        for (int i = 0; i < 3; i++)
        {
            tiles[i].position += new Vector3(deltaX, deltaY, 0);
        }

        lastCamPos = camPos;

        // Recenter tiles if the middle tile is too far from the camera
        float camX = camPos.x * parallaxFactor;
        float centerTileX = tiles[centerIndex].position.x;
        float dist = camX - centerTileX;

        if (Mathf.Abs(dist) >= spriteWidth / 2f)
        {
            if (dist > 0)
                ShiftRight();
            else
                ShiftLeft();
        }
    }

    void ShiftLeft()
    {
        Transform right = tiles[2];
        tiles[2] = tiles[1];
        tiles[1] = tiles[0];
        tiles[0] = right;

        tiles[0].position = tiles[1].position - new Vector3(spriteWidth, 0, 0);
    }

    void ShiftRight()
    {
        Transform left = tiles[0];
        tiles[0] = tiles[1];
        tiles[1] = tiles[2];
        tiles[2] = left;

        tiles[2].position = tiles[1].position + new Vector3(spriteWidth, 0, 0);
    }
}
