using UnityEngine;

public class MonsterLootDrop : MonoBehaviour
{
    public GameObject bagPrefab;

    public void SpawnBag()
    {
        Instantiate(bagPrefab, transform.position, Quaternion.identity);
    }
}