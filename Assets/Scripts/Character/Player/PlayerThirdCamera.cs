using Cinemachine;
using UnityEngine;

public class PlayerThirdCamera : MonoBehaviour
{
    private PlayerInputManager inputManager;
    private PlayerController playerController;
    private PlayerAxisInput axisInput;

    [SerializeField] private Transform LookAt;

    [SerializeField] private float cameraOffsetY;
    [SerializeField] private float cameraOffsetZ;

    [SerializeField] LayerMask layerMask;

    /// <summary>The Vertical axis.  Value is -90..90. Controls the vertical orientation</summary>
    [Tooltip("The Vertical axis.  Value is -90..90. Controls the vertical orientation")]
    [AxisStateProperty]
    public AxisState m_VerticalAxis = new AxisState(-70, 70, false, false, 300f, 0.1f, 0.1f, "Mouse Y", true);

    /// <summary>The Horizontal axis.  Value is -180..180.  Controls the horizontal orientation</summary>
    [Tooltip("The Horizontal axis.  Value is -180..180.  Controls the horizontal orientation")]
    [AxisStateProperty]
    public AxisState m_HorizontalAxis = new AxisState(-180, 180, true, false, 300f, 0.1f, 0.1f, "Mouse X", false);

    private void Awake()
    {
        inputManager = Player.Instance.InputManager;
        playerController = Player.Instance.PlayerController;
        axisInput = new(inputManager);
    }

    private void Start()
    {
        transform.forward = (transform.position - LookAt.position).normalized;
    }

    private void OnEnable()
    {
        m_VerticalAxis.SetInputAxisProvider(0, axisInput);
        m_HorizontalAxis.SetInputAxisProvider(1, axisInput);

        playerController.OnLook += UpdateCamera;
    }

    private void OnDisable()
    {
        m_VerticalAxis.SetInputAxisProvider(0, null);
        m_HorizontalAxis.SetInputAxisProvider(1, null);

        m_VerticalAxis.Reset();
        m_HorizontalAxis.Reset();

        playerController.OnLook -= UpdateCamera;
    }

    private void UpdateCamera()
    {
        SetCameraRotation();
        SetCameraPosition();
    }

    private void SetCameraRotation()
    {
        m_VerticalAxis.Update(Time.deltaTime);
        m_HorizontalAxis.Update(Time.deltaTime);

        transform.rotation = Quaternion.Euler(m_VerticalAxis.Value, m_HorizontalAxis.Value, 0);
    }

    private void SetCameraPosition()
    {
        var cameraPos = LookAt.position + transform.TransformDirection(new Vector3(0, cameraOffsetY, cameraOffsetZ));

        Ray cameraRay = new(LookAt.position, cameraPos);
        if (Physics.Raycast(cameraRay, out var hit, Vector3.Distance(LookAt.position, cameraPos), layerMask))
        {
            cameraPos = hit.point;
        }

        transform.position = cameraPos;
    }

    private class PlayerAxisInput : AxisState.IInputAxisProvider
    {
        private readonly PlayerInputManager inputManager;

        public PlayerAxisInput(PlayerInputManager inputManager)
        {
            this.inputManager = inputManager;
        }

        public float GetAxisValue(int axis)
        {
            if (axis == 0) return inputManager.PlayerAxis.y;
            else return inputManager.PlayerAxis.x;
        }
    }
}
