using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Item Data/Weapons", order = 0)]
public class Weapon : Item
{
    [Tooltip("The base damage of the weapon, without dampening calculations")]
    public float baseDamage;

    [Tooltip("Muzzle exit speed measured in meters/second. This is NOT a velocity vector.")]
    public float muzzleVelocity;

    [Tooltip("The fire rate of the weapon.")]
    public float fireRate;

    [Tooltip("The clip size of the weapon.")]
    public int clipSize;

    public AudioClip firingSound;
    public AudioClip reloadingSound;    
}
