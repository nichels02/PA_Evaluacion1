using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] public int damageBullet;
    private string currentTag;
    public event Action<BulletController, HealthBarController> onCollision;

    private void OnEnable() {
        HealthManager.instance.SetUpdateHealth(this);
    }

    public void SetUpVelocity(Vector2 newVelocity, string newTag){
        myRBD2.velocity = newVelocity;
        currentTag = newTag;
    }

    private void OnBecameInvisible() {
        DestroyBullet();
    }

    private void DestroyBullet(){
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag != currentTag && (other.CompareTag("Player") || other.CompareTag("Enemy"))){
            onCollision?.Invoke(this, other.GetComponent<HealthBarController>());
            DestroyBullet();
        }
    }
}
