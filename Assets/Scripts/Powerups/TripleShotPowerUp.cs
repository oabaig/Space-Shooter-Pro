using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPowerUp : Powerup
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().SetTripleShotActive();
            Destroy(this.gameObject);
        }
    }
}
