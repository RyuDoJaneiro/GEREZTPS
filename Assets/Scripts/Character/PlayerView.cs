using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GunController))]
[RequireComponent(typeof(ThirdPersonShooterController))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _gunHandle;
    [SerializeField] private TrailRenderer _trailRenderer;
    private Transform _gunCanonTransform;
    private ThirdPersonShooterController _shooterController;
    private Animator _animator;
    private GunController _gunController;

    private void Awake()
    {
        _shooterController = GetComponent<ThirdPersonShooterController>();
        _gunController = GetComponent<GunController>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _shooterController.OnAim += ChangeAnimations;
        _gunController.OnGunShoot += RenderBullet;
    }

    private void OnDisable()
    {
        _shooterController.OnAim -= ChangeAnimations;
        _gunController.OnGunShoot -= RenderBullet;
    }

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        GameObject createdGun = Instantiate(_gunController.GunData.gunPrefab, _gunHandle);
        createdGun.transform.parent = _gunHandle;
    }

    private void RenderBullet(Vector3 origin, Vector3 hitPosition)
    {
        _gunCanonTransform = _gunHandle.transform.GetChild(0).Find("GunCanon");
        TrailRenderer trail = Instantiate(_trailRenderer, _gunCanonTransform.position, Quaternion.identity);
        StartCoroutine(SpawnTrail(trail, hitPosition));
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hit)
    {
        float timer = 0f;
        Vector3 startPosition = trail.transform.position;

        while (timer < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit, timer);
            timer += Time.deltaTime / trail.time;

            yield return null;
        }
        trail.transform.position = hit;

        Destroy(trail.gameObject, trail.time);
    }

    private void ChangeAnimations(bool aimState)
    {
        if (aimState)
        {
            _animator.SetBool("Aim", true);
        }
        else
        {
            _animator.SetBool("Aim", false);
            
        }
    }
}
