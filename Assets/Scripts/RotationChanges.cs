using UnityEngine;

public struct RotationChanges
{
    public Vector3 TurnAddition { get; }
        
    public float PitchAdditionInDegrees { get; }

    public RotationChanges(Vector3 turnAddition, float pitchAdditionInDegrees)
    {
        TurnAddition = turnAddition;
        PitchAdditionInDegrees = pitchAdditionInDegrees;
    }

    /// <summary>
    /// Returns an empty result. You can add this to your rotations as if they were actual populated values and they'll make no difference.
    /// </summary>
    public static RotationChanges Empty => new RotationChanges();
}