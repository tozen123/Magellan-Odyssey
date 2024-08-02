using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed;
    private Vector3 _currentVelocity = Vector3.zero;


    [SerializeField] private WorldObjectFadingHandler _FadingHandler;
    [SerializeField] private GameObject _Player;

    private void Awake() => _offset = transform.position - target.position;

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothSpeed);
    }

    private void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");

    }
    private void Update()
    {
        if (_Player != null)
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 direction = _Player.transform.position - cameraPosition;

            Ray ray = new Ray(cameraPosition, direction);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red); 
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider == null)
                {
                    return;
                }
                if (hit.collider.gameObject == _Player)
                {
                    
                    if (_FadingHandler != null)
                    {
                        _FadingHandler.activeFade = false;
                    }
                    
                }
                else
                {

                    _FadingHandler = hit.collider.gameObject.GetComponent<WorldObjectFadingHandler>();
                    if (_FadingHandler != null)
                    {
                        _FadingHandler.activeFade = true;
                    }
                }
            }
        }
    }
}