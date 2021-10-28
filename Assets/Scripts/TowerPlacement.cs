using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    // komponen
    private Tower _placedTower;

    // Fungsi yang terpanggil sekali ketika ada object Rigidbody yang
    // menyentuh area collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // jika place tower tidak sama dengan null
        if (_placedTower != null)
        {
            return;
        }

        // memberikan komponen kepada tower
        Tower tower = collision.GetComponent<Tower>();
        // jika tower tidak sama dengan null, maka akan mengatur posisi
        if  (tower != null)
        {
            tower.SetPlacePosition(transform.position);
            _placedTower = tower;
        }
    }

    // Kebalikan dari OnTriggerEnter2D, fungsi ini terpanggil sekali 
    // ketika object tersebut meninggalkan area collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_placedTower == null)
        {
            return;
        }

        _placedTower.SetPlacePosition(null);
        _placedTower = null;
    }
}
