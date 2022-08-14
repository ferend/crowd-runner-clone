using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    void Start()
    {
  AddMovementTween();
    }

    private void AddMovementTween()
    {
        transform.DOMoveY(transform.position.y + 0.5F, 1).SetLoops(-1, LoopType.Yoyo).SetId(2);
    }

    public void KillTween()
    {
        DOTween.Kill(2);
    }

    public void EjectPlayerBall(float forceMultiplier, Transform hitPoint)
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        this.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward.normalized * 5 * forceMultiplier  ,
            hitPoint.position, ForceMode.Impulse);
    }
}
