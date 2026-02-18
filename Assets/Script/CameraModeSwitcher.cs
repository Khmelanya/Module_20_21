using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CameraModeSwitcher : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> _camera;

    private Queue<CinemachineVirtualCamera> _cameraQueue;

    private void Awake()
    {
        _cameraQueue = new Queue<CinemachineVirtualCamera>(_camera);
        SwitchNextMode();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            SwitchNextMode();
    }

    private void SwitchNextMode()
    {
        CinemachineVirtualCamera nextMode = _cameraQueue.Dequeue();

        foreach (var item in _camera)
        {
            item.gameObject.SetActive(false);
        }

        nextMode.gameObject.SetActive(true);

        _cameraQueue.Enqueue(nextMode);
    }
}
