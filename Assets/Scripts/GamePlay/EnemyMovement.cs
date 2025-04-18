using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;    

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;        

    private Transform target;
    private int pathIndex = 0;
    private LiveManager liveManager;

    private float baseSpeed;

    private void Start()
    {
        liveManager = FindObjectOfType<LiveManager>();
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];           
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;            

            if (pathIndex == LevelManager.main.path.Length)
            {
                if (liveManager != null)
                {
                    liveManager.ReduceLives(1);
                    Debug.Log("REDUCE LIVE BY ! BECUASE ENEMY REACH ENDPOINT");
                } 
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);                             
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];                
            }
        }        
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction*moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}
