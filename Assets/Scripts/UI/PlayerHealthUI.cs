
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerInfo Player { get; set; }
    public PlayerHealth PlayerHealth { get; set; }

    [Header("References")]
    [SerializeField] Image _bgUI;
    [SerializeField] TextMeshProUGUI _healthUIText;
    [SerializeField] TextMeshProUGUI _colorUIText;

    bool _isShaking = false;
    Tween _shakeTween;

    public void UpdateText()
    {
        _bgUI.color = Player.MaterialColor;
        _colorUIText.text = Player.transform.name;
        _healthUIText.text = PlayerHealth.Health.ToString("N1");
    }

    void Update()
    {
        _healthUIText.text = PlayerHealth.Health.ToString("N1");
        _healthUIText.color = Color.Lerp(Color.red, Color.white, PlayerHealth.Health / 100);

        if (PlayerHealth.Health <= 20 & !_isShaking)
        {
            _isShaking = true;
            _shakeTween = _healthUIText.transform.DOShakePosition(1, 10, 10).SetLoops(-1, LoopType.Restart);
        }
        else
        {
            _isShaking = false;
            if (_shakeTween != null)
            {
                _shakeTween.Kill();
                _shakeTween = null;
            }
        }

    }
}
