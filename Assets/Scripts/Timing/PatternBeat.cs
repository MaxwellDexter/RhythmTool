using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternBeat
{
    public int bar;

    public int subdivLocation;
    // can through each subdiv and see if they overlap with eachother with secs per beat
    // then go through the troublesome ones with a finer comb
    // recursion might be good

    [HideInInspector]
    public double width;
}
