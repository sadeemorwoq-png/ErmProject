//كود بعد محادثه ابو فانوس مع جود يصير يمشي مع جود
using UnityEngine;
using UnityEngine.AI;

public class AbuFanousFollow : MonoBehaviour
{
    public Transform player;
    public float followDistance = 2f;

    private NavMeshAgent agent;
    private bool canFollow = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!canFollow)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > followDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    public void StartFollowing()
    {
        canFollow = true;
    }
}