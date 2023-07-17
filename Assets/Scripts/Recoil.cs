using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public float recoilAmount = 1.0f;
    public float recoilOverTime = 0.1f;
    private float currentRotation = 0f;

    public void StartRecoil()
    {
        StartCoroutine(RecoilCoroutine());
    }

    private IEnumerator RecoilCoroutine()
    {
        while (currentRotation < recoilAmount)
        {
            float recoil = Mathf.Lerp(0, recoilAmount, recoilOverTime);
            currentRotation += recoil;
            transform.localEulerAngles = new Vector3(-currentRotation, 0, 0);
            yield return null;
        }
        while (currentRotation > 0)
        {
            currentRotation -= recoilAmount * Time.deltaTime;
            transform.localEulerAngles = new Vector3(-currentRotation, 0, 0);
            yield return null;
        }
    }
}
