using UnityEngine;

/// <summary>
/// Unused
/// To Control a GameObject using joystick
/// </summary>
public class JoystickControl : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float drag = 0.5f;
    public float terminalRotationSpeed = 25.0f;
    public Vector3 MoveVector { set; get; }
    public Joystick joystick;
    private Rigidbody thisRigidbody;

    // Use this for initialization
    void Start()
    {
        
        thisRigidbody = gameObject.AddComponent<Rigidbody>();
        print(gameObject.name);
        thisRigidbody.maxAngularVelocity = terminalRotationSpeed;
        thisRigidbody.drag = drag;
    }

    // Update is called once per frame
    void Update()
    {
        MoveVector = PoolInput();

        Move();
    }

    /// <summary>
    /// Move a Game Object using his rigidbody
    /// </summary>
    private void Move()
    {
        thisRigidbody.AddForce((MoveVector * moveSpeed));
    }

    private Vector3 PoolInput()
    {
        Vector3 dir = Vector3.zero;
        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();

        if (dir.magnitude > 1)
        {
            dir.Normalize();
        }
        return dir;
    }
}
