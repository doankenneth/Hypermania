using UnityEngine;
using Utils;

public class AudioConfig : ScriptableObject
{
    public int FirstMusicalBeat = 0;
    public float BPM = 60;
    public int FPS = 60;
    //Convert BPM to seconds per frame, then seconds to frames
    public int FramesPerQuarterNote =>
        Mathf.RoundToInt((60f / BPM) * FPS);

    public enum BeatSubdivision
    {
        WholeNote = 1,
        HalfNote = 2,
        QuarterNote = 4,
        EighthNote = 8,
        SixteenthNote = 16,
        Quartertriplet = 12
    }

    public int FramesPerBeat = 60;
    public int FirstBeatFrame = 0;
    public bool BeatWithinWindow(int frame, BeatSubdivision subdivision, int windowFrames)
    {
        float framesPerSubdivision = FramesPerBeat * (4f / (int)subdivision);
        int framesSinceFirstBeat = frame - FirstBeatFrame;
        // if we havent gotten to first beat yet, then no alignment so far
        if (framesSinceFirstBeat < 0)
        {
            return false;
        }

        int nearestBeatOffset = Mathf.RoundToInt(framesSinceFirstBeat / framesPerSubdivision) * Mathf.RoundToInt(framesPerSubdivision);
        
        int distanceToNearestBeat = Mathf.Abs(framesSinceFirstBeat - nearestBeatOffset)

        bool isInWindow = distanceToNearestBeat <= windowFrames;

        if (isInWindow)
        {
            Debug.Log($"Beat hit! Frame: {frame}, Nearest beat frame: {FirstBeatFrame + nearestBeatOffset}, Distance to beat: {distanceToNearestBeat} frames");
        }
        return isInWindow;
    }
}
