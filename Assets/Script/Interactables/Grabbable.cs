using UnityEngine;

public class Grabbable : MonoBehaviour, IGrabbable
{
    [SerializeField] private float _followSpeed = 25f;
    [SerializeField] private bool _freezeRotationOnDrag = true;

    private bool _prevFreezeRotation;

    public float FollowSpeed => _followSpeed;

    public void OnGrab(GrabContext context)
    {
        Rigidbody rigidbody = context.Rigidbody;

        Stop(rigidbody);

        _prevFreezeRotation = rigidbody.freezeRotation;

        if (_freezeRotationOnDrag)
            rigidbody.freezeRotation = true;
    }

    public void OnRelease(GrabContext context)
    {
        Rigidbody rigidbody = context.Rigidbody;

        Stop(rigidbody);

        rigidbody.freezeRotation = _prevFreezeRotation;
    }

    private void Stop(Rigidbody rigidbody)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
