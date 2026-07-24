using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleController : MonoBehaviour
{
    public RectTransform approachRing;
    public float shrinkDuration = 1.5f;
    public Vector2 startSize = new Vector2(250f, 250f);
    public Vector2 endSize = new Vector2(0, 0);

    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / shrinkDuration;

        Vector2 currentSize = Vector2.Lerp(startSize, endSize, t);
        approachRing.sizeDelta = currentSize;

        if (t > 1)
        {
            Destroy(gameObject);
        }
    }
}
