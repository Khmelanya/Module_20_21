using UnityEngine;

public class DragInteraction : IContinuousInteraction
{
    private readonly MouseRaycaster _planeRaycaster;
    private readonly LayerMask _draggableMask;
    private readonly LayerMask _groundMask;

    private Rigidbody _rigidbody;
    private IGrabbable _grabbable;

    public DragInteraction(MouseRaycaster planeRaycaster, LayerMask draggableMask, LayerMask groundMask)
    {
        _planeRaycaster = planeRaycaster;
        _draggableMask = draggableMask;
        _groundMask = groundMask;
    }

    public void Begin() => TryPick();
    public void Tick() => Drag();
    public void End() => Drop();

    private void TryPick()
    {
        if (TryGetHit(out RaycastHit hit) == false)
            return;

        if (TryGetGrabbable(hit, out IGrabbable grabbable) == false)
            return;

        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody == null)
            return;

        SetSelection(rigidbody, grabbable);
    }

    private void Drag()
    {
        if (HasPicked())
            return;

        if (TryGetTargetPoint(out Vector3 targetPoint) == false)
            return;

        MoveTowards(targetPoint);
    }

    private void Drop()
    {
        if (HasPicked())
            return;

        ClearSelection();
    }

    private void SetSelection(Rigidbody rigidbody, IGrabbable grabbable)
    {
        _rigidbody = rigidbody;
        _grabbable = grabbable;

        _rigidbody.velocity = Vector3.zero;

        _rigidbody.freezeRotation = true;
    }

    private void MoveTowards(Vector3 targetPoint)
    {
        Vector3 toTarget = targetPoint - _rigidbody.position;
        Vector3 desiredVelocity = toTarget * _grabbable.FollowSpeed;

        _rigidbody.velocity = desiredVelocity;
    }

    private void ClearSelection()
    {
        _rigidbody.velocity = Vector3.zero;

        _rigidbody = null;
        _grabbable = null;
    }

    private bool HasPicked() => _rigidbody == null;

    private bool TryGetHit(out RaycastHit hit)
    {
        return _planeRaycaster.TryGetObject(_draggableMask, out hit);
    }

    private bool TryGetGrabbable(RaycastHit hit, out IGrabbable grabbable)
    {
        return hit.collider.TryGetComponent(out grabbable);
    }

    private bool TryGetTargetPoint(out Vector3 targetPoint)
    {
        return _planeRaycaster.TryGetPoint(_groundMask, out targetPoint);
    }
}
