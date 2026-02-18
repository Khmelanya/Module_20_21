using UnityEngine;

public class ExplosionInteraction : IInteraction
{
    private MouseRaycaster _planeRaycaster;

    private readonly float _radius;
    private readonly float _force;
    private readonly float _upwardModifier;

    private readonly LayerMask _groundMask;

    public ExplosionInteraction(MouseRaycaster planeRaycaster, float radius, float force, float upwardModifier, LayerMask groundMask)
    {
        _planeRaycaster = planeRaycaster;
        _radius = radius;
        _force = force;
        _upwardModifier = upwardModifier;
        _groundMask = groundMask;
    }

    public void Begin() => ExplodeAtMouse();
    public void Tick() { }
    public void End() { }

    private void ExplodeAtMouse()
    {
        bool hasPoint = _planeRaycaster.TryGetPoint(_groundMask, out Vector3 point);

        if (hasPoint == false)
            return;

        ApplyExplosionForce(point);
    }
    private void ApplyExplosionForce(Vector3 point)
    {
        Collider[] hits = Physics.OverlapSphere(point, _radius);

        foreach (var hit in hits)
        {
            Rigidbody rigidbody = hit.attachedRigidbody;
            if (rigidbody == null) continue;

           rigidbody.AddExplosionForce(_force, point, _radius, _upwardModifier, ForceMode.Impulse);
        }
    }
}



