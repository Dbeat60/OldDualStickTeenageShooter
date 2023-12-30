using UnityEngine;

[CreateAssetMenu(fileName = "Shake Transform Event", menuName = "Custom/Shake Transform Event 2", order = 1)]
public class CameraShakeData : ScriptableObject
{

    // Public members

    // Shake based on pos o rot
    public enum Target
    {
        Position,
        Rotation
    }
    public Target target = Target.Position;

    // Range of motion / multiple of noise sample
    public float amplitude = 1.0f;
    // Speed of motion
    public float frequency = 1.0f;
    // Duration duh
    public float duration = 1.0f;

    // Animation atenuation / multiple of nois sample
    public AnimationCurve blendOverLifetime = new AnimationCurve(
        new Keyframe(0.0f, 0.0f, Mathf.Deg2Rad * 0.0f, Mathf.Deg2Rad * 720f),
        new Keyframe(0.20f, 1.0f),
        new Keyframe(1.0f, 0.0f));

    // Initialization

    public void Init(float amplitude, float frequency, float duration, AnimationCurve blendOverLifetime, Target target)
    {
        this.amplitude = amplitude;
        this.frequency = frequency;
        this.duration = duration;
        this.blendOverLifetime = blendOverLifetime;
        this.target = target;
    }

}
