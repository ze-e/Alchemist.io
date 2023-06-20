using UnityEngine;

public class Bullseye : MonoBehaviour
{

    public GameObject missilePrefab;

    private void Update()
    {
        // Update the position of the Bullseye based on the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);

        // Check if the player presses the Space key
        if ( Input.GetMouseButtonDown(0))
        {

            // Get the Player position
            GameObject player = GameObject.Find("Player");
            Vector3 playerPosition = player.transform.position;

            // Instantiate the Missile object at the Player position
            GameObject missile = Instantiate(missilePrefab, playerPosition, Quaternion.identity);

            // Get the Missile component and set the target position as the Bullseye position
            Missile missileComponent = missile.GetComponent<Missile>();
            if (missileComponent != null)
            {
                missileComponent.SetTargetPosition(transform.position);
            }
        }
    }
}
