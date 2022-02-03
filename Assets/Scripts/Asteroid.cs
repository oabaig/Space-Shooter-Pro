using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private GameObject _explosionPrefab = null;

    // Update is called once per frame
    void Update()
    {
        RotateAsteroid();
    }

    /// <summary>
    /// Rotates the asteroid along the z-axis
    /// </summary>
    private void RotateAsteroid()
    {
        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            Instantiate(_explosionPrefab);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
