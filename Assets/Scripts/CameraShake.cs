using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // How long it will shake
    [SerializeField]
    private float _shakeDuration = 0.1f;
    // How much it will shake
    [SerializeField]
    private float _shakeStrength = 0.2f;
    private bool isShaking = false;

 

    private IEnumerator Shake()
    {
        if (isShaking)
        {
            yield return null;
        }
        isShaking = true;
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < _shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * _shakeStrength;
            float y = Random.Range(-1f, 1f) * _shakeStrength;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
        isShaking = false;
    }


    public void ShakePlayer()
    {
        StartCoroutine(Shake());
    }
}
