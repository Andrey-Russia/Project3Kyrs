using UnityEngine;

public class CargoBeacon : MonoBehaviour
{
    [SerializeField] private BeaconTargetType _targetType = BeaconTargetType.Light;
    [SerializeField] private BlinkMode _currentBlinkMode = BlinkMode.Fast;

    [SerializeField] private Light _targetLight;
    [SerializeField] private Renderer _targetRenderer;

    [SerializeField] private Color _emissionColor = Color.white;

    [SerializeField] private float _minimumIntensity = 0.5f;
    [SerializeField] private float _maximumIntensity = 4.0f;

    [SerializeField] private float _fastBlinkSpeed = 8.0f;
    [SerializeField] private float _longBlinkSpeed = 1.5f;

    [SerializeField] private float _fastModeDuration = 3.0f;
    [SerializeField] private float _longModeDuration = 8.0f;

    private Material _material;
    private float _modeTimer;

    private void Reset()
    {
        _targetLight = GetComponent<Light>();
        _targetRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        if (_targetRenderer != null)
            _material = _targetRenderer.material;
    }

    private void Update()
    {
        UpdateBlink();
        UpdateMode();
    }

    private void UpdateBlink()
    {
        float blinkSpeed = GetCurrentBlinkSpeed();

        float normalizedTime = (Mathf.Sin(Time.time * blinkSpeed) + 1.0f) * 0.5f;

        float intensity = Mathf.Lerp(
            _minimumIntensity,
            _maximumIntensity,
            normalizedTime);

        if (_targetType == BeaconTargetType.Light)
            UpdateLight(intensity);
        else
            UpdateMaterial(intensity);
    }

    private void UpdateLight(float intensity)
    {
        if (_targetLight == null)
            return;

        _targetLight.intensity = intensity;
    }

    private void UpdateMaterial(float intensity)
    {
        if (_material == null)
            return;

        _material.SetColor("_EmissionColor", _emissionColor * intensity);
    }

    private void UpdateMode()
    {
        _modeTimer += Time.deltaTime;

        float currentModeDuration = GetCurrentModeDuration();

        if (_modeTimer < currentModeDuration)
            return;

        _modeTimer = 0.0f;
        SwitchBlinkMode();
    }

    private void SwitchBlinkMode()
    {
        _currentBlinkMode = _currentBlinkMode == BlinkMode.Fast ? BlinkMode.Long : BlinkMode.Fast;
    }

    private float GetCurrentBlinkSpeed()
    {
        return _currentBlinkMode == BlinkMode.Fast ? _fastBlinkSpeed : _longBlinkSpeed;
    }

    private float GetCurrentModeDuration()
    {
        return _currentBlinkMode == BlinkMode.Fast ? _fastModeDuration : _longModeDuration;
    }
}