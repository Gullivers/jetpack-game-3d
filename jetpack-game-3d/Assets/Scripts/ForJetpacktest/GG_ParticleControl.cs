using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_ParticleControl : MonoBehaviour
{
    [SerializeField] ParticleSystem Smoke1, Smoke2, Fire1, Fire2;
    [SerializeField] ParticleSystem LandingParticle;
    // [SerializeField] ParticleSystem SoftlaunchParticle1, SoftlaunchParticle2;

    public void StopJetpackParticle()
    {
        Smoke1.Stop();
        Smoke2.Stop();
        Fire1.Stop();
        Fire2.Stop();
    }
    public void StartJetpackParticle()
    {
        Smoke1.Play();
        Smoke2.Play();
        Fire1.Play();
        Fire2.Play();
    }
    public void StartLangingPart()
    {
        LandingParticle.Play();
    }
    

}
