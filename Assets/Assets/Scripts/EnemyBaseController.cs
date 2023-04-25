using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseController : MonoBehaviour
{
    [SerializeField] private int pointsEnemy;
    [SerializeField] private HealthBarController healthBarController;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private GameObject parentGameObject;

    // Start is called before the first frame update
    void Start()
    {
        healthBarController.onEnitityDeath += UpdateScore;
        healthBarController.onEnitityDeath += Destroy;
    }

    private void UpdateScore(){
        GuiManager.instance.UpdateScore(pointsEnemy);
    }

    private void Destroy(){
        animatorController.SetDie();
        Destroy(parentGameObject, 1f);
    }
}
