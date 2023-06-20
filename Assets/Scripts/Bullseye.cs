using UnityEngine;

public class Bullseye : MonoBehaviour
{

    public GameObject missilePrefab;

    private void Update()
    {
        // Update the position of the Bullseye based on the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }
}
