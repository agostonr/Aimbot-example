using UnityEngine;

public class Aimbot : MonoBehaviour
{
    private const string Tag = "Enemy";

    public Transform playerCamera;
    public float aimAssistRadius = 0.5f;
    public float timeToAim = 0.3f;

    public RotationChanges TrackTarget()
    {
        var target = SelectTarget();

        if (!target)
        {
            return RotationChanges.Empty;
        }

        var targetPos = target.position;
        var totalHorizontalRotationAngles = CalculateTotalRotationAngles(Vector3.up, targetPos);
        var totalVerticalRotationAngles = CalculateTotalRotationAngles(playerCamera.right, targetPos);

        var dx = CalculateDeltaRotationDegrees(totalVerticalRotationAngles, timeToAim, Time.deltaTime, targetPos);
        var dy = CalculateDeltaRotationDegrees(totalHorizontalRotationAngles, timeToAim, Time.deltaTime, targetPos);

        return new RotationChanges(pitchAdditionInDegrees: dx, turnAddition: dy * Vector3.up);
    }

    private Transform SelectTarget()
    {
        var direction = playerCamera.transform.forward;
        var startPoint = playerCamera.position;

        if (!Physics.SphereCast(startPoint, aimAssistRadius, direction, out var hit, 1000f))
        {
            return null;
        }

        return hit.collider.CompareTag(Tag) ? hit.collider.transform : null;
    }

    private float CalculateDeltaRotationDegrees(float totalRotation, float time, float deltaTime, Vector3 target)
    {
        var adjustedTimeToAim = time * aimAssistRadius;
        var distance = (target - playerCamera.transform.position).magnitude;
        var angularVelocity = Mathf.Atan2(1f, distance) * Mathf.Rad2Deg / adjustedTimeToAim;
        return Mathf.Min(angularVelocity * deltaTime, Mathf.Abs(totalRotation)) * Mathf.Sign(totalRotation);
    }

    private float CalculateTotalRotationAngles(Vector3 planeNormal, Vector3 target)
    {
        var camForwardProjected = Vector3.ProjectOnPlane(playerCamera.forward, planeNormal);
        var playerToTargetProjected = Vector3.ProjectOnPlane((target - playerCamera.position).normalized, planeNormal);
        return Vector3.SignedAngle(camForwardProjected, playerToTargetProjected, planeNormal);
    }
}