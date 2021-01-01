namespace HubServiceInterfaces
{
    public static class Strings
    {
        public static string HubUrl => "https://localhost:44352/print/hub";

        public static class Events
        {
            public static string TimeSent => nameof(IClock.ShowTime);
        }
    }
}