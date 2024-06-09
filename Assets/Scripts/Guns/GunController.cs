using UnityEngine;
using System;
using System.Collections;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    private Camera _mainCamera;

    [Header("Gun Settings")]
    [SerializeField] private GunData _gunData;
    [SerializeField] private float _gunMaxDistance = 100f;
    [SerializeField] private int _gunAmmo;
    private Coroutine _currentCoroutine;

    public GunData GunData { get => _gunData; set => _gunData = value; }

    public event Action<Vector3, Vector3> OnGunShoot = delegate { };
    public event Action<int> OnAmmoUpdate = delegate { };
    public event Action OnReload = delegate { };

    private void Awake()
    {
        if (!_gunData)
        {
            Debug.Log($"{name}: Gun Data is null!\nDisabling to avoid errors!");
            enabled = false;
            return;
        }

        _mainCamera = Camera.main;
        _gunAmmo = _gunData.maxAmmo;
    }

    public void Shoot()
    {
        if (_gunAmmo <= 0)
        {
            Debug.Log($"{name}: No ammo left!");
            return;
        }

        if (_gunData.isProyectile)
        {
            // TODO: Proyectile logic here
        }
        
        if (!_gunData.isProyectile)
        {
            Ray bullet = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(bullet, out RaycastHit hitInfo, _gunMaxDistance))
            {
                Debug.Log($"{name}: Shooted to {hitInfo.collider.name}");
            }
            else
            {

            }
            
            OnGunShoot?.Invoke(bullet.origin, hitInfo.point);
        }
    }

    public void Reload()
    {
        if (_currentCoroutine != null)
            return;

        Debug.Log($"{name} is reloading...");
        _currentCoroutine = StartCoroutine(ReloadSequence());
    }

    private IEnumerator ReloadSequence()
    {
        OnReload?.Invoke();
        yield return new WaitForSeconds(2f);

        _gunAmmo = _gunData.maxAmmo;
        _currentCoroutine = null;
    }
}
