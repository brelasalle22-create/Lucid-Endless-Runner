using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Whether this obstacle is pink (true) or teal (false)
    public bool isPink = true;

    // The two colors obstacles can be (matches PlayerController)
    public Color pinkColor = new Color(1f, 0.78f, 0.86f);
    public Color tealColor = new Color(0.6f, 0.95f, 0.93f);

    // Reference to the SpriteRenderer
    private SpriteRenderer sr;

    // Start is called once when the game begins
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateObstacleColor();
    }

    // Set the visible color based on isPink
    void UpdateObstacleColor()
    {
        if (isPink == true)
        {
            sr.color = pinkColor;
        }
        else
        {
            sr.color = tealColor;
        }
    }
}