using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    private Animator ch_animator;

    public Transform player;

    public LayerMask whatIsPlayer;

    public float health;
    public Image healthBar;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    bool reloading;
    int bulletsLeft;
    public int magazineSize;
    public float reloadTime;
    public GameObject muzzleFlash;
    public List<GameObject> bloodSpotPrefabs;
    private List<GameObject> bloodSpots;

    public AudioClip reloadAudio, shootAudio;
    protected AudioSource AudioSorcePlayer;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool isDeath = false;
    private bool dontSeePlayer = false;

    public RaycastHit rayHit;
    private CapsuleCollider enemy_collider;
    private Rigidbody rb;
    
    protected Transform attackPoint;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        ch_animator = GetComponent<Animator>();
        enemy_collider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        bloodSpots = new List<GameObject>();
        attackPoint = transform.Find("AttackPoint");
        AudioSorcePlayer = GetComponent<AudioSource>();
        bulletsLeft = magazineSize;
    }

    private void Update()
    {
        if (!isDeath) {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            if (dontSeePlayer) {
                playerInAttackRange = Physics.CheckSphere(transform.position, 4, whatIsPlayer);
            } else {
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            }

            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        ch_animator.SetTrigger("Run");
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        ch_animator.SetTrigger("ReadyShoot");
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked && !reloading && bulletsLeft > 0)
        {
            float x = 0;

            if (!dontSeePlayer) {
                x = Random.Range(-0.15f, 0.15f);
            }

            //Calculate Direction with Spread
            Vector3 direction = transform.forward + new Vector3(x, 0, 0);

            //RayCast
            if (Physics.Raycast(transform.position + new Vector3(0, 1.4f, 0), direction, out rayHit, attackRange, ~0))
            {
                if (rayHit.collider.CompareTag("Player")) {
                    dontSeePlayer = false;
                } else {
                    dontSeePlayer = true;
                }

                AudioSorcePlayer.PlayOneShot(shootAudio);
                Instantiate(muzzleFlash, attackPoint.position, Quaternion.Euler(0, 160, 0));
                alreadyAttacked = true;
                bulletsLeft--;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }

        }

        if (bulletsLeft == 0 && !reloading) {
            Reload();
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Reload()
    {
        reloading = true;
        AudioSorcePlayer.PlayOneShot(reloadAudio);
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.fillAmount = health / 100f;

        float randomToShowBlood = Random.Range(0, 10);
        if (randomToShowBlood == 5) { // just random number
            showBloodOnFloor(bloodSpotPrefabs[1]);
        }

        if (health <= 0) {
            isDeath = true;
            ch_animator.SetTrigger("Death");
            enemy_collider.isTrigger = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            showBloodOnFloor(bloodSpotPrefabs[0]);
            Destroy(transform.Find("Health").gameObject);
            Invoke(nameof(DestroyEnemy), 60f);
        }
    }

    private void showBloodOnFloor(GameObject bloodItem) {
        float y = Random.Range(-180, 180);
        var bloodSpotInstance = Instantiate(bloodItem, transform.position, Quaternion.Euler(0, y, 0));
        bloodSpots.Add(bloodSpotInstance);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);

        foreach(GameObject blood in bloodSpots) {
            Destroy(blood);
        }
        bloodSpots.Clear();
    }
}
