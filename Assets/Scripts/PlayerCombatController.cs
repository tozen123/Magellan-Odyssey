using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private FixedJoystick lookAttackJoystick;


    [Header("Attack Configuration")]
    
    [Range(0.5f, 1.0f)]
    [SerializeField] private float attackSpeed;
    private float attackTime;


    [Range(0.1f, 1.0f)]
    [SerializeField] private float attackSensitivity;

    [Range(0.1f, 1.0f)]
    [SerializeField] private float attackAimLineSensitivity;

    private Vector3 _playerLookAttackInput;

    [Header("Projectile")]
    [SerializeField] private GameObject playerProjectile;

    [Header("Shoot Point")]
    [SerializeField] private Transform playerProjectileShootPoint;

    [Header("Raycast")]
    [SerializeField] private LineRenderer aimLineRenderer;
    [SerializeField] private float maxAimDistance = 15f;  // Maximum distance for the raycast
    [SerializeField] private LayerMask aimLayerMask;  // Layer mask to specify which objects the raycast can hit
    [SerializeField] private Color rayColor = Color.red;  // Color of the debug ray
    private void Awake()
    {
   
    }


    private void Update()
    {
        _playerLookAttackInput = new Vector3(lookAttackJoystick.Horizontal, 0, lookAttackJoystick.Vertical);

        UpdateAimLine();
        if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;
        }

        if (_playerLookAttackInput.magnitude > attackSensitivity && attackTime <= 0)
        {
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        attackTime = attackSpeed;

        Instantiate(playerProjectile, playerProjectileShootPoint.position, playerProjectileShootPoint.rotation);
    }

    private void UpdateAimLine()
    {
        if (_playerLookAttackInput.magnitude > attackAimLineSensitivity)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var newInput = matrix.MultiplyPoint3x4(_playerLookAttackInput).normalized;

            Vector3 aimDirection = newInput;

            Ray ray = new Ray(playerProjectileShootPoint.position, aimDirection);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxAimDistance, aimLayerMask))
            {
                Debug.DrawRay(ray.origin, aimDirection * hit.distance, rayColor);

                aimLineRenderer.SetPosition(0, playerProjectileShootPoint.position);
                aimLineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                Debug.DrawRay(ray.origin, aimDirection * maxAimDistance, rayColor);

                aimLineRenderer.SetPosition(0, playerProjectileShootPoint.position);
                aimLineRenderer.SetPosition(1, ray.origin + aimDirection * maxAimDistance);
            }

            aimLineRenderer.enabled = true;
        }
        else
        {
            aimLineRenderer.enabled = false;
        }
    }


}
