using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public Weapon weaponData;
    public GameObject weaponBullet;

    public Transform muzzleExit;

    private void OnEnable()
    {
        InputManager.Instance.MainControlsService.Player.Shoot.performed += OnShootCommand;
    }

    private void OnDisable()
    {
        InputManager.Instance.MainControlsService.Player.Shoot.performed -= OnShootCommand;
    }

    public void OnShootCommand(InputAction.CallbackContext ctx)
    {
        GameObject bulletInstance = GameObject.Instantiate(weaponBullet, muzzleExit.position, Quaternion.identity);
    }
}
