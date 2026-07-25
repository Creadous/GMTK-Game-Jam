using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawlerMovment : MonoBehaviour
{
    public Vector2Int facingDirection = Vector2Int.up;
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private float moveTime = 0.15f;
    [SerializeField] private float rotationTime = 0.15f;

    public Vector2 positionOffset;

    private bool isMoving;
    public bool hasMoved; // this is used by player controller to figure when its finished it movement
    [SerializeField] public Vector2Int gridLocation;

    // Update is called once per frame
    public void Start()
    {
        
    }
    public void FixedUpdate()
    {
        UpdateGridLocation(); // not great place to put this but it for debuging
    }
    public void MoveForward()
    {
        if (isMoving == true) return;
        if (CanMove(transform.forward))
        {
            StartCoroutine(Move(transform.forward));
        }
        else
        {
            //TODO camera shake
        }
    }
    public void MoveBackwards()
    {
        if (isMoving == true) return;
        if (CanMove(-transform.forward))
        {
            StartCoroutine(Move(-transform.forward));
        }
        else
        {
            //TODO camera shake
        }
    }
    public void RotateLeft()
    {
        if (isMoving == true) return;
        StartCoroutine(Rotate(-90f));
    }
    public void RotateRight()
    {
        if (isMoving == true) return;
        StartCoroutine(Rotate(90f));
    }
    public void UpdateGridLocation()
    {
        Vector3 startPos = transform.position;
        gridLocation = new Vector2Int(Mathf.RoundToInt((transform.position.x + positionOffset.x) / moveDistance), Mathf.RoundToInt((transform.position.z + positionOffset.y) / moveDistance));
    }
    public bool CanMove(Vector3 direction)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + direction * moveDistance;

        Vector2Int gridPosition = new Vector2Int(Mathf.RoundToInt((endPos.x + positionOffset.x) / moveDistance), Mathf.RoundToInt((endPos.z + positionOffset.y) / moveDistance));

        if (DungeonManager.instance.currentRoom.GetTile(gridPosition.x, gridPosition.y).type == TileType.Empty)
        {
            return false;
        }
        return true;
    }
    public IEnumerator Move(Vector3 direction)
    {
        isMoving = true;

        UpdateFacingDirection(direction);

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + direction * moveDistance;

        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / moveTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        isMoving = false;
        hasMoved = true;
    }

    private IEnumerator Rotate(float angle)
    {
        isMoving = true;

        Quaternion startRot = transform.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0, angle, 0);

        float elapsed = 0f;

        while (elapsed < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / rotationTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRot;
        isMoving = false;
    }

    #region helpers
    public Vector3 GridDirectionToWorld(Vector2Int direction)
    {
        if (direction.x > 0)
            return Vector3.right;

        if (direction.x < 0)
            return Vector3.left;

        if (direction.y > 0)
            return Vector3.forward;

        if (direction.y < 0)
            return Vector3.back;

        return Vector3.zero;
    }
    public void FaceDirection(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = targetRotation;
    }

    public Vector3 GetFacingWorldPosition()
    {
        Vector2Int attackGridPosition = gridLocation + facingDirection;

        return new Vector3(
            attackGridPosition.x * moveDistance,
            transform.position.y,
            attackGridPosition.y * moveDistance
        );
    }
    private void UpdateFacingDirection(Vector3 direction)
    {
        if (direction == Vector3.forward)
        {
            facingDirection = Vector2Int.up;
        }
        else if (direction == Vector3.back)
        {
            facingDirection = Vector2Int.down;
        }
        else if (direction == Vector3.right)
        {
            facingDirection = Vector2Int.right;
        }
        else if (direction == Vector3.left)
        {
            facingDirection = Vector2Int.left;
        }
    }

    public Vector2Int GetNextMove(Vector2Int enemyPos, Vector2Int playerPos)
    {
        Vector2Int nextPosition = enemyPos;

        int xDifference = playerPos.x - enemyPos.x;
        int yDifference = playerPos.y - enemyPos.y;

        // Move horizontally first
        if (xDifference != 0)
        {
            nextPosition.x += xDifference > 0 ? 1 : -1;
        }
        // Then move vertically
        else if (yDifference != 0)
        {
            nextPosition.y += yDifference > 0 ? 1 : -1;
        }

        return nextPosition;
    }
    #endregion
}
