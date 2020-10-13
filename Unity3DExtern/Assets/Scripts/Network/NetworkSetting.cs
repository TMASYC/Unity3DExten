using UnityEngine;


namespace TMAS
{
    public class NetworkSettings
    {
        public virtual bool IsAirplaneMode()
        {
            bool isAirplaneMode = true;
            NetworkReachability reachability = UnityEngine.Application.internetReachability;

            if (reachability == NetworkReachability.ReachableViaCarrierDataNetwork || reachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                isAirplaneMode = false;
            }
            return isAirplaneMode;
        }
    }
}