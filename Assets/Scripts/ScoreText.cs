using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI _textField;

    private const string _scoreText = "Score: ";
    private int _currentScore;

    // Singletons
    private EventManager _EventManager;

    // Start is called before the first frame update
    void Start()
    {
        _EventManager = EventManager.instance;

        _textField = gameObject.GetComponent<TextMeshProUGUI>();

        _currentScore = 0;
        UpdateScoreText(_currentScore);

        _EventManager.AddPointsAddedListener(UpdateScoreText);

    }

    void UpdateScoreText(int score)
    {
        _currentScore += score;

        _textField.text = _scoreText + _currentScore;
    }
}
