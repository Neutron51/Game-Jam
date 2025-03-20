using UnityEngine;
using UnityEngine.AI;

public class Enemy : Health
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject player;
    [SerializeField] public int damage;
    [SerializeField] public float damageInterval = 1f;
    public float speed = 3.5f;
    [SerializeField] int xpDrop;
    public bool isClone;
    private float nextAttackTime;
    private bool damageRange;
    private Animator enemyAnimator;
    private Collider collider;
    private bool alive = true;
    void Start()
    {
        damageRange = false;
        enemyAnimator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        agent.speed = speed;
    }
    void Update()
    {


    }
    void FixedUpdate()
    {
        if (alive)
            agent.SetDestination(player.transform.position);
    }
    public override void TakeDamage(int amount)
    {
        if (alive)
        {
            health = Mathf.Clamp(health - amount, 0, maxHealth);
            agent.speed = 0f;
            if (health == 0)
            {
                alive = false;
                Debug.Log("dead");
                // collider.enabled = false;
                Destroy(collider);
                //Destroy(GetComponent<Rigidbody>());
                agent.ResetPath();
                enemyAnimator.SetTrigger("Death");
            }
            else
            {
                enemyAnimator.SetTrigger("TakeHit");
            }
            Debug.Log("ow");
        }
    }
    public void StaggerDone()
    {
        agent.speed = speed;
    }
    public override void Death()
    {
        player.GetComponent<PlayerContrller>().GetXP(xpDrop);

        Destroy(gameObject);
        gameObject.SetActive(false);
    }
    private void Attack()
    {
        player.GetComponent<Health>().TakeDamage(damage);
        nextAttackTime = Time.time + damageInterval;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && alive)
        {
            if (nextAttackTime < Time.time)
                enemyAnimator.SetTrigger("Attack");
        }
    }
}
