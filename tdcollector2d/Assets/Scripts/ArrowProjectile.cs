using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    private Transform targetEnemyTransform;
    public static ArrowProjectile Create(Vector3 position, Transform targetEnemyTransform)
    {
        Transform arrowPrefab = Resources.Load<Transform>("pfArrowProjectile");
        Transform arrowTransform = Instantiate(arrowPrefab, position, Quaternion.identity);
        ArrowProjectile arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.setTargetEnemy(targetEnemyTransform);
        return arrowProjectile;
    }

    void Update()
    {
        ArrowMovement();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Enemy enemy = collision.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.GetComponent<HealthSystem>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
    private void setTargetEnemy(Transform targetEnemyTransform)
    {
        this.targetEnemyTransform = targetEnemyTransform;
    }
    Vector3 lastMoveDir;
    float moveSpeed = 20f;

    private void ArrowMovement()
    {
        Vector3 moveDir;
        if (targetEnemyTransform != null)
        {
            moveDir = (targetEnemyTransform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.FromVectorToDegree(moveDir));
    }
}
