using UnityEngine;

public class FlyingTextShower : MonoBehaviour
{
    public Transform flyingTextPrefab;
    private Color32 textColor;

    public void ShowText(string text, Vector2 speed, bool useRandomSpread)
    {
        Instantiate(flyingTextPrefab).GetComponent<FlyingText>().SetUp(transform, text, speed, textColor, useRandomSpread);
    }

    public void SetColor(Color32 color)
    {
        textColor = color;
    }
}
