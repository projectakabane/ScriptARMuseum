using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float speed = 50f;
    public float resetX = -1000f; // When offscreen left
    public float startX = 1000f;  // Where it spawns again (offscreen right)
    public float minY = -100f;    // Y spawn range
    public float maxY = 100f;

    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.anchoredPosition += Vector2.left * speed * Time.deltaTime;

        if (rect.anchoredPosition.x < resetX)
        {
            // Reset to the right with random Y
            float randomY = Random.Range(minY, maxY);
            rect.anchoredPosition = new Vector2(startX, randomY);
        }
    }
}
