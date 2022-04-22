using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform goal;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 4f;
        agent.destination = Managers.Player.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = Managers.Player.player.transform.position;
    }
}
