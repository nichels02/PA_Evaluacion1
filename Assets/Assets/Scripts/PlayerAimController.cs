using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    [Header("Mouse Positions")]
    public Vector3 mousePosition;
    public Vector3 mousPositionCamera;
    public Vector3 mousPositionCameraNormalized;
    public Vector3 aimVector;
    
    [Header("Mouse Positions")]
    [SerializeField] private GameObject aimMousePosition;
    [SerializeField] private GameObject aimPositionCamera;
    [SerializeField] private GameObject aimPositionCameraNormalized;
    [SerializeField] private GameObject aimTarget;

    [Header("Others")]
    [SerializeField] private float rayDistance = 10f;

    private void Update() {
        mousePosition = Input.mousePosition;
        aimMousePosition.transform.position = mousePosition;
        Debug.DrawRay(transform.position, mousePosition * rayDistance, Color.green);
        
        mousPositionCamera = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimPositionCamera.transform.position = mousPositionCamera;
        Debug.DrawRay(transform.position, mousPositionCamera * rayDistance, Color.yellow);

        mousPositionCameraNormalized = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
        aimPositionCameraNormalized.transform.position = mousPositionCameraNormalized;
        Debug.DrawRay(transform.position, mousPositionCameraNormalized * rayDistance, Color.red);

        aimVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aimTarget.transform.position = aimVector;
        Debug.DrawRay(transform.position, aimVector * rayDistance, Color.cyan);
    }
}
