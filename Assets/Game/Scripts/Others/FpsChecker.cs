using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsChecker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    
    public int FramesPerSec { get; protected set; }
 
    [SerializeField] private float frequency = 0.5f;
    
 
    private void Start()
    {
        textMeshProUGUI.text = "";
        StartCoroutine(FPS());
    }
 
    private IEnumerator FPS()
    {
        for (; ; )
        {
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
 
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;
 
            FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
            textMeshProUGUI.text = "FPS: " + FramesPerSec.ToString();
        }
    }
}
