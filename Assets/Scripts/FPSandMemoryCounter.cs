using System.Text;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class FPSandMemoryCounter : MonoBehaviour
{
    string statsText;
    ProfilerRecorder systemUsedMemoryRecorder;
    public Text fpsDisplay;
    public Text averageFPSDisplay;
    int framesPassed = 0;
    float fpsTotal = 0f;

    void OnEnable()
    {
        systemUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
    }

    void OnDisable()
    {
        systemUsedMemoryRecorder.Dispose();
    }


    void Update()
    {
        float fps = (int)1 / Time.unscaledDeltaTime;
        fpsTotal += fps;
        framesPassed++;

        var sb = new StringBuilder(500);
        if (systemUsedMemoryRecorder.Valid)
        {
            sb.AppendLine($"System Used Memory: {systemUsedMemoryRecorder.LastValue / 1024 / 1024}MB");
        }
        sb.AppendLine($"FPS Average: { Mathf.RoundToInt(fpsTotal / framesPassed) }");
        sb.AppendLine($"FPS: { Mathf.RoundToInt(fps) }");

        statsText = sb.ToString();
    }

    void OnGUI()
    {
        GUI.TextArea(new Rect(10, 30, 250, 50), statsText);
    }
}