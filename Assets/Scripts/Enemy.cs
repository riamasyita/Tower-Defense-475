using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // komponen untuk enemy
    [SerializeField] private int _maxHealth = 1;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private SpriteRenderer _healthBar;
    [SerializeField] private SpriteRenderer _healthFill;

    private int _currentHealt;

    public Vector3 TargetPosition { get; private set; }
    public int CurrentPathIndex { get; private set; }

    // fungsi ini terpanggil setiap kali menghidupkan game object
    // yang memiliki script ini
    private void OnEnable()
    {
        _currentHealt = _maxHealth;
        _healthFill.size = _healthBar.size;
    }

    // untuk bergerak ke arah target
    public void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, _moveSpeed * Time.deltaTime);
    }

    // set posisi target
    public void SetTargetPosition(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
        _healthBar.transform.parent = null;

        // Mengubah rotasi dari enemy
        Vector3 distance = TargetPosition - transform.position;

        if (Mathf.Abs(distance.y) > Mathf.Abs(distance.x))
        {
            // Menghadap atas
            if (distance.y > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            }

            // Menghadap bawah
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
            }
        }

        else
        {
            // Menghadap kanan (default)
            if (distance.x > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }

            // Menghadap kiri
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            }
        }

        _healthBar.transform.parent = transform;

    }

    // Fungsi untuk me reduce / mengurangi darah musuh
    // ketika terkena damage atau serangan
    public void ReduceEnemyHealth(int damage)
    {
        _currentHealt -= damage;
        AudioPlayer.Instance.PlaySFX("hit-enemy");

        // kondisi dimana jika current health lebih kecil saama dengan 0
        if (_currentHealt <= 0)
        {
            gameObject.SetActive(false);
            AudioPlayer.Instance.PlaySFX("enemy-die");
        }

        float healthPercentage = (float) _currentHealt / _maxHealth;
        _healthFill.size = new Vector2(healthPercentage * _healthBar.size.x, _healthBar.size.y);
    }

    // Menandai indeks terakhir pada path
    public void SetCurrentPathIndex(int currentIndex)
    {
        CurrentPathIndex = currentIndex;
    }

}
