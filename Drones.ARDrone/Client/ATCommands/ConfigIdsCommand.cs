using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.ATCommands
{
    /// <summary>
    /// Identiﬁers for the next AT*CONFIG command.
    /// 
    /// While in multiconﬁguration, you must send this command before every AT*CONFIG. The conﬁg
    /// will only be applied if the ids must match the current ids on the AR.Drone.
    /// </summary>
    public class ConfigIdsCommand : ATCommand
    {
        // @Properties
        public readonly string SessionId;
        public readonly string UserId;
        public readonly string ApplicationId;


        // @Public
        /// <summary> Constructor. </summary>
        ///
        /// <param name="sessionId">     Current session id. </param>
        /// <param name="userId">        Current user id. </param>
        /// <param name="applicationId"> Current application id. </param>
        public ConfigIdsCommand(string sessionId, string userId, string applicationId)
        {
            SessionId = sessionId;
            UserId = userId;
            ApplicationId = applicationId;
        }

        public override string ToString(int sequenceNumber)
        {
            return string.Format("AT*CONFIG_IDS={0},\"{1}\",\"{2}\",\"{3}\"\r",
                sequenceNumber,
                SessionId,
                UserId,
                ApplicationId);
        }
    }
}
