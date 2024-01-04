namespace Miniprojekt_API
{
    public static class Utility
    {
        // Generic method that checks if a generic variable is null or not and returns a bool.
        public static bool DoesEntityExist<T>(T entity)
        {
            if (entity == null) return false;
            else return true;
        }
    }
}
