using UnityEngine;
using UnityEngine.Tilemaps;

public class Ball : MonoBehaviour
{
    public TileBase YourTeamTile;
    public Tilemap tilemap;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Ball");

        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector3 worldPos = contact.point;
            Vector3Int tilePos = tilemap.WorldToCell(worldPos);

            // Example: claim the tile
            TileBase hitTile = tilemap.GetTile(tilePos);
            if (hitTile != YourTeamTile)
            {
                tilemap.SetTile(tilePos, YourTeamTile);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ball trigger " + collision.name);
    }
}
