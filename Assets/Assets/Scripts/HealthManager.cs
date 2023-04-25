using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance {get; private set;}

    private void Awake() {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }

        instance = this;
    }

    public void SetUpdateHealth(BulletController bulletData){
        bulletData.onCollision += UpdateHealth;
    }
    
    private void UpdateHealth(BulletController bulletData, HealthBarController entityHealthBar){
        entityHealthBar.UpdateHealth(bulletData.damageBullet);
    }
}
