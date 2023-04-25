using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    [Header("Raycast")]
    [SerializeField] private float distanceRaycast = 10f;
    [SerializeField] private LayerMask physicRaycast;
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    private float normalVelocity;
    private float increasedVelocity;

    private void Start() {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;

        normalVelocity = velocityModifier;
        increasedVelocity = velocityModifier * 3.5f;
    }

    private void Update() {
        CheckNewPoint();
        RaycastLogic();

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
    }

    private void CheckNewPoint(){
        if(Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25){
            transform.position = currentPositionTarget.position;
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length? 0: patrolPos+1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized*velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }
        
    }

    private void RaycastLogic(){
        Vector2 distance = currentPositionTarget.position - transform.position;
        RaycastHit2D hit =  Physics2D.Raycast(transform.position, distance.normalized, distanceRaycast,physicRaycast);
        Debug.DrawRay(transform.position, distance.normalized * distanceRaycast, Color.gray);

        if(hit){
            if(hit.collider.CompareTag("Player")){
                Debug.Log(hit.collider.gameObject.name);
                velocityModifier = increasedVelocity;
                myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized*velocityModifier;
            }
        }else{
            velocityModifier = normalVelocity;
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized*velocityModifier;
        }
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
}
