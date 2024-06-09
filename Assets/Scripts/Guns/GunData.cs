using UnityEngine;

[CreateAssetMenu(menuName = "Data/Gun", fileName = "New Gun")]
public class GunData : ScriptableObject
{
    [Header("Gun Info")]
    public string gunName = "Gun";
    public GameObject gunPrefab;
    public bool isAutomatic = false;
    public bool isProyectile = false;
    public AnimationClip idleAnim;
    public AnimationClip aimAnim;

    [Header("Gun Stats")]
    public Vector3 bulletSpread;
    public int bulletsPerShoot = 1;
    public int damagePerBullet = 1;
    public float timeBetweenShoots = 1f;
    public int maxAmmo = 1;
}