using UnityEngine;
using UnityEngine.InputSystem;

//////////////////////////////////////////////  
//                                          //
//       This script shall live on:         //
//              The Player !                //
//                                          //
//////////////////////////////////////////////

// script's purpose: let the player control the character !

// script's requirements: Set up an Input Map/ thingie using new input :)

// important to note: i still have to fix a bug in it :( 

public class PlayerMoveIt : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;                     // from the inspector assign the animator for the character here ! :3
    private CharacterController characterController;                // this finds the character controller on the player body ! new input stuff :D

    [Header("Constrained Movement (default)")]
    [SerializeField] private float twoDSonic = 4f;                  // this is the speed for left / right :3
    [SerializeField] private float threeDSonic = 1.5f;              // this is the speed for clsose / far
    [SerializeField] private float maxDepthOffset = 1.2f;           // this is how far the player can stray toward/away from the screen ! :D
    [SerializeField] private Vector3 twoDAxis = Vector3.right;
    [SerializeField] private Vector3 threeDAxis = Vector3.forward;

    [Header("Open Movement")]   // this is triggered based on what the camera trigger the player is in right now :O
    [SerializeField] private float freeThreeDSonic = 5f;            // this is the speed for open world movement ! :D
    private bool areWeFreeYet = false;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Gravity")]
    [SerializeField] private float gravityMultiplier = 2f;

    private Vector2 moveInput;
    private Vector3 verticalVelocity;
    private float depthOrigin;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (animator == null) animator = GetComponent<Animator>();

        depthOrigin = Vector3.Dot(transform.position, threeDAxis.normalized);
    }

    private void Update()
    {
        Vector3 moveDir = areWeFreeYet ? NeverMindItsThreeD() : OnlyTwoDInThisHouse();

        GravityFalls();
        MoveCharacter(moveDir);
        HeyLookWhereYoureGoing(moveDir);
        AnimationTime(moveDir);
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    // this is for the camera ! so when we want movement to go to fully 3D :3
    public void SetOpenMovement(bool isOpen) => areWeFreeYet = isOpen;

    private Vector3 OnlyTwoDInThisHouse()
    {
        // this is to allow the player some room to walk close / far from cam juusst a little bit
        // i think it's a lot nicer than just FULL 2D :3

        Vector3 lateral = twoDAxis.normalized * (moveInput.x * twoDSonic);
        Vector3 depth = threeDAxis.normalized * (moveInput.y * threeDSonic);
        float currentDepth = Vector3.Dot(transform.position, threeDAxis.normalized) - depthOrigin;
        float projectedDepth = currentDepth + Vector3.Dot(depth * Time.deltaTime, threeDAxis.normalized);

        if (projectedDepth > maxDepthOffset || projectedDepth < -maxDepthOffset)
        {
            depth = Vector3.zero;
        }

        return lateral + depth;
    }

    private Vector3 NeverMindItsThreeD()
    {
        // this controls if the player can have open movement or just 2D
        Transform cam = Camera.main.transform;
        Vector3 forward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;
        return (forward * moveInput.y + right * moveInput.x) * freeThreeDSonic;
    }

    private void MoveCharacter(Vector3 moveDir)
    {
        // i like to move it move it
        Vector3 motion = moveDir;
        motion.y = verticalVelocity.y;
        characterController.Move(motion * Time.deltaTime);
    }

    private void GravityFalls()
    {  
        // need this so i can add jump 
        // havent done it cuz im still learning new input lol
        if (characterController.isGrounded && verticalVelocity.y < 0)
            verticalVelocity.y = -2f;
        else
            verticalVelocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
    }

    private void HeyLookWhereYoureGoing(Vector3 moveDir)
    {
        // this is to make the player face the direction they are walking to :)
        Vector3 flatDir = new Vector3(moveDir.x, 0f, moveDir.z);
        if (flatDir.sqrMagnitude < 0.0001f) return;                 // if theres no input, keep facing the last place player looked :)

        Quaternion targetRotation = Quaternion.LookRotation(flatDir.normalized, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void AnimationTime(Vector3 moveDir)
    {
        // this is for my animtor ! :))
        // in the animator this upadtes the paramater "Speed" to check if the player moved or not
        if (animator == null) return;
        Vector3 flatDir = new Vector3(moveDir.x, 0f, moveDir.z);
        float speedNormalized = areWeFreeYet
            ? Mathf.Clamp01(flatDir.magnitude / freeThreeDSonic)
            : moveInput.magnitude;

        animator.SetFloat("Speed", speedNormalized);
    }
}
