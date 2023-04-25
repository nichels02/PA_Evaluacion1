using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private float bulletVelocityMultiplier = 5f;
    [SerializeField] private HealthBarController healthBarController;
    [SerializeField] private CameraManager cameraManager;

    public UnityEvent onPlayerDeath;

    private void Start() {
        healthBarController.onPlayerDamage += cameraManager.ScreenShake;
        healthBarController.onEnitityDeath += OnPlayerDeath;
    }

    private void Update() {
        Vector2 movementPlayer = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        myRBD2.velocity = movementPlayer * velocityModifier;

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 newDistane = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Debug.DrawRay(transform.position, newDistane * rayDistance, Color.white);

        CheckFlip(newDistane.x);
        
        if(Input.GetMouseButtonDown(0)){
            Instantiate(bulletPrefab, transform.position, Quaternion.identity).SetUpVelocity(newDistane.normalized * bulletVelocityMultiplier, gameObject.tag);
        }else if(Input.GetMouseButtonDown(1)){
        }
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = x_Position < 0;
    }

    private void OnPlayerDeath(){
        onPlayerDeath?.Invoke();
    }
}
