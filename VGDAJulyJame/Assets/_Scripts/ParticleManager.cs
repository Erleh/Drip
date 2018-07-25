using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem playerWaterParticles;

    [SerializeField]
    private ParticleSystem enemyWaterParticles;

    public void TurnOnPlayerWaterParticles()
    {
        playerWaterParticles.Play();
    }

    public void TurnOffPlayerWaterParticles()
    {
        playerWaterParticles.Stop();
    }

    public void TurnOnEnemyWaterParticles()
    {
        enemyWaterParticles.Play();
    }

    public void TurnOffEnemyWaterParticles()
    {
        enemyWaterParticles.Stop();
    }
}
