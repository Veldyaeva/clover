namespace SewingProduction.Helpers
{
    public class StatusHelper
    {
        public static string GetStatusText(int status)
        {
            switch (status)
            {
                case 1: return "Предварительный";
                case 2: return "Актуальный";
                case 3: return "Архивный";
                case 4: return "Актуальный";
                default: return "Неизвестный статус";
            }
        }
    }
}
