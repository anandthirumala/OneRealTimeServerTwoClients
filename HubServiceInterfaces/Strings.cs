namespace HubServiceInterfaces
{
    public static class Strings
    {
        public static string HubUrl { get { return "https://localhost:5001/hubs/clock"; }}

        public static class Events
        {
            public static string TimeSent { get { return "SendTimeToClients"; }}
        }
    }
}