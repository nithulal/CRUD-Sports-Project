using CRUDProject.Interfaces;

namespace CRUDProject
{
    public static class Error
    {
        public static void TrapError(this ILogger logging, Exception exception, string customMessage = "")
        {
            TrapError(logging, exception, customMessage);
        }
    }
}
