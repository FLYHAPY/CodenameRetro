using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CamaraEffects : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] AnimationCurve curve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator DoShake()
    {
        Vector3 startposition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startposition + Random.insideUnitSphere * strength;
            yield return null;
        }
        
        transform.position = startposition;
    }

    public void Shake()
    {
        StartCoroutine(DoShake());
    }
}
