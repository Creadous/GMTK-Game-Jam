using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMiniGame : MonoBehaviour
{
    public GameObject hitCirclePrefab;
    public Transform spawnParent;
    public float delayBetweenSpawn = 1;
    public float halfWidth;
    public float halfHeight;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnSequence());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnSequence()
    {
        for (int i = 0; i < 3; i++)
        {
            //Creates a new random position and spawns a circle at that position, 3 times
            Vector2 randomPosition = new Vector2(Random.Range(-spawnParent.GetComponent<RectTransform>().rect.width/2, spawnParent.GetComponent<RectTransform>().rect.width/2), Random.Range(-spawnParent.GetComponent<RectTransform>().rect.height / 2, spawnParent.GetComponent<RectTransform>().rect.height/2));
            //Debug.Log(randomPosition);
            GameObject newCircle = Instantiate(hitCirclePrefab, spawnParent);
            newCircle.GetComponent<RectTransform>().anchoredPosition = randomPosition;

            yield return new WaitForSeconds(delayBetweenSpawn);
        }
    }
}
