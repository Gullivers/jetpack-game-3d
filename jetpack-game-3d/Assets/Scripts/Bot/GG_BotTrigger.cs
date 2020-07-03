using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GG_BotTrigger : MonoBehaviour
{
    public Vector3 LastCheckpoint;

    [SerializeField] GG_BotTrajectory BotTrajectory;
    [SerializeField] GG_BotJetpackMovement JetpackMove;

    GG_ParticleControl particleControl;
    Rigidbody rb;
    public Transform LastTransform;
    [SerializeField] GG_VoidEvent LoseEvent;
    float xPos;

    private void Start()
    {
        xPos = transform.localPosition.x;
        rb = GetComponent<Rigidbody>();
        LastCheckpoint = transform.position;
        particleControl = GetComponent<GG_ParticleControl>();
    }
    private void OnTriggerEnter(Collider col)
    {
        #region Rigidbody

        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        #endregion


        if (col.tag == "PlatformFront")
        {
            SetLastCheckpoint();
          
        }
        if (col.tag == "Platform")
        {
            PlatformPassed(col);
            DOTween.Kill("Softlaunch" + this.transform.name); //Killing Tweens
            particleControl.StartLangingPart(); //Pof particle
          
        }
        if (col.tag == "Water")
        {
            SetLastCheckpoint();
          
        }
        if (col.tag == "Finish")
        {
            particleControl.StopJetpackParticle();
            rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.EulerAngles(0, 0, 0);
            DOTween.Kill("Softlaunch" + this.transform.name);
            DOTween.Kill("FallingAngle"+transform.name);
            particleControl.StartLangingPart(); //Pof particle
            transform.position = new Vector3(transform.position.x, col.transform.position.y + 9.5f, transform.position.z);//Set position to top the Platform
            rb.useGravity = false;
            //Raising Lose Event
            LoseEvent.Raise();

        }
    }

    public void SetLastCheckpoint()
    {
         DOTween.Kill("FallingAngle"+transform.name);
        if (!JetpackMove.MadedPass) { JetpackMove.LoseTry++; }

        JetpackMove.SetWaitTime(); //Choose random wait time again

        transform.rotation = Quaternion.EulerAngles(0, 0, 0);

        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        transform.position = LastCheckpoint;
        particleControl.StopJetpackParticle();

        StartCoroutine(WaitAndGoOn());



    }
    void PlatformPassed(Collider col)
    {   DOTween.Kill("FallingAngle"+transform.name);
        JetpackMove.SetWaitTime();//Choose random wait time again


        if (JetpackMove.MadedPass) { JetpackMove.SetLevel(); } 
        particleControl.StopJetpackParticle();

        transform.position = new Vector3(transform.position.x, col.transform.position.y + 9.5f, transform.position.z);//Set position to top the Platform
        transform.rotation = Quaternion.EulerAngles(0, 0, 0);

        LastCheckpoint = transform.position;
        LastTransform = col.transform; // İndiği platformu sonraki kalkışlarda dikkate almaması için
        StartCoroutine(WaitAndGoOn());
    }
    IEnumerator WaitAndGoOn()
    {
        yield return new WaitForSeconds(1f);
        BotTrajectory.CanMove = true;
    }

    public void Retrylevel()
    {
        DOTween.Kill("FallingAngle"+transform.name);
        Destroy(this.gameObject);
        
    }
}
