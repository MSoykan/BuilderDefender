using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public static Enemy Create(Vector3 position) {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private float hitTargetTimer;
    private float hitTargetTimerMax = 2f;
    private Rigidbody2D rb2D;
    private Transform targetTransform;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;
    private HealthSystem healthSystem;
    [SerializeField] private Animator enemyVisualAnimator;

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        if (BuildingManager.Instance.GetHQBuilding() != null) {
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        hitTargetTimer = hitTargetTimerMax;

        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        SoundManager.instance.PlaySound(SoundManager.Sound.EnemyHit, 1f);
        Debug.Log("Playing enemy sound:" + SoundManager.Sound.EnemyHit.ToString());
        //CinemachineShake.Instance.ShakeCamera(3f, .1f);
        //ChromaticAberrationEffect.Instance.SetWeight(1f);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e) {
        SoundManager.instance.PlaySound(SoundManager.Sound.EnemyDie, 1f);
        Instantiate(Resources.Load<Transform>("pfEnemyDieParticles"), transform.position, Quaternion.identity);
        Destroy(gameObject);
        //CinemachineShake.Instance.ShakeCamera(7f ,.15f);
    }

    private void Update() {
        HandleMovement();
        HandleTargeting();
        HandleHitting();
    }

    private void HandleHitting() { // Timer var, update'in içine attýk
        if (targetTransform == null) return;
        bool InAttackRange = Vector3.Distance(targetTransform.position, transform.position) < 5f;
        enemyVisualAnimator.SetBool("Attacking", InAttackRange);
        if (InAttackRange) {
            Debug.Log("OPEN FIRE");
            hitTargetTimer -= Time.deltaTime;
            if (hitTargetTimer < 0f) {
                HealthSystem targetHealthSystem = targetTransform.GetComponent<HealthSystem>();
                targetHealthSystem.Damage(3);
                hitTargetTimer = hitTargetTimerMax;
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision) {
    //    Building building = collision.gameObject.GetComponent<Building>();
    //    if (building != null) {
    //        // Collided with building
    //        HealthSystem buildingHealthSystem = building.GetComponent<HealthSystem>();
    //        buildingHealthSystem.Damage(10);
    //        this.healthSystem.Damage(999);
    //    }
    //}

    private void HitEnemy() {


        if (targetTransform == null) return;
        if (Vector3.Distance(targetTransform.position, transform.position) < 1f) {

        }
    }

    private void HandleMovement() {
        if (targetTransform != null) {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;

            float moveSpeed = 6f;
            rb2D.velocity = moveDir * moveSpeed;
        }
        else {
            rb2D.velocity = Vector2.zero;
        }
    }

    private void HandleTargeting() {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0f) {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void LookForTargets() {
        float targetMaxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray) {
            Building building = collider2D.GetComponent<Building>();
            if (building != null) {
                //It's a building 
                if (targetTransform == null) {
                    targetTransform = building.transform;
                }
                else {
                    if (Vector3.Distance(transform.position, building.transform.position) <
                        Vector3.Distance(transform.position, targetTransform.position)) {
                        //Closer!
                        targetTransform = building.transform;
                    }
                }
            }
        }
        if (targetTransform == null) {
            //Found no target within range.
            if (BuildingManager.Instance.GetHQBuilding() != null) {
                targetTransform = BuildingManager.Instance.GetHQBuilding().transform;

            }
        }
    }

}
