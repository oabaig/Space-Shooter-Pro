using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;

    [SerializeField] private GameObject _laserPrefab = null;
    [SerializeField] private GameObject _tripleLaserPrefab = null;

    [SerializeField] private float _fireRate = 0.5f;
    private float _laserSpawnOffset = 1.06f;
    private float _canFire = -1f;

    // player bounds
    private const float MAX_X = 11.3f;
    private const float MIN_X = -11.3f;
    private const float MAX_Y = 0;
    private const float MIN_Y = -3.8f;

    [SerializeField] private int _numberLives = 3;

    // powerups
    [SerializeField] private float _tripleShotActiveTime = 5f;
    [SerializeField] private float _speedBoostActiveTime = 5f;
    private bool _canFireTripleShot;
    private float _speedBoostMultiplier;
    private bool _isShieldActive;

    [SerializeField] private GameObject _playerShields = null;

    // Singletons
    private SpawnManager _SpawnManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _SpawnManager = SpawnManager.instance;

        _canFireTripleShot = false;
        _speedBoostMultiplier = 1;
        _isShieldActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            if (_canFireTripleShot)
            {
                FireLaser(_tripleLaserPrefab);
            }
            else
            {
                FireLaser(_laserPrefab);
            }
        }
    }

    /// <summary>
    /// Calculates movement based on user input.
    /// </summary>
    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 newPosition = new Vector3(horizontalInput, verticalInput) * _speed * Time.deltaTime * _speedBoostMultiplier;
        transform.Translate(newPosition);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y), 0);

        if (transform.position.x >= MAX_X)
        {
            transform.position = new Vector3(MIN_X, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= MIN_X)
        {
            transform.position = new Vector3(MAX_X, transform.position.y, transform.position.z);
        }
    }

    /// <summary>
    /// Fires the laser.
    /// </summary>
    private void FireLaser(GameObject laser)
    {
        _canFire = Time.time + _fireRate;

        Vector3 spawnPosition = transform.position;
        spawnPosition.y += _laserSpawnOffset;
        Instantiate(laser, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// Deals damage to player.
    /// </summary>
    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _playerShields.SetActive(false);
            return;
        }
        _numberLives--;

        if(_numberLives < 1)
        {
            _SpawnManager.OnPlayerDeath();

            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Sets Triple Shot Active.
    /// </summary>
    public void SetTripleShotActive()
    {
        _canFireTripleShot = true;
        StartCoroutine(TripleShotPowerupRoutine());
    }

    /// <summary>
    /// Sets speed boost multiplier with a given multiplier input.
    /// </summary>
    /// <param name="multiplier"></param>
    public void SetSpeedBoostActive(float multiplier)
    {
        _speedBoostMultiplier = multiplier;
        StartCoroutine(SpeedBoostPowerupRoutine());
    }

    public void SetShieldPowerup()
    {
        _isShieldActive = true;
        _playerShields.SetActive(true);
    }

    IEnumerator TripleShotPowerupRoutine()
    {
        yield return new WaitForSeconds(_tripleShotActiveTime);
        _canFireTripleShot = false;
    }

    IEnumerator SpeedBoostPowerupRoutine()
    {
        yield return new WaitForSeconds(_speedBoostActiveTime);
        _speedBoostMultiplier = 1;
    }
}
