using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody rb;
    private float horizontal;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce = 7f;

    [SerializeField] private float crouchSpeed = 2.5f;
    private bool isCrouching;

    private Vector3 originalScale;
    [SerializeField] private Vector3 crouchScale = new Vector3(1f, 0.5f, 1f);

    private bool isGrounded;

    [SerializeField] private GameObject torch;
    private bool torchVisible = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        originalScale = transform.localScale;
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

       if (Input.GetKey(KeyCode.LeftControl))
       {
           isCrouching = true;
           transform.localScale = crouchScale;
       }
       else
       {
        isCrouching = false;
        transform.localScale = originalScale;
       }

      if (Input.GetKeyDown(KeyCode.F))
      {
        torchVisible = !torchVisible;
        torch.SetActive(torchVisible);
      }

    }

    void FixedUpdate()
    {
        float currentSpeed = isCrouching ? crouchSpeed : speed;
        rb.linearVelocity = new Vector3(horizontal * currentSpeed, rb.linearVelocity.y, 0);
    }

}
