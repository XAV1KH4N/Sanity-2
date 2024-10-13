using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class AppModule : MonoBehaviour // Maybe split appModule into multiple interfaces .. ?
{
    public void runTimedEvent(int seconds, Action callback)
    {
        TimedWorker worker = new TimedWorker(seconds, callback);
        worker.run();
    }
}

interface Runnable
{
    public void run();
}

class TimedWorker : MonoBehaviour, Runnable
{
    private int seconds;
    private Action callback;

    public TimedWorker(int seconds, Action callback)
    {
        this.seconds = seconds;
        this.callback = callback;
    }

    public void run()
    {
        Thread thread = new Thread(delegate ()
        {
            Thread.Sleep(seconds * 1000);
            callback();
        });
        thread.Start();
    }
}
