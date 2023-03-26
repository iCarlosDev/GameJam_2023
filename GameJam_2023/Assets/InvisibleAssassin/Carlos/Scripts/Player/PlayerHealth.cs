using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerStorage _playerStorage;

    private void Awake()
    {
        _playerStorage = GetComponent<PlayerStorage>();
    }

    private void TakeDamage()
    {
        GameManager.instance.CurrentLives --;
        Destroy(_playerStorage.HealthBarController.HeartsBox.GetChild(0).gameObject);

        if (GameManager.instance.CurrentLives == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _playerStorage.PlayerController.enabled = false;
        _playerStorage.PlayerController.Animator.SetTrigger("Death");
        StartCoroutine(GoNormalDimension_Coroutine());
    }

    private IEnumerator GoNormalDimension_Coroutine()
    {
        yield return new WaitForSeconds(5f);
        VirtualCameraController.instance.VirtualCameraBeginAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttackColider"))
        {
            if (_playerStorage.HealthBarController.HeartsBox.childCount > 0)
            {
                TakeDamage();
            }
        }
    }
}
