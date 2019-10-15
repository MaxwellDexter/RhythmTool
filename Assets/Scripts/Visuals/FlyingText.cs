using UnityEngine;
using TMPro;

public class FlyingText : MonoBehaviour
{
    private TextMeshPro text;

    private Vector3 force;
    private readonly static float MAX_DEATH_TIMER = 2f;
    private float deathTimer = MAX_DEATH_TIMER;

    private readonly float speedMultiplier = 1f;

    public void SetUp(Transform trans, string textToShow, Vector2 speed, Color textColor, bool useRandomSpread)
    {
        transform.position = trans.position;
        text = GetComponent<TextMeshPro>();
        text.SetText(textToShow);
        text.color = textColor;
        deathTimer = MAX_DEATH_TIMER;

        float speedX = speed.x;
        if (useRandomSpread && speed.x != 0)
        {
            speedX = Mathf.Sign(speed.x) * Random.Range(0.5f, 1.5f);
        }
        float speedY = speed.y;
        if (useRandomSpread && speed.y != 0)
        {
            speedY = Mathf.Sign(speed.y) * Random.Range(0.5f, 1.5f);
        }
        force = new Vector3(speedX, speedY, 0f);
    }
    
    void Update()
    {
        transform.position += force * speedMultiplier * Time.deltaTime;
        deathTimer -= Time.deltaTime;
        if (deathTimer < MAX_DEATH_TIMER / 2)
        {
            text.color *= new Color(text.color.r, text.color.g, text.color.b, text.color.a * 0.99f);
            text.fontSize *= 0.99f;
            force *= 0.99f;
        }
        if (deathTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
