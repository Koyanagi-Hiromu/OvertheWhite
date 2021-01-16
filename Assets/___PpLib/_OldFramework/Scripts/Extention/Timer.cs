using System;
using UnityEngine;

namespace SR.Nite
{
    public class Timer
    {
        public event Action onComplete;
        public float restSec;
        public bool complete { get; private set; }

        public Timer() : this(0) { }
        public Timer(float duration)
        {
            restSec = duration;
            if (IsAlreadyComplete())
            {
                End();
            }
        }

        public void Tick()
        {
            Tick(Time.deltaTime);
        }

        public void Tick(float duration)
        {
            if (IsRunning())
            {
                restSec -= duration;
                if (IsAlreadyComplete())
                {
                    End();
                }
            }
        }

        public bool IsRunning()
        {
            return !IsCompleted();
        }

        public bool IsCompleted()
        {
            return complete;
        }

        private bool IsAlreadyComplete()
        {
            return restSec <= 0;
        }

        public void End(bool onComplete = true)
        {
            restSec = 0;
            complete = true;
            if (onComplete && this.onComplete != null)
            {
                this.onComplete();
            }
        }
    }
}
