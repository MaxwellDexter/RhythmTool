using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject tempo;
    private TempoManager man;

    private void Start()
    {
        man = tempo.GetComponent<TempoManager>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            man.Tap();
        }
    }
}
