using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] private float _flickerTime;

    private TextMeshProUGUI _textComponent;
    private bool _isGameOver;

    // Singletons
    private EventManager _EventManager;

    private void Start()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();

        _EventManager = EventManager.instance;

        _EventManager.AddGameOverEventListener(ActivateSelf);

        _isGameOver = false;
    }

    private void ActivateSelf(bool isEnabled)
    {
        _isGameOver = isEnabled;
        StartCoroutine(FlickerDisplayRoutine());
    }

    private IEnumerator FlickerDisplayRoutine()
    {
        while (_isGameOver)
        {
            _textComponent.enabled = true;
            yield return new WaitForSeconds(_flickerTime);
            _textComponent.enabled = false;
            yield return new WaitForSeconds(_flickerTime);
        }
    }
}
