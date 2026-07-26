using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawlerMovment : MonoBehaviour
{
    public Vector2Int facingDirection = Vector2Int.up;
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private float moveTime = 0.15f;
    [SerializeField] private float rotationTime = 0.15f;
    [SerializeField] private float movementDelay = 0.1f;

    public Vector2 positionOffset;

    private bool isMoving;
    public bool hasMoved; // this is used by player controller to figure when its finished it movement
    [SerializeField] public Vector2Int gridLocation;

    public bool IsPlayer;

    [Header("SoundEffect")]
    [SerializeField] private List<string> footstepsSoundEffects;
    private int footstepsSoundEffectsIndex = 0;

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

        if (gridPosition.x <0 || gridPosition.y  <0 || DungeonManager.instance.currentRoom.GetTile(gridPosition.x, gridPosition.y).type == TileType.Empty)
        {
            return false;
        }
        return true;
    }
    public IEnumerator Move(Vector3 direction)
    {
        PlayFootStep();
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
        yield return new WaitForSeconds(movementDelay);
        isMoving = false;
        hasMoved = true;
    }

    public IEnumerator Rotate(float angle)
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
        yield return new WaitForSeconds(movementDelay);
        isMoving = false;
    }

    #region helpers
    
    
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

    
    #endregion

    public void PlayFootStep()
    {
        if (IsPlayer)
        {
            GameAudioManager.instance.PlaySoundEffectChannelTwo(footstepsSoundEffects[footstepsSoundEffectsIndex]);
            footstepsSoundEffectsIndex++;
            if(footstepsSoundEffectsIndex >= footstepsSoundEffects.Count)
            {
                footstepsSoundEffectsIndex = 0;
            }
        }
    }
}
