using UnityEngine;

public readonly struct ExplosionContext
{
    public Vector3 Point { get; }
    public float Force { get; }
    public float Radius { get; }
    public float UpwardModifier { get; }

    public ExplosionContext(Vector3 point, float force, float radius, float upwardModifier)
    {
        Point = point;
        Force = force;
        Radius = radius;
        UpwardModifier = upwardModifier;
    }
}
