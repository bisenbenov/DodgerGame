using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Border _bottom;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Goodie _goodie;
    
    private int _score = 0;

    public void SetDefault()
    {
        _score = 0;
        _scoreText.text = $"Score: {0}";
    }

    private void OnEnable()
    {
        _bottom.OnHitBottom += ChangeScore;
    }

    private void ChangeScore(object sender, EventArgs e)
    {
        if (_goodie.isGoodieAlive)
        {
            _scoreText.text = $"Score: {++_score}";
        }
    }

    private void OnDisable()
    {
        _bottom.OnHitBottom -= ChangeScore;
    }
}
