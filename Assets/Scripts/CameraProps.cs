using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProps : Singleton<CameraProps>
{
    [SerializeField] private Camera _camera;
    private Vector3 _startPos;
    private float _shakeDuration = 0.5f;
    private float _shakeMagnitude = 1f;

    private void Start()
    {
        _startPos = _camera.transform.position;
    }

    public void CamShake(float duration)
    {
        _shakeDuration = duration;
        StartCoroutine(_shakeCoroutine());
    }

    private IEnumerator _shakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _shakeDuration)
        {
            float offsetX = Random.Range(-_shakeMagnitude, _shakeMagnitude);
            float offsetY = Random.Range(-_shakeMagnitude, _shakeMagnitude);

            _camera.transform.position = new Vector3(_startPos.x + offsetX, _startPos.y + offsetY, _startPos.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _camera.transform.position = _startPos;
    }
}
