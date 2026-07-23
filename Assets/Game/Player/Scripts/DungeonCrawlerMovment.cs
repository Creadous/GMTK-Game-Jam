using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawlerMovment : MonoBehaviour
{
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private float moveTime = 0.15f;
    [SerializeField] private float rotationTime = 0.15f;

    private bool isMoving;
    [SerializeField] private Vector2Int gridLocation;

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
        gridLocation = new Vector2Int(Mathf.RoundToInt(startPos.x / (float) moveDistance), Mathf.RoundToInt(startPos.z/ (float) moveDistance));
    }
    public bool CanMove(Vector3 direction)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + direction * moveDistance;

        Vector2Int gridPosition = new Vector2Int(Mathf.RoundToInt(endPos.x /(float)moveDistance),Mathf.RoundToInt(endPos.z/ (float)moveDistance));

        if (DungeonManager.instance.currentRoom.GetTile(gridPosition.x, gridPosition.y).type == TileType.Empty)
        {
            return false;
        }
        return true;
    }
    private IEnumerator Move(Vector3 direction)
    {
        isMoving = true;

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
}
