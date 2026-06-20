using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (SkillManager.Instance != null && SkillManager.Instance.CanUseShield())
            {
                SkillManager.Instance.UseShield();
                Destroy(gameObject);
                return;
            }

            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX(SfxType.Hit);

            GameManager.Instance.PlayerDead();
        }
    }
}