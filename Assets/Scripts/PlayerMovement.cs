using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody rb;
    private float horizontal;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce = 7f;

    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);


       horizontal = Input.GetAxis("Horizontal");
       if (horizontal > 0)
       {
        transform.rotation = Quaternion.Euler(0, 90, 0);
       }
       else if (horizontal < 0)
       {
        transform.rotation = Quaternion.Euler(0, -90, 0);
       }

       if (Input.GetButtonDown("Jump") && isGrounded)
       {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
       }

    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(horizontal * speed, rb.linearVelocity.y, 0);
    }

}
