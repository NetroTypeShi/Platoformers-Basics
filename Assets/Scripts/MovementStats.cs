using UnityEngine;
[CreateAssetMenu]
public class MovementStats : ScriptableObject
{
    [SerializeField] public float defaultGravity;
    [SerializeField] public float fallingGravity;
    [SerializeField] public float peakGravity;
    [SerializeField] public float groundFriction;
    [SerializeField] public float acceleration;
    [SerializeField] public float groundAcceleration;
    [SerializeField] public float maxGroundHorizontalSpeed = 10;
    [SerializeField] public float jumpStrength;
    [SerializeField] public int defaultJumpsNumber;
    [SerializeField] public int onAirJumps;
    [SerializeField] public float maxJumpTime = 1.3f;
}
