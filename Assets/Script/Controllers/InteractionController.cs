using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Camera _rayCamera;

    [SerializeField] private LayerMask _groundMask;     
    [SerializeField] private LayerMask _draggableMask;

    [SerializeField] private ParticleSystem _effectPrefab;

    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _force = 50f;
    [SerializeField] private float _upwardModifier = 1f;

    private const int LeftMouseButton = 0;
    private const int RightMouseButton = 1;

    private IContinuousInteraction _drag;
    private IBeginInteraction _explosion;

    private MouseRaycaster _raycaster;

    private void Awake()
    {
        if (_rayCamera == null)
            _rayCamera = Camera.main;

        _raycaster = new MouseRaycaster(_rayCamera);

        _drag = new DragInteraction(_raycaster, _draggableMask, _groundMask);
        _explosion = new ExplosionInteraction(_raycaster, _radius, _force, _upwardModifier, _groundMask);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
            _drag.Begin();

        if (Input.GetMouseButton(LeftMouseButton)) 
            _drag.Tick();

        if (Input.GetMouseButtonUp(LeftMouseButton))
            _drag.End();

        if (Input.GetMouseButtonDown(RightMouseButton))
        {
            _explosion.Begin();
            PlayEffect();
        }   
    }

    private void PlayEffect()
    {
        if (_raycaster.TryGetPoint(_groundMask, out Vector3 point))
        {
            Instantiate(_effectPrefab, point, Quaternion.identity);
        }
    }
}

