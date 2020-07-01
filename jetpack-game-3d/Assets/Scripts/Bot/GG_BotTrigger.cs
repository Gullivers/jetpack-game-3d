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
        xPos = transform.position.x;
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
            Debug.Log("Girdii Front");
        }
        if (col.tag == "Platform")
        {
            PlatformPassed(col);

        }
        if (col.tag == "Water")
        {
            SetLastCheckpoint();
            Debug.Log("Girdii Water");
        }
        if (col.tag == "Finish")
        {
            particleControl.StopJetpackParticle();
            rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.EulerAngles(0, 0, 0);

            rb.useGravity = false;

            LoseEvent.Raise();

        }
    }

    public void SetLastCheckpoint()
    {
        if (!JetpackMove.MadedPass) { JetpackMove.LoseTry++; }

        JetpackMove.SetWaitTime();

        transform.rotation = Quaternion.EulerAngles(0, 0, 0);

        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        transform.position = LastCheckpoint;
        particleControl.StopJetpackParticle();

        StartCoroutine(WaitAndGoOn());



    }
    void PlatformPassed(Collider col)
    {
        JetpackMove.SetWaitTime();
        if (JetpackMove.MadedPass) { JetpackMove.SetLevel(); }
        particleControl.StopJetpackParticle();
        transform.position = new Vector3(transform.position.x, col.transform.position.y + 9.5f, transform.position.z);
        transform.rotation = Quaternion.EulerAngles(0, 0, 0);
        LastCheckpoint = transform.position;
        LastTransform = col.transform;
        StartCoroutine(WaitAndGoOn());
    }
    IEnumerator WaitAndGoOn()
    {
        yield return new WaitForSeconds(1f);
        BotTrajectory.CanMove = true;
    }

    public void Retrylevel()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        BotTrajectory.CanMove = false;
        DOTween.Kill("FallingAngle");
        transform.rotation = Quaternion.EulerAngles(0, 0, 0);
        JetpackMove.SetLevel();
        particleControl.StopJetpackParticle();



        transform.position = new Vector3(xPos, 3, 0);

        LastCheckpoint = transform.position;
        LastTransform = this.transform;

        StartCoroutine(WaitAndGoOn());
    }
}
