using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform targetEnemyTransform;
    private float targetTimer;
    private float targetTimerMax = 0.2f;
    private float fireSpeedTimer;
    [SerializeField] float fireSpeedTimerMax;
    [SerializeField] Transform arrowProjectileSpawnPosition;

    void Update()
    {
        HandleTargeting();
        HandleShooting();
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
   
    private void LookForTarget()
    {
        float maxtargetarea = 20;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, maxtargetarea);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemyTransform == null)
                {
                    targetEnemyTransform = enemy.transform;

                }
                else
                {
                    if (Vector3.Distance(enemy.transform.position, transform.position) < Vector3.Distance(targetEnemyTransform.position, transform.position))
                    {
                        targetEnemyTransform = enemy.transform;
                    }
                }
            }
        }
    }
    private void HandleShooting()
    {
        fireSpeedTimer -= Time.deltaTime;
        if(fireSpeedTimer <= 0)
        {
            fireSpeedTimer = fireSpeedTimerMax;
            if(targetEnemyTransform != null)
            {
                ArrowProjectile.Create(arrowProjectileSpawnPosition.position, targetEnemyTransform);
            }
        }
    }
}
