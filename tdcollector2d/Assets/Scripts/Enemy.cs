using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        Transform enemyPrefab = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform = Instantiate(enemyPrefab, position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }
    private Building HQBuilding;
    private Transform targetTransform;
    private Rigidbody2D rigidbody2d;
    private float targetTimer;
    private float targetTimerMax = 0.2f;
   
    void Start()
    {
        GetComponent<HealthSystem>().onDied += Enemy_onDied;
        rigidbody2d = GetComponent<Rigidbody2D>();
        HQBuilding = BuildingManager.Instance.GetHQBuilding();
        targetTransform = HQBuilding.transform;
        targetTimer = Random.Range(0, targetTimerMax);
    }

    private void Enemy_onDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        HandleMovement();
        HandleTargeting();


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.transform.GetComponent<Building>();
        if(building != null)
        {
            building.GetComponent<HealthSystem>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
    
    private void LookForTarget()
    {
        float maxtargetarea = 10;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, maxtargetarea);

        foreach(Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();
            if(building != null)
            {
                if(targetTransform == null)
                {
                    targetTransform = building.transform;

                }
                else
                {
                    if (Vector3.Distance(building.transform.position, transform.position) < Vector3.Distance(targetTransform.position,transform.position ))
                    {
                        targetTransform = building.transform;
                    }
                }
            }
            if(targetTransform == null)
            {
                targetTransform = HQBuilding.transform;
            }
        }
    }
    private void HandleMovement()
    {
        if (targetTransform != null)
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;
            float moveSpeed = 8f;
            rigidbody2d.velocity = moveDir * moveSpeed;
        }
        else
        {
            rigidbody2d.velocity = new Vector3(0, 0, 0);
        }
    }

    private void HandleTargeting()
    {
        targetTimer -= Time.deltaTime;
        if (targetTimer <= 0)
        {
            targetTimer = targetTimerMax;
            LookForTarget();
        }
    }
}
