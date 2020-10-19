using System;

namespace TMAS
{
    [Serializable]
    public abstract class TMASMsg
    {
        public int seq;
        public int cmd;
        public int err;
    }

}