using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    public float smoothTurnFactor;
    public float groundedDistance;

    public bool drawDebug;
    public bool getRunStateFromVelocity;

    private Vector2 _movementData;
    private Vector2 _lookData;

    private float _moveSpeed;

    [ReadOnly][BoxGroup("DEBUG")]
    [SerializeField] private bool isJumping;

    [ReadOnly][BoxGroup("DEBUG")]
    [SerializeField] private bool isInAir;

    [ReadOnly][BoxGroup("DEBUG")]
    [SerializeField] private bool isGrounded;

    [ReadOnly][BoxGroup("DEBUG")]
    [SerializeField] private bool isRunning;

    [ReadOnly][BoxGroup("DEBUG")]
    [SerializeField] private Vector3 velocity;

    private CharacterController _characterController;
    [SerializeField] private Camera _playerCamera;

    private void Awake()
    {
        if (_characterController == null)
            _characterController = gameObject.GetComponent<CharacterController>();

        _moveSpeed = walkSpeed;       

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        velocity = _characterController.velocity;
        DrawDebug();

        GetMovementInputData();
        GetLookInputData();

        HandleGravity();
        HandleMovement();
    }

    private void OnEnable()
    {
        InputManager.Instance.MainControlsService.Player.Run.performed += OnRunButtonDown;
        InputManager.Instance.MainControlsService.Player.Run.canceled += OnRunButtonUp;

        InputManager.Instance.MainControlsService.Player.Jump.performed += OnJumpButtonDown;
        InputManager.Instance.MainControlsService.Player.Jump.canceled += OnJumpButtonUp;
    }

    private void OnDisable()
    {
        InputManager.Instance.MainControlsService.Player.Run.performed -= OnRunButtonDown;
        InputManager.Instance.MainControlsService.Player.Run.canceled -= OnRunButtonUp;

        InputManager.Instance.MainControlsService.Player.Jump.performed -= OnJumpButtonDown;
        InputManager.Instance.MainControlsService.Player.Jump.canceled -= OnJumpButtonUp;
    }

    #region Input Handling Methods
    private void HandleMovement()
    {
        Vector3 forwardCameraVector = Vector3.ProjectOnPlane(_playerCamera.transform.forward, Vector3.up).normalized;
        Vector3 rightCameraVector = Vector3.ProjectOnPlane(_playerCamera.transform.right, Vector3.up).normalized;

        if (_movementData.x != 0 || _movementData.y != 0)
        {
            Vector3 motionVector = rightCameraVector * _movementData.x + forwardCameraVector * _movementData.y;

            motionVector = motionVector.normalized * _moveSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(motionVector.normalized), smoothTurnFactor);
            _characterController.Move(motionVector.normalized * _moveSpeed * Time.deltaTime);
        }
        else _characterController.Move(Vector3.zero);
    }

    private void HandleGravity()
    {
        if (!IsGrounded())
        {            
            float gravityDisplacement = -Physics.gravity.magnitude * Time.deltaTime;
            Vector3 uprightVector = new Vector3(0f, gravityDisplacement, 0f);

            _characterController.Move(uprightVector);
        }        
    }
    #endregion

    #region Utility Methods
    private void DrawDebug()
    {
        if (!drawDebug) return;

        Debug.DrawRay(transform.position, -transform.up * groundedDistance, Color.magenta);
    }

    private bool IsGrounded()
    {
        Ray groundedRay = new Ray(transform.position, -transform.up);
        RaycastHit hitInfo;

        if (Physics.Raycast(groundedRay, out hitInfo, groundedDistance))
        {
            Debug.Log(hitInfo.collider.name);
            isGrounded = true;
            return true;
        }
        else 
        { 
            isGrounded = false;
            return false;
        }
    }

    public bool IsRunning()
    {
        if (getRunStateFromVelocity)
        {
            if (_characterController.velocity.magnitude >= runSpeed)
                return true;
            
            else return false;
        }
        else
        {
            if (isRunning) return true;

            else return false;
        }
    }

    public Vector3 GetVelocity()
    {
        return _characterController.velocity;
    }

    #endregion

    // TODO: Put in an input handler
    #region Input Reading Methods
    private void GetMovementInputData()
    {
        _movementData = InputManager.Instance.MainControlsService.Player.Move.ReadValue<Vector2>();
    }

    private void GetLookInputData()
    {
        _lookData = InputManager.Instance.MainControlsService.Player.Look.ReadValue<Vector2>() * 0.5f * 0.1f;
    }
    #endregion

    #region Input Event Callbacks
    private void OnRunButtonDown(InputAction.CallbackContext ctx)
    {
        _moveSpeed = runSpeed;
    }

    private void OnRunButtonUp(InputAction.CallbackContext ctx)
    {
        _moveSpeed = walkSpeed;
    }

    private void OnJumpButtonDown(InputAction.CallbackContext ctx)
    {
        isJumping = true;
    }

    private void OnJumpButtonUp(InputAction.CallbackContext ctx)
    {
        isJumping = false;
    }
    #endregion
}
