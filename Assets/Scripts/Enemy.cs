using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    // properties
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _minShootTime = 0.5f;
    [SerializeField] private float _maxShootTime = 1.5f;

    // prefabs
    [SerializeField] private GameObject _explosionPrefab = null;
    [SerializeField] private GameObject _laserPrefab = null;

    private float _screenBottom = ScreenBounds.GetScreenBottom();
    private float _screenTop = ScreenBounds.GetScreenTop();
    private float _screenLeft = ScreenBounds.GetScreenLeft();
    private float _screenRight = ScreenBounds.GetScreenRight();

    // Singletons
    private EventManager _EventManager;

    // Events
    private PointsAddedEvent _PointsAddedEvent = new PointsAddedEvent();

    private float _laserSpawnOffset = -1.06f;
    private bool _canFire;

    private void Start()
    {
        _EventManager = EventManager.instance;

        _EventManager.AddPointsAddedInvoker(this);

        _canFire = true;
        StartCoroutine(FireLaser());
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private IEnumerator FireLaser()
    {
        while (_canFire)
        {
            float shootTimer = Random.Range(_minShootTime, _maxShootTime);
            yield return new WaitForSeconds(shootTimer);

            Vector3 spawnPosition = transform.position;
            spawnPosition.y += _laserSpawnOffset;
            GameObject laserInstance = Instantiate(_laserPrefab, spawnPosition, Quaternion.identity);
        }
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
        if (other.tag == "Laser")
        {
            _PointsAddedEvent.Invoke(10);

            _EventManager.RemovePointsAddedInvoker(this);

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            _canFire = false;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                other.GetComponent<Player>().Damage();
                Debug.Log(gameObject.name + ": Dealt Damage to Player");
            }

            _canFire = false;
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
