using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;

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
    }

    void Update()
    {
        if (agent != null && player != null && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
        }
    }
}