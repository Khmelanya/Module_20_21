using UnityEngine;

public class MouseRaycaster
{
    private readonly Camera _camera;

    public MouseRaycaster(Camera camera)
    {
        _camera = camera;
    }

    public bool TryGetObject(LayerMask mask, out RaycastHit hit)
    {
        if (_camera == null)
        {
            hit = default;
            return false;
        }

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, mask);
    }

    public bool TryGetPoint(LayerMask mask, out Vector3 point)
    {
        if (TryGetObject(mask, out RaycastHit hit))
        {
            point = hit.point;
            return true;
        }

        point = default;
        return false;
    }
}

