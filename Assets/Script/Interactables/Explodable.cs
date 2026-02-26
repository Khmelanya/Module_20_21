using UnityEngine;

public class Explodable : MonoBehaviour, IExplodable
{
    [SerializeField] private Rigidbody _rigidbody;

    public void Explode(ExplosionContext context)
    {
        if (_rigidbody == null)
            return;

        _rigidbody.AddExplosionForce(
            context.Force,
            context.Point,
            context.Radius,
            context.UpwardModifier,
            ForceMode.Impulse
        );
    }
}
