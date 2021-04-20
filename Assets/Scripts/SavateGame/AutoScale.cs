using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScale : MonoBehaviour
{
    public float delay;

    public Vector3 defaultScale;
    public Vector3 punchScale = new Vector3(0.5f,0.5f,0.5f);
    public bool hideOnStart = true;

    private void Awake()
    {
        defaultScale = transform.localScale;
        if(hideOnStart) transform.localScale = Vector3.zero;
    }


    private void OnEnable()
    {
      
        StartCoroutine(DelayAndTween(delay));
    }

    IEnumerator DelayAndTween(float delay)
    {
        yield return new WaitForSeconds(delay);

        transform.localScale = defaultScale;

        iTween.ScaleFrom(gameObject, iTween.Hash("scale", Vector3.zero, "time", 0.5f, "easeType", iTween.EaseType.easeOutBounce));
    }

    public void ScalePunch()
    {
        iTween.PunchScale(gameObject, punchScale, 0.5f);
    }

}
