using UnityEngine;

public class FlyingTextShower : MonoBehaviour
{
    public Transform flyingTextPrefab;
    private Color textColor;

    public void ShowText(string text, Vector2 speed, bool useRandomSpread)
    {
        Instantiate(flyingTextPrefab).GetComponent<FlyingText>().SetUp(transform, text, speed, textColor, useRandomSpread);
    }

    public void SetColor(Color color)
    {
        textColor = color;
    }
}
