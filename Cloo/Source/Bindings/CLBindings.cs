namespace Cloo.Bindings
{
    public static class CLBindings
    {
        public static ICL10 cl10 { get; private set; }
        public static ICL11 cl11 { get; private set; }
        public static ICL12 cl12 { get; private set; }

        public static void SetBinding(ICL10 cl)
        {
            cl10 = cl;
        }

        public static void SetBinding(ICL11 cl)
        {
            cl11 = cl;
            cl10 = cl;
        }

        public static void SetBinding(ICL12 cl)
        {
            cl12 = cl;
            cl11 = cl;
            cl10 = cl;
        }
    }
}
