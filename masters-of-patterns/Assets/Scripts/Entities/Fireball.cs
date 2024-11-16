using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public bool active;
    public bool isTowardsAPoint;
    public Vector2 direction;
    public Vector2 finalPoint;
    public float speed;
    public float explodeTime;
    public float currentExplodeTime;

    void Start ()
    {
        currentExplodeTime = 0.0f;
        active = false;

        if(isTowardsAPoint)
        {
            CalcDirection();
        }
    }

    void Update()
    {
        if(active)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(direction*speed*Time.deltaTime);
        currentExplodeTime += Time.deltaTime;
            if(currentExplodeTime >= explodeTime)
            {
                Destroy(this.gameObject);
            }

    }

    public void CalcDirection()
    {
        //Calcula a direção do ponto
        Vector2 newDirection = new Vector2(
            finalPoint.x - transform.position.x,
            finalPoint.y - transform.position.y
        );
        direction = newDirection.normalized;
    }
}
