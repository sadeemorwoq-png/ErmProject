using UnityEngine;
using Unity.Cinemachine;

public class CameraObstacle : MonoBehaviour
{

    [SerializeField] private Transform player;
    private Camera cam;
    private CinemachineFollow follow;

    [SerializeField] private LayerMask obstacleLayer;

    [SerializeField] private float raisedHeight = 2.5f;
    [SerializeField] private float normalHeight = 0.89f;

    [SerializeField] private float smoothSpeed = 5f;

    [SerializeField] private LayerMask GroundLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        follow = GetComponent<CinemachineFollow>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        Vector3 direction = transform.position - player.position;
        float distance = direction.magnitude;

        Vector3 offset = follow.FollowOffset;
        if (Physics.Raycast(player.position, direction.normalized, out RaycastHit groundHit, distance, GroundLayer))
        {
            offset.y = Mathf.Lerp(offset.y, raisedHeight, Time.deltaTime * smoothSpeed);
        }
        else
        {
            offset.y = Mathf.Lerp(offset.y, normalHeight, Time.deltaTime * smoothSpeed);
        }

        if (Physics.Raycast(player.position, direction.normalized, out RaycastHit obstacleHit, distance, obstacleLayer))
        {
            float hitDistance = obstacleHit.distance;
            float targetZ = -(hitDistance - 0.5f);
            targetZ = Mathf.Clamp(targetZ, -9.6f, 06.5f);

            offset.z = Mathf.Lerp(offset.z, targetZ, Time.deltaTime * smoothSpeed);
        }
        else
        {
            offset.z = Mathf.Lerp(offset.z, -9.6f, Time.deltaTime * smoothSpeed);
        }

        follow.FollowOffset = offset;
        

        
        }
    }
