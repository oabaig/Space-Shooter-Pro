using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float _flickerTime = 0f;
    [SerializeField] private TextMeshProUGUI _scoreTextField = null;
    [SerializeField] private TextMeshProUGUI _gameOverTextField = null;
    [SerializeField] private TextMeshProUGUI _restartTextField = null;
    [SerializeField] private List<Sprite> _livesImages = null;
    [SerializeField] private Image _livesDisplay = null;


    private const string _scoreText = "Score: ";
    private int _currentScore;
    private bool _isGameOver;

    // Singletons
    private EventManager _EventManager;
    private GameMaster _GameMaster;

    // Start is called before the first frame update
    private void Start()
    {
        _EventManager = EventManager.instance;
        _GameMaster = GameMaster.instance;

        _isGameOver = false;
        _currentScore = 0;
        UpdateScoreText(_currentScore);

        _EventManager.AddPointsAddedListener(UpdateScoreText);
        _EventManager.AddUpdateLivesListener(UpdateNumberLivesDisplay);
    }

    private void UpdateScoreText(int score)
    {
        _currentScore += score;

        _scoreTextField.text = _scoreText + _currentScore;
    }

    private void EnableGameOverDisplay(bool isEnabled)
    {
        _isGameOver = isEnabled;
        _restartTextField.enabled = true;
        StartCoroutine(FlickerDisplayRoutine());
    }

    private void UpdateNumberLivesDisplay(int numLives)
    {
        _livesDisplay.sprite = _livesImages[numLives];

        if (numLives <= 0)
        {
            _isGameOver = true;
            EnableGameOverDisplay(_isGameOver);

            _GameMaster.SetIsGameOver(_isGameOver);
        }
    }

    private IEnumerator FlickerDisplayRoutine()
    {
        while (_isGameOver)
        {
            _gameOverTextField.enabled = true;
            yield return new WaitForSeconds(_flickerTime);
            _gameOverTextField.enabled = false;
            yield return new WaitForSeconds(_flickerTime);
        }
    }
}
