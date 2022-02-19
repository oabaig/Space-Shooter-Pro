using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : Powerup
{
    [SerializeField] private float _speedBoostMultiplier = 2.0f;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            base.PlayPowerUpSound();

            collision.gameObject.GetComponent<Player>().SetSpeedBoostActive(_speedBoostMultiplier);
            Destroy(this.gameObject);
        }
    }
}
