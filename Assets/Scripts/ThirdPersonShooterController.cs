using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using UnityEngine.VFX;

public class ThirdPersonShooterController : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform aimTarget;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform vfxHitYellow;
    [SerializeField] private Transform vfxHitRed;
    [SerializeField] private Rig aimRig;
    [SerializeField] private VisualEffect visualEffect;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    AudioSource m_ShootAudioSource;
    public AudioClip ShootSfx;
    public bool UseContinuousShootSound = false;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        m_ShootAudioSource = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {            
            mouseWorldPosition = raycastHit.point;
            aimTarget.position = raycastHit.point;
            hitTransform = raycastHit.transform;
        }
        else
        {
            mouseWorldPosition = ray.GetPoint(10);
        }

        if (starterAssetsInputs.aim)
        {            
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 13f));            

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 20f);
            aimRig.weight = 1f;
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 13f));
            aimRig.weight = 0f;
        }


        if (starterAssetsInputs.shoot)
        {
            visualEffect.Play();
            // play shoot SFX
            if (ShootSfx && !UseContinuousShootSound)
            {
                m_ShootAudioSource.Play();
                Instantiate(vfxHitYellow, mouseWorldPosition, Quaternion.identity);
            }
            else
            {
                // Hit something else
                Instantiate(vfxHitRed, mouseWorldPosition, Quaternion.identity);
            }
            // Projectile Shoot
            //Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            //Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.shoot = false;
        }

    }

    private void OnDisable()
    {
        //thirdPersonController = null;
        //starterAssetsInputs = null;
        //animator = null;
        //m_ShootAudioSource = null;
        Debug.Log($"{gameObject.name} has been disabled");
    }

    // Restore the saved transform when reactivating
    private void OnEnable()
    {
        //thirdPersonController = GetComponent<ThirdPersonController>();
        //starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        //animator = GetComponent<Animator>();
        //m_ShootAudioSource = GetComponent<AudioSource>();
    }

}