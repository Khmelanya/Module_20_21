using UnityEngine;

public class ExplosionInteraction : IBeginInteraction
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

    private void ExplodeAtMouse()
    {
        bool hasPoint = _planeRaycaster.TryGetPoint(_groundMask, out Vector3 point);

        if (hasPoint == false)
            return;

        ExplosionContext context = new ExplosionContext(point, _force, _radius, _upwardModifier);

        ApplyExplosionForce(point, context);
    }
    private void ApplyExplosionForce(Vector3 point, ExplosionContext context)
    {
        Collider[] hits = Physics.OverlapSphere(point, _radius);

        foreach (var hit in hits)
        {
            bool hasExplodable = hit.TryGetComponent(out IExplodable explodable);

            if (hasExplodable == false)
                continue;

            explodable.Explode(context);
        }
    }
}



