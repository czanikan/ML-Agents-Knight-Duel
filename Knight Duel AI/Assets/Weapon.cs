using UnityEngine;
using Unity.MLAgents;

public class Weapon : MonoBehaviour
{
    public int damage;

    public Agent ownerAgent;

    private void Awake()
    {
        ownerAgent = GetComponentInParent<AgentKnight>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Knight")
        {
            collision.gameObject.GetComponent<AgentKnight>().TakeDamage(damage);

            Debug.Log(ownerAgent.gameObject.name + " hitted " + collision.gameObject.name);
            Debug.Log(collision.gameObject.name + "'s current HP: " + collision.gameObject.GetComponent<AgentKnight>().currHP);

            ownerAgent.AddReward(1f);
        }
    }
}
