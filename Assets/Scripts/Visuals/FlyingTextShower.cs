using UnityEngine;

public class FlyingTextShower : MonoBehaviour
{
    public Transform flyingTextPrefab;

    public void ShowDamage(string text, Vector2 speed)
    {
        Instantiate(flyingTextPrefab).GetComponent<FlyingText>().SetUp(transform, text, speed);
    }
}
