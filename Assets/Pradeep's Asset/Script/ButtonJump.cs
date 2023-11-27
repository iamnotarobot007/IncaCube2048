using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonJump : MonoBehaviour
{

    void Start()
    {
        this.gameObject.transform.
            DOShakePosition(duration: 1.5f, strength: new Vector3(0f, 10f, 0f), vibrato: 4, randomness: 90f)
            .SetLoops(-1, LoopType.Restart);
        //.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360);
        //.DOShakePosition(duration: 0.5f, strength: new Vector3(10f, 10f, 0f), vibrato: 10, randomness: 90f).SetLoops(-1, LoopType.Restart);

    }


}