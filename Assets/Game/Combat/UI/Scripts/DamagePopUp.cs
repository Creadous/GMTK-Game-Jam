using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopUp : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float speed = 25f;
    public float lifetime = 1f;

    Vector3 direction;

    void Awake()
    {
        direction = new Vector3(Random.Range(-3f, 3f), Random.Range(0.1f, 1f), 0f).normalized;
    }

    public void Setup(int damage, Color color)
    {
        text.text = damage.ToString();
        text.color = color;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        Color c = text.color;
        c.a -= Time.deltaTime / lifetime;
        text.color = c;

        if (c.a <= 0)
            Destroy(gameObject);
    }
}
