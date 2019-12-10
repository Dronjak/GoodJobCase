using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    
    private float shakeDuration = 0.1f;
    private float shakeAmount = 0.1f;
    private float decreaseFactor = 1.0f;
    
    Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator ShakeCo()
    {
        originalPos = transform.position;
        while (shakeDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
            yield return null;
        }
        shakeDuration = 0.1f;
        transform.localPosition = originalPos;
    }
    public void Shake()
    {
        StartCoroutine(ShakeCo());
    }
}
