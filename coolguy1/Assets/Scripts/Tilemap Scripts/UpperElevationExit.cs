using UnityEngine;

public class UpperElevationExit : MonoBehaviour
{
    public Collider2D[] upperMountainColliders;
    public Collider2D[] upperBoundaryColliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D upperMountain in upperMountainColliders)
            {
                upperMountain.enabled = true;
            }
            foreach (Collider2D upperBoundary in upperBoundaryColliders)
            {
                upperBoundary.enabled = false;
            }
        }

        collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
    }
}
