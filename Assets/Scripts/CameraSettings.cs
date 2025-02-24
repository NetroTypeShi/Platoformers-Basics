using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -1));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
    // Update is called once per frame
}
