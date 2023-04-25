using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovementController : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private float velocityModifier;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private float bulletVelocityMultiplier;
    [SerializeField] private float timeToShoot = 0.5f;
    private bool followPlayer;
    private bool canMove;
    private bool canShoot = true;
    private Transform targetFollow;

    private void Update() {
        if(followPlayer){
            MoveTowards(targetFollow);
            if(canShoot){
                StartCoroutine(FireBullet(targetFollow));
                canShoot = false;
            }
        }else if(canMove){
            MoveTowards(transform);
            SetPosition(transform);
        }

        enemyObject.GetComponent<AnimatorController>().SetVelocity(velocityCharacter: enemyObject.GetComponent<Rigidbody2D>().velocity.magnitude);
    }

    private void MoveTowards(Transform currentTarget){
        Vector2 distanceVector = currentTarget.position - enemyObject.transform.position;
        enemyObject.GetComponent<Rigidbody2D>().velocity = distanceVector.normalized*velocityModifier;
        canMove = true;
    }

    IEnumerator FireBullet(Transform currentTarget){
        Vector2 distanceVector = currentTarget.position - enemyObject.transform.position;
        Instantiate(bulletPrefab, enemyObject.transform.position, Quaternion.identity).SetUpVelocity(distanceVector.normalized * bulletVelocityMultiplier, gameObject.tag);
        yield return new WaitForSeconds(timeToShoot);
        canShoot = true;
    }

    private void SetPosition(Transform currentTarget){
        Vector2 distanceVector = currentTarget.position - enemyObject.transform.position;

        if(distanceVector.magnitude < 0.15f){
            enemyObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            enemyObject.transform.position = currentTarget.position;
            canMove = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            targetFollow = other.transform;
            followPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            targetFollow = null;
            followPlayer = false;
        }
    }
}
