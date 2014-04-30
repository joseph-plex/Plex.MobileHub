namespace MobileHubClient.Logs.Behaviour
{
    public class DefaultAddBehaviour : IAddBehaviour
    {
        LogManager logManager;

        public DefaultAddBehaviour(LogManager logManager)
        {
            this.logManager = logManager;
        }

        public void Add(Log Log)
        {
            logManager.Logs.Add(Log);
        }
    }
}
