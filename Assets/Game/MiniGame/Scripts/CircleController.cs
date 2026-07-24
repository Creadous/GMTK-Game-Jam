using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleController : MonoBehaviour
{
    public RectTransform approachRing;
    public float shrinkDuration = 2f;
    public Vector2 startSize = new Vector2(300f, 300f);
    public Vector2 endSize = new Vector2(0, 0);
    public float hitWindowStart = 1.06f;
    public float critWindowStart = 0.9f;
    public float critWindowEnd = 1.05f;

    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //keeps track of the time
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / shrinkDuration;
        //keeps track of the size of the approaching ring
        Vector2 currentSize = Vector2.Lerp(startSize, endSize, t);
        approachRing.sizeDelta = currentSize;
        //if the ring shrinks the full shrinkDuration, it gets deleted
        if (t > 1)
        {
            Destroy(gameObject);
            Debug.Log("Miss!");
        }
    }

    public void OnButtonClicked()
    {
        //clicking after the hitWindowStart registers a hit
        Debug.Log(elapsedTime);
        if (elapsedTime > hitWindowStart)
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
        //clicking inbetween the critWindow start and stop registers a crtical hit
        else if (elapsedTime > critWindowStart && elapsedTime < critWindowEnd)
        {
            Debug.Log("Crit!");
            Destroy(gameObject);
        }
        //clicking too early registers a miss
        else if (elapsedTime < critWindowStart)
        {
            Debug.Log("Miss!");
            Destroy(gameObject);
        }
    }    
}
