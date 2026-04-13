using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public static class MobileInput
{
    public static bool moveLeft;
    public static bool moveRight;
    public static bool jump;
}
public class PlayerMovement : MonoBehaviour
{
    public GameManager gm;
    public Rigidbody rb;

    public float runSpeed = 500f;
    public float strafeSpeed = 500f;

    public float deathHeight = -5f;
    public float jumpForce = 15f;

    public Slider flightSlider;

    protected bool strafeLeft = false;
    protected bool strafeRight = false;
    protected bool doJump = false;

    bool IsGrounded()
    {
        Vector3 origin = transform.position + Vector3.down * 0.5f;
        return Physics.Raycast(origin, Vector3.down, 0.3f);
    }

    [Header("Flight")]
    public float maxFlightTime = 3f;
    public float flightForce = 15f;
    public float flightRecovery = 2f;

    private float currentFlight;
    private bool isFlying;

    private bool isGrounded = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            gm.EndGame();
            Debug.Log("Ты не достоин!");
        }

        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Update()
{
    bool isMobile = PlatformManager.Instance != null && PlatformManager.Instance.IsMobile();

    // ДВИЖЕНИЕ
    if (isMobile)
    {
        strafeLeft = MobileInput.moveLeft;
        strafeRight = MobileInput.moveRight;
    }
    else
    {
        strafeLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        strafeRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }

    // ПРЫЖОК 
if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
{
    doJump = true;
}

    // СМЕРТЬ 
    if (transform.position.y < deathHeight)
    {
        gm.EndGame();
        Debug.Log("Ты не достоин!");
    }

    //  ВОССТАНОВЛЕНИЕ ПОЛЕТА 
    if (isGrounded && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
    {
        currentFlight += flightRecovery * Time.deltaTime;
        currentFlight = Mathf.Clamp(currentFlight, 0, maxFlightTime);
    }

    //  ПОЛЕТ 
    if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && currentFlight > 0)
    {
        isFlying = true;
    }
    else
    {
        isFlying = false;
    }

    flightSlider.value = currentFlight / maxFlightTime;
}

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + Vector3.forward * runSpeed * Time.deltaTime);

        if (strafeLeft)
        {
            rb.AddForce(-strafeSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (strafeRight)
        {
            rb.AddForce(strafeSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (doJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            transform.DORewind();
            transform.DOShakeScale(.5f, .5f, 3, 30);

            doJump = false;
        }

        if (isFlying && currentFlight > 0)
        {
            rb.AddForce(Vector3.up * flightForce, ForceMode.Acceleration);
            currentFlight -= Time.fixedDeltaTime;
        }
    }
}