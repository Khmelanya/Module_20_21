using UnityEngine;

public class DragInteraction : IInteraction
{
    private readonly MouseRaycaster _planeRaycaster;
    private readonly LayerMask _draggableMask;
    private readonly LayerMask _groundMask;

    private Rigidbody _rigidbody;
    private Draggable _draggable;

    private bool _prevFreezeRotation;

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

        if (TryGetDraggable(hit, out Draggable draggable) == false)
            return;

        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody == null)
            return;

        SetSelection(rigidbody, draggable);
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

    private void SetSelection(Rigidbody rigidbody, Draggable draggable)
    {
        _rigidbody = rigidbody;
        _draggable = draggable;

        _rigidbody.velocity = Vector3.zero;

        _prevFreezeRotation = _rigidbody.freezeRotation;
        _rigidbody.freezeRotation = true;
    }

    private void MoveTowards(Vector3 targetPoint)
    {
        Vector3 toTarget = targetPoint - _rigidbody.position;
        Vector3 desiredVelocity = toTarget * _draggable.followSpeed;

        _rigidbody.velocity = desiredVelocity;
    }

    private void ClearSelection()
    {
        _rigidbody.velocity = Vector3.zero;

        _rigidbody = null;
        _draggable = null;
    }

    private bool HasPicked() => _rigidbody == null;

    private bool TryGetHit(out RaycastHit hit)
    {
        return _planeRaycaster.TryGetObject(_draggableMask, out hit);
    }

    private bool TryGetDraggable(RaycastHit hit, out Draggable draggable)
    {
        return hit.collider.TryGetComponent(out draggable);
    }

    private bool TryGetTargetPoint(out Vector3 targetPoint)
    {
        return _planeRaycaster.TryGetPoint(_groundMask, out targetPoint);
    }
}
