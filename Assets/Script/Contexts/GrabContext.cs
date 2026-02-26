using UnityEngine;

public readonly struct GrabContext
{
    public readonly Rigidbody Rigidbody;

    public GrabContext(Rigidbody rigidbody)
    {
        Rigidbody = rigidbody;
    }
}