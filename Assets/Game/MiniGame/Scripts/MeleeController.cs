using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    public RectTransform hitZoneGroup;
    public RectTransform missZone;
    public RectTransform indicator;
    public float hitZoneWidth = 100;
    public float critZoneWidth = 25;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        RandomizeHitZonePosition();
    }

    // Update is called once per frame
    void Update()
    {
        IndicatorBounce();
        CheckIndicatorInput();
    }

    public void RandomizeHitZonePosition()
    {
        //randomizes attack zone
        float redBarWidth = missZone.rect.width;
        float safeRange = redBarWidth / 2 - hitZoneWidth / 2;

        float randomX = Random.Range(-safeRange, safeRange);
        hitZoneGroup.anchoredPosition = new Vector2(randomX, hitZoneGroup.anchoredPosition.y);
    }

    public void IndicatorBounce()
    {
        //makes the indicator bounce from the left to the right side of the bar
        float x = Mathf.PingPong(Time.time * speed, missZone.rect.width);
        indicator.anchoredPosition = new Vector2(x - missZone.rect.width/2 + 15, indicator.anchoredPosition.y);
        
    }

    public void CheckIndicatorInput()
    {
        //Checks if the indicator is within the hit, crit or miss zone and returns the condition to match
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool inCritZone = indicator.anchoredPosition.x > (hitZoneGroup.anchoredPosition.x - critZoneWidth / 2) && indicator.anchoredPosition.x < (hitZoneGroup.anchoredPosition.x + critZoneWidth / 2);
            bool inHitZone = indicator.anchoredPosition.x > (hitZoneGroup.anchoredPosition.x - hitZoneWidth / 2) && indicator.anchoredPosition.x < (hitZoneGroup.anchoredPosition.x + hitZoneWidth / 2);

            if (inCritZone)
            {
                Debug.Log("Crit!");
                Destroy(gameObject);
            }
            else if (inHitZone)
            {
                Debug.Log("Hit!");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Miss!");
                Destroy(gameObject);
            }
        }
    }
}
