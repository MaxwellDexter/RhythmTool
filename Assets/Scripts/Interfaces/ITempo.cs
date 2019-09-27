using System;
public interface ITempo
{
    bool IsInTime(double time);

    double GetCurrentBeatTime();
}
