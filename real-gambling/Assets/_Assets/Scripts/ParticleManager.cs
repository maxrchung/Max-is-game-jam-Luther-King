using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem[] pSystems;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void BurstParticles(int num)
    {
        pSystems[num].Play();
    }
}
