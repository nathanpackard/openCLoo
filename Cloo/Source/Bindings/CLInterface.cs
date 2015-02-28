using System;

namespace Cloo.Bindings
{
    /// <summary>
    /// Sets the underlying OpenCL library interface for Cloo.
    /// </summary>
    public static class CLInterface
    {
        static ICL10 _CL10;
        static ICL11 _CL11;
        static ICL12 _CL12;

        /// <summary>
        /// OpenCL 1.0.
        /// </summary>
        internal static ICL10 CL10
        {
            get
            {
                if (_CL10 == null) 
                    throw new EntryPointNotFoundException();

                    return _CL10;
            } 
        }
        /// <summary>
        /// OpenCL 1.1.
        /// </summary>
        internal static ICL11 CL11
        {
            get
            {
                if (_CL11 == null)
                    throw new EntryPointNotFoundException();

                return _CL11;
            }
        }
        /// <summary>
        /// OpenCL 1.2.
        /// </summary>
        internal static ICL12 CL12
        {
            get
            {
                if (_CL12 == null)
                    throw new EntryPointNotFoundException();

                return _CL12;
            }
        }

        /// <summary>
        /// Returns true if the CLInterface has been set.
        /// </summary>
        public static bool IsAvailable()
        {
            return _CL12 != null;
        }

        /// <summary>
        /// Sets the underlying OpenCL implementation.
        /// </summary>
        public static void SetInterface(ICL12 cl)
        {
            _CL12 = cl;
            _CL11 = cl;
            _CL10 = cl;
        }
    }
}
