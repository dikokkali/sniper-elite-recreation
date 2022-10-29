using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBrain : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private ThirdPersonController _playerController;

    [ReadOnly]
    [BoxGroup("DEBUG")]
    [SerializeField]private Vector3 _velocity;

    private void Awake()
    {
        if (_playerAnimator == null)
            Debug.LogWarning("No animator is set for the player");
    }

    private void Update()
    {
        _velocity = _playerController.GetVelocity();

        _playerAnimator.SetFloat("playerSpeed", _velocity.magnitude / _playerController.runSpeed);
    }
}
