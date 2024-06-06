using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Trigger _trigger;
    [SerializeField] private float _speed;

    private Coroutine _increaseVolume;
    private Coroutine _decreaseVolume;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;

    private void OnEnable()
    {
        _trigger.GotInHouse += TurnOnAlarm;
        _trigger.GotOutHouse += TurnOffAlarm;
    }

    private void OnDisable()
    {
        _trigger.GotInHouse -= TurnOnAlarm;
        _trigger.GotOutHouse -= TurnOffAlarm;
    }

    private void TurnOnAlarm()
    {
        Stop(_decreaseVolume);
        _audioSource.Play();
        _increaseVolume = StartCoroutine(ChangeVolume(_maxVolume));
    }

    private void TurnOffAlarm()
    {
        Stop(_increaseVolume);
        _decreaseVolume = StartCoroutine(ChangeVolume(_minVolume));
    }

    private IEnumerator ChangeVolume(float target)
    {
        while (_audioSource.volume != target)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _speed * Time.deltaTime);
            yield return null;
        }

        if (_audioSource.volume == _minVolume)
        {
            _audioSource.Stop();
        }
    }

    private void Stop(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
}
