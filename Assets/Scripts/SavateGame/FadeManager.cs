using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;
    [Tooltip("Reference to a UI image for fading in")]
    [SerializeField]
    private UnityEngine.UI.Image FadeImage;

    [Tooltip("Length of the fade")]
    [SerializeField]
    private float FadeTimer = 2f;

    [Tooltip("Delay until fades beghins")]
    [SerializeField]
    private float FadeInDelay = 1f;

    [Tooltip("Color to fade to")]
    [SerializeField]
    private Color EndColor = Color.white;

    [Tooltip("Color to fade from")]
    [SerializeField]
    private Color StartColor = Color.clear;

    private void Awake()
    {
        //SINGLETON
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        FadeIn();
    }

    public void updateColor(float val)
    {
        FadeImage.color = ((1f - val) * StartColor) + (val * EndColor);
    }

    public void FadeIn()
    {
        // Commented out unity cross fade since this seems to 
        // have bugs when working on transparent images
        // FadeImage.CrossFadeColor(FadeColor, FadeTimer, false, true);
        iTween.ValueTo(this.gameObject, iTween.Hash("from", 0f, "to", 3f, "delay", FadeInDelay, "time", FadeTimer, "onupdate", "updateColor"));
    }

    public void FadeOut()
    {
        // Commented out unity cross fade since this seems to 
        // have bugs when working on transparent images
        // FadeImage.CrossFadeColor(FadeColor, FadeTimer, false, true);
        iTween.ValueTo(this.gameObject, iTween.Hash("from", 3f, "to", 0f, "delay", FadeInDelay, "time", FadeTimer, "onupdate", "updateColor"));
    }
}
