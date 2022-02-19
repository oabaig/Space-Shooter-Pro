using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    /// <summary>
    /// Speed of current powerup
    /// </summary>
    [SerializeField] private float _speed = 3f;
    [SerializeField] private AudioClip _powerupAudio = null;

    private float _screenBottom = ScreenBounds.GetScreenBottom();

    // Update is called once per frame
    void Update()
    {
        // move down at a speed
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // when leave the screen, destroy object
        if (transform.position.y < _screenBottom)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }

    protected void PlayPowerUpSound() {
       AudioSource.PlayClipAtPoint(_powerupAudio, new Vector3(0,0,-10));
    }
}
