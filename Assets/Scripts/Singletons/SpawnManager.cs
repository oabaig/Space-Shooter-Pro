using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Singleton
    public static SpawnManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of " + gameObject.name + " found!");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject _enemyPrefab = null;
    [SerializeField] private float _spawnTimer = 5f;
    [SerializeField] private GameObject _enemyContainer = null;
    [SerializeField] private GameObject _powerUpContainter = null;
    [SerializeField] private List<GameObject> _powerups = null;
    [SerializeField] private int _minPowerupSpawnTime = 3;
    [SerializeField] private int _maxPowerupSpawnTime = 7;


    private bool _canSpawnEnemies;
    private bool _canSpawnPowerups;

    private float _screenTop = ScreenBounds.GetScreenTop();
    private float _screenLeft = ScreenBounds.GetScreenLeft();
    private float _screenRight = ScreenBounds.GetScreenRight();

    // Start is called before the first frame update
    void Start()
    {
        _canSpawnEnemies = true;
        _canSpawnPowerups = true;
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // spawn game objects every 5 seconds
    IEnumerator SpawnEnemyRoutine()
    {
        while (_canSpawnEnemies)
        {
            if (_enemyPrefab)
            {
                float xLocation = Random.Range(_screenLeft, _screenRight);
                Vector3 spawnLocation = new Vector3(xLocation, _screenTop, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, spawnLocation, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
            }
            yield return new WaitForSeconds(_spawnTimer);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_canSpawnPowerups)
        {
            if (_powerups.Count > 0)
            {
                int time = Random.Range(_minPowerupSpawnTime, _maxPowerupSpawnTime);
                yield return new WaitForSeconds(time);

                float xLocation = Random.Range(_screenLeft, _screenRight);
                Vector3 spawnLocation = new Vector3(xLocation, _screenTop, 0);

                int index = Random.Range(0, _powerups.Count);

                GameObject powerUp = Instantiate(_powerups[index], spawnLocation, Quaternion.identity);
                powerUp.transform.parent = _powerUpContainter.transform;
            }
        }
    }

    public void OnPlayerDeath()
    {
        _canSpawnEnemies = false;
        _canSpawnPowerups = false;
    }
}
