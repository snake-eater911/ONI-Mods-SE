using System;
using UnityEngine;

namespace LogicGateDiode
{
    public class LogicGateDiode : LogicGate
    {
        protected virtual int UpdateState()
        {
            return 1;
        }
        protected virtual int Disabled()
        {
            return 0;
        }
        protected override int GetCustomValue(int val1, int val2)
        {
            if (val1 == 1)
            {
                return this.UpdateState();
            }
            return this.Disabled();
        }
    }
}