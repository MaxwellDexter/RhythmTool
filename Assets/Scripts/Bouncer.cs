using UnityEngine;

public class Bouncer : AbstractTempoReceiver, ITempoReceiver
{
    private double secsPerAnim;
    private Vector3 desiredPosition;
    private Rigidbody rb;
    public Vector3[] points;
    private int pointPosition;
    public bool random;

    private new void Start()
    {
        BeatInformer.GetInstance().RegisterReceiver(this);
        TempoStartInformer.GetInstance().RegisterReceiver(this);
        desiredPosition = new Vector3();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayAnimation();
    }

    public override void OnBeat()
    {
        if (random)
        {
            int tempPoint = Random.Range(0, points.Length);
            if (tempPoint == pointPosition)
            {
                tempPoint++;
            }
            pointPosition = tempPoint;
        }
        if (pointPosition >= points.Length)
        {
            pointPosition = 0;
        }
        desiredPosition = points[pointPosition++];
    }

    private void PlayAnimation()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, (float)secsPerAnim);
        //Vector3 currentVel = rb.velocity;
        //transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVel, (float)(secsPerAnim / 10));
    }

    public void ReceiveTempo(double secsPerBeat)
    {
        secsPerAnim = secsPerBeat;
    }

    public void StopTempo()
    {

    }
}
