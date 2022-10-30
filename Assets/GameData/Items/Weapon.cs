using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Item Data/Weapons", order = 0)]
public class Weapon : Item
{
    [Tooltip("The base damage of the weapon, without dampening calculations")]
    [SerializeField] private float _baseDamage;

    [Tooltip("Muzzle exit speed measured in meters/second. This is NOT a velocity vector.")]
    [SerializeField] private float _muzzleVelocity;

    [Tooltip("The fire rate of the weapon.")]
    [SerializeField] private float _fireRate;

    [Tooltip("The clip size of the weapon.")]
    [SerializeField] private int _clipSize;

    
    public float BaseDamage { get; set; }
    public float MuzzleVelocity { get; set; }
    public float FireRate { get; set; }
    public int ClipSize { get; set; }

    public AudioClip firingSound;
    public AudioClip reloadingSound;    
}
