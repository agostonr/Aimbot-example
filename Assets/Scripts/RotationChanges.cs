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

    public static RotationChanges Empty => new RotationChanges();
}