using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;

    private float _maxDistance = 7f;

    private Transform _parentTransform;

    private bool _isEnemyLaser;

    private void Start()
    {
        _parentTransform = transform.parent;

        if (gameObject.name == "Enemy_Laser")
            _isEnemyLaser = true;

    }


    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    /// <summary>
    /// Calculates movement for the laser. Destroys after max distance.
    /// </summary>
    private void CalculateMovement()
    {
        if (_isEnemyLaser)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

            if (transform.position.y > _maxDistance)
            {
                if (_parentTransform)
                {
                    Destroy(_parentTransform.gameObject);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

            if (transform.position.y > _maxDistance)
            {
                if (_parentTransform)
                {
                    Destroy(_parentTransform.gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
}
