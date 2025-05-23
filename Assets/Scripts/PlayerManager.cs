using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    public bool IsMoving { get; private set; }
    [SerializeField] private float timeToMove = 0.2f;
    public Tilemap grid;
    public RuleTile ruleTile;

    private void Start()
    {
        StartCoroutine(MovePlayerRoutine());
    }

    // ReSharper disable once IteratorNeverReturns
    private IEnumerator MovePlayerRoutine()
    {
        while (true)
        {
            var direction = GetDirection();
            var targetPos = transform.position;
            while (direction == GetDirection() && direction != Vector3.zero)
            {
                // Check if target cell matches rule tile
                if (grid.GetTile(grid.WorldToCell(targetPos + direction)) == ruleTile) break;
            
                targetPos += direction;
                IsMoving = true;
                while (IsUnderPosition(direction, transform.position, targetPos))
                {
                    transform.position += direction * Time.deltaTime / timeToMove;
                    yield return null;
                }
            }

            transform.position = targetPos;
            IsMoving = false;

            yield return null;
        }
    }

    private static bool IsUnderPosition(Vector3 direction, Vector3 a, Vector3 b)
    {
        return direction switch
        {
            { x: >= 0, y: >= 0 } => a.x <= b.x && a.y <= b.y,
            { x: < 0, y: >= 0 } => a.x >= b.x && a.y <= b.y,
            { x: >= 0, y: < 0 } => a.x <= b.x && a.y >= b.y,
            { x: < 0, y: < 0 } => a.x >= b.x && a.y >= b.y,
            _ => false
        };
    }
    private Vector3 GetDirection()
    {
        var direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _ = Mathf.Abs(direction.x) < Mathf.Abs(direction.y) ? direction.x = 0 : direction.y = 0;
        direction.Normalize();
        return direction;
    }
}