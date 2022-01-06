using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] private List<Sprite> _livesImages = null;

    private Image _livesDisplay = null;

    // Singletons
    private EventManager _EventManager = null;

    // Start is called before the first frame update
    void Start()
    {
        _livesDisplay = GetComponent<Image>();

        _EventManager = EventManager.instance;
        _EventManager.AddUpdateLivesListener(UpdateNumberLivesDisplay);
    }

    private void UpdateNumberLivesDisplay(int numLives)
    {
        _livesDisplay.sprite = _livesImages[numLives];
    }
}
