using StarterAssets;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(StarterAssetsInputs))]
[RequireComponent(typeof(GunController))]
public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private GameObject _aimCinemachine;
    private StarterAssetsInputs _input;
    private GunController _gunController;
    private Coroutine _currentCoroutine;

    public event Action<bool> OnAim = delegate { };   

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _gunController = GetComponent<GunController>();
    }

    private void Update()
    {
        Aim();
        HandleShootInput();
    }

    private void HandleShootInput()
    {
        if (_input.IsShooting)
        {
            if (_currentCoroutine != null)
                return;

            if (_gunController.GunData.isAutomatic)
                _currentCoroutine = StartCoroutine(HandleAutomaticShoot());
            else
                Debug.Log("Shoot");
        }
    }

    private IEnumerator HandleAutomaticShoot()
    {
        GunData gunData = _gunController.GunData;

        while (_input.IsShooting)
        {
            _gunController.Shoot();
            yield return new WaitForSeconds(gunData.timeBetweenShoots);
        }

        _currentCoroutine = null;
    }

    private void Aim()
    {
        if (_input.IsAiming)
        {
            OnAim?.Invoke(true);
            _aimCinemachine.SetActive(true);
        }
        else
        {
            OnAim?.Invoke(false);
            _aimCinemachine.SetActive(false);
        }
    }
}
