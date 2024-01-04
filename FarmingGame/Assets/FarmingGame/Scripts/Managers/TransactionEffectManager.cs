using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransactionEffectManager : MonoBehaviour
{
    public static TransactionEffectManager Instance { get; set; }

    [Header("----- Elements -----")]
    [SerializeField] private ParticleSystem coinParticle;
    [SerializeField] private RectTransform coinRectTransform;


    [Header("----- Settings -----")]
    [SerializeField] private float moveSpeed;
    private int coinsAmount;
    private Camera _camera;




    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(this);

        
        
        
        _camera = Camera.main;
    }

    public void PlayCoinParticles(int amount)
    {
        Debug.Log("1");
        if (coinParticle.isPlaying) return;
        Debug.Log("2");
        ParticleSystem.Burst burst = coinParticle.emission.GetBurst(0);
        burst.count = amount;
        coinParticle.emission.SetBurst(0, burst);

        ParticleSystem.MainModule main = coinParticle.main;
        main.gravityModifier = 2;

        coinParticle.gameObject.SetActive(true);
        coinParticle.Play();
        coinsAmount = amount;
        StartCoroutine(PlayCoinParticlesCoroutine());
    }

    IEnumerator PlayCoinParticlesCoroutine()
    {
        yield return new WaitForSeconds(1);

        ParticleSystem.MainModule main = coinParticle.main;
        main.gravityModifier = 0;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[coinsAmount];
        coinParticle.GetParticles(particles);

        Vector3 direction = (coinRectTransform.position - _camera.transform.position).normalized;
        Vector3 targetPosition = _camera.transform.position + direction * Vector3.Distance(_camera.transform.position, coinParticle.transform.position);

        while (coinParticle.isPlaying)
        {
            coinParticle.GetParticles(particles);
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].remainingLifetime <= 0) continue;

                particles[i].position = Vector3.MoveTowards(particles[i].position, targetPosition, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(particles[i].position, targetPosition) < 0.1f)
                {
                    particles[i].position += Vector3.up * 100000;
                    CashManager.Instance.AddCoins(1);
                }
            }

            coinParticle.SetParticles(particles);

            yield return null;
        }

    }
}
