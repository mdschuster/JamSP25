using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Demo
{
    public static bool active;   // set by the menu before loading MainGame
    public static int seed;      // the run's seed; pillars read this in Start
}

public class SteerSource : MonoBehaviour
{
    public InputActionReference moveAction;
    public bool record;              // tick in the inspector to capture a run (editor only)
    public TextAsset[] recordings;   // drag saved runs here

    private List<float> values = new List<float>();
    private int index = 0;
    private bool playingBack = false;

    // Awake, not Start: every Awake in the scene runs before any Start,
    // so the seed is set before the pillars read it.
    void Awake()
    {
        //Demo.active = true;
        if (Demo.active && recordings.Length > 0)
        {
            TextAsset run = recordings[Random.Range(0, recordings.Length)];
            string[] lines = run.text.Split('\n');

            // First line is the seed
            Demo.seed = int.Parse(lines[0].Trim(), CultureInfo.InvariantCulture);

            // Remaining lines are steer values, one per physics tick
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.Length > 0)
                    values.Add(float.Parse(line, CultureInfo.InvariantCulture));
            }
            playingBack = true;
        }
        else
        {
            // Normal play: fresh seed, so pillars still feel random
            Demo.seed = Random.Range(int.MinValue, int.MaxValue);
        }
    }

    public float GetSteer()
    {
        if (playingBack)
        {
            return index < values.Count ? values[index++] : 0f;
        }

        float steer = moveAction.action.ReadValue<Vector2>().x;
        if (record) values.Add(steer);
        return steer;
    }

    public void Save()
    {
#if UNITY_EDITOR
        if (!record || playingBack) return;

        List<string> lines = new List<string>();
        lines.Add(Demo.seed.ToString(CultureInfo.InvariantCulture));
        foreach (float v in values)
            lines.Add(v.ToString("R", CultureInfo.InvariantCulture));

        string folder = Application.dataPath + "/Demo";
        System.IO.Directory.CreateDirectory(folder);
        string path = folder + "/run_" + System.DateTime.Now.ToString("HHmmss") + ".txt";
        System.IO.File.WriteAllLines(path, lines);
        Debug.Log("Saved demo run: " + path);
#endif
    }
}