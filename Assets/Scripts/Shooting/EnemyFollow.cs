using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on this GameObject!");
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player transform not assigned!");
            return;
        }

        // Check if the agent is on a NavMesh
        if (!agent.isOnNavMesh)
        {
            Debug.LogError("Enemy is not placed on a NavMesh!");
            return;
        }

        animator = GetComponent<Animator>();
        
        // Check if the Animator component exists
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (agent != null && player != null && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
            Vector3 direction = agent.velocity.normalized;
            transform.LookAt(transform.position + direction);
        }
    }
}