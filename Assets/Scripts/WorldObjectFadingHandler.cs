using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectFadingHandler : MonoBehaviour
{
    [SerializeField] private float fadingSpeed;
    [SerializeField] private float fadingAmount;

    private float _OriginalOpacity;
    private Material _ObjectMaterial;

    public bool activeFade;
    private GameObject _Player;

    public float timer = 2f;

    void Start()
    {
        _ObjectMaterial = GetComponent<Renderer>().material;
        _OriginalOpacity = _ObjectMaterial.color.a;


        _Player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if(activeFade)
        {
            FadeStateTrue();

            timer -= Time.deltaTime;

            if(timer < 0)
            {
                activeFade = false;
                timer = 2f;
            }
        }
        else
        {
            FadeStateReset();
        }


    }

    private void FadeStateTrue()
    {
       
        Color currentColor = _ObjectMaterial.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadingAmount, fadingSpeed * Time.deltaTime));
        _ObjectMaterial.color = smoothColor;

    }
    private void FadeStateReset()
    {

        Color currentColor = _ObjectMaterial.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, _OriginalOpacity, fadingSpeed * Time.deltaTime));
        _ObjectMaterial.color = smoothColor;

    }
}
