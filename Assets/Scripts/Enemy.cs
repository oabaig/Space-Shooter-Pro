using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private GameObject _explosionPrefab = null;

    private float _screenBottom = ScreenBounds.GetScreenBottom();
    private float _screenTop = ScreenBounds.GetScreenTop();
    private float _screenLeft = ScreenBounds.GetScreenLeft();
    private float _screenRight = ScreenBounds.GetScreenRight();

    // Singletons
    private EventManager _EventManager;

    // Events
    private PointsAddedEvent _PointsAddedEvent;

    private void Start()
    {
        _EventManager = EventManager.instance;

        _PointsAddedEvent = new PointsAddedEvent();

        _EventManager.AddPointsAddedInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    /// <summary>
    /// Moves enemy down. If enemy reached bottom of screen, then move back to top at a random x location.
    /// </summary>
    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < _screenBottom)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = _screenTop;
            newPosition.x = Random.Range(_screenLeft, _screenRight);
            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            _PointsAddedEvent.Invoke(10);

            _EventManager.RemovePointsAddedInvoker(this);

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                other.GetComponent<Player>().Damage();
                Debug.Log(gameObject.name + ": Dealt Damage to Player");
            }

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void AddPointsAddedEventListener(UnityAction<int> listener)
    {
        _PointsAddedEvent.AddListener(listener);
    }

    public void RemovePointsAddedEventListener(UnityAction<int> listener)
    {
        _PointsAddedEvent.RemoveListener(listener);
    }
}
