using UnityEngine;

public class HeadBobbingCamera : MonoBehaviour
{
[Range(0.001f, 0.01f)]
public float _amount = 0.002f;
[Range(1f, 30f)]
public float _frequency = 10.0f;
[Range(1f, 20f)]
public float _smooth = 10.0f;

private Vector3 startingPosition;

void Start()
{
    startingPosition = transform.localPosition;
}

void Update()
{
    float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

    if (inputMagnitude > 0)
    {
        StartHeadBob();
    }
    else
    {
        StopHeadBob();
    }
}

private void StopHeadBob()
{
    transform.localPosition = Vector3.Lerp(transform.localPosition, startingPosition, _smooth * Time.deltaTime);
}

private Vector3 StartHeadBob()
{
    Vector3 targetPosition = startingPosition;
    targetPosition.y += Mathf.Sin(Time.time * _frequency) * _amount * 1.4f;
    targetPosition.x += Mathf.Cos(Time.time * _frequency / 2) * _amount * 1.6f;

    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, _smooth * Time.deltaTime);
    return targetPosition;
}
}
