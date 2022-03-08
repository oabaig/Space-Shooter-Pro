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
    [SerializeField] private List<Image> _livesDisplay = null;

    private const string _scoreText = "Score: ";
    private int _currentScore;
    private bool _isGameOver;
    private bool _isSinglePlayer;
    private bool _isPlayerOneDead;
    private bool _isPlayerTwoDead;

    // Singletons
    private EventManager _EventManager;
    private GameMaster _GameMaster;
    private SpawnManager _SpawnManager;

    // Start is called before the first frame update
    private void Start()
    {
        _EventManager = EventManager.instance;
        _GameMaster = GameMaster.instance;
        _SpawnManager = SpawnManager.instance;

        _isSinglePlayer = _GameMaster.GetIsSinglePlayer();

        _isPlayerOneDead = false;
        _isPlayerTwoDead = _isSinglePlayer ? true : false;
        _isGameOver = false;
        _currentScore = 0;
        UpdateScoreText(_currentScore);

        _EventManager.AddPointsAddedListener(UpdateScoreText);
        _EventManager.AddUpdateLivesListener(UpdateNumberLivesDisplay);
    }

    private void UpdateScoreText(int score)
    {
        Debug.Log("Update Score Text Invoked");
        _currentScore += score;

        _scoreTextField.text = _scoreText + _currentScore;
    }

    private void EnableGameOverDisplay(bool isEnabled)
    {
        _isGameOver = isEnabled;
        _restartTextField.enabled = true;
        StartCoroutine(FlickerDisplayRoutine());
    }

    private void UpdateNumberLivesDisplay(int numLives, int playerNum)
    {
        int playerIndex = playerNum - 1;

        _livesDisplay[playerIndex].sprite = _livesImages[numLives];

        if (numLives <= 0)
        {
            if (playerNum == 1)
            {
                _isPlayerOneDead = true;
            }
            else
            {
                _isPlayerTwoDead = true;
            }
        }

        if (_isPlayerOneDead && _isPlayerTwoDead)
        {
            _SpawnManager.OnPlayerDeath();
            _isGameOver = true;
            EnableGameOverDisplay(_isGameOver);
            _GameMaster.OnGameOver();
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
