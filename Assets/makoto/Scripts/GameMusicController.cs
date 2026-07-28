using UnityEngine;

public class GameMusicController : MonoBehaviour
{
    [SerializeField] AudioClip damage_se;
    [SerializeField] AudioClip attack_se;
    [SerializeField] AudioClip keyitem_se;
    [SerializeField] AudioSource audioSource01;
    [SerializeField] AudioSource audioSource02;
    [SerializeField] AudioSource audioSource03;

    [SerializeField] AudioClip walk_se;

    bool is_walking = false;

    public void PlayDamageSE()
    {
        audioSource01.PlayOneShot(damage_se);
    }

    public void PlayAttackSE()
    {
        audioSource02.PlayOneShot(attack_se);
    }

    public void PlayItemGet()
    {
        audioSource02.PlayOneShot(keyitem_se);
    }

    public void PlayWalkSe(bool flag)
    {
        if (!flag)
        {
            audioSource03.Stop();
            is_walking = false;
            return;
        }

        if (is_walking && flag)
        {
            return;
        }
        else
        {
            audioSource03.Play();

            is_walking = true;
        }
    }
}
