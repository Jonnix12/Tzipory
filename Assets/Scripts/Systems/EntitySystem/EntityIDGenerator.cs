namespace Tzipory.EntitySystem
{
    public static class EntityIDGenerator
    {
        private static int _count = 0;

        public static int GetInstanceID()
        {
            int temp = _count;
            _count++;
            return temp;
        }
    }
}