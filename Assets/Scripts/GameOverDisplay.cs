using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverDisplay : MonoBehaviour
{
    private TextMeshProUGUI _textComponent;

    // Singletons
    private EventManager _EventManager;

    private void Start()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();

        _EventManager = EventManager.instance;

        _EventManager.AddGameOverEventListener(ActivateSelf);
    }

    private void ActivateSelf()
    {
        _textComponent.enabled = true;
    }
}
