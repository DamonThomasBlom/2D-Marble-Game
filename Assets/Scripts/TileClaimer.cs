using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClaimer : MonoBehaviour
{
    public ObjectPooler pooler;
    public Tilemap tilemap;       // Assign once (shared reference is better)
    public TileBase teamTile;     // This ball’s team tile
    public BallGun ballGun;

    private Vector3Int lastCell;  // Last tile claimed
    private static readonly Vector3Int InvalidCell = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
    private Vector2 _lastVelocity;
    private Rigidbody2D _rigidBody;

    private void OnEnable()
    {
        if (tilemap == null)
            tilemap = FindObjectOfType<Tilemap>();
        if (_rigidBody == null)
            _rigidBody = GetComponent<Rigidbody2D>();
        lastCell = InvalidCell;
    }

    private void FixedUpdate()  // Physics rate is better for consistency + performance
    {
        _lastVelocity = _rigidBody.velocity;

        Vector3 worldPos = transform.position;
        Vector3Int cellPos = tilemap.WorldToCell(worldPos);

        if (cellPos == lastCell) return;

        TileBase currentTile = tilemap.GetTile(cellPos);

        // Only change if it's different and not null
        if (currentTile != null && currentTile != teamTile)
        {
            tilemap.SetTile(cellPos, teamTile);
            lastCell = cellPos;
            pooler.ReturnToPool(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "TileMap")
        {
            Vector2 normal = collision.GetContact(0).normal;
            Vector2 reflected = Vector2.Reflect(_lastVelocity, normal);

            _rigidBody.velocity = reflected;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BallGun>() != null)
        {
            Debug.Log("Hit Gun");
            var hitGun = collision.gameObject.GetComponent<BallGun>();
            if (hitGun != ballGun)
                hitGun.Die();
        }
    }
}
