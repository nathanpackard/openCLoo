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
        static ICL20 _CL20;

        /// <summary>
        /// OpenCL 1.0.
        /// </summary>
        internal static ICL10 CL10
        {
            get
            {
                if (_CL10 == null) 
                    throw new NotImplementedException();

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
                    throw new NotImplementedException();

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
                    throw new NotImplementedException();

                return _CL12;
            }
        }
        /// <summary>
        /// OpenCL 2.0.
        /// </summary>
        internal static ICL20 CL20
        {
            get
            {
                if (_CL20 == null)
                    throw new NotImplementedException();

                return _CL20;
            }
        }

        /// <summary>
        /// Returns true if the CLInterface has been set.
        /// </summary>
        public static bool IsAvailable()
        {
            return _CL10 != null;
        }

        /// <summary>
        /// Returns true if the CLInterface has been set.
        /// </summary>
        public static bool IsOpenCL11Available()
        {
            return _CL11 != null;
        }

        /// <summary>
        /// Returns true if the CLInterface has been set.
        /// </summary>
        public static bool IsOpenCL12Available()
        {
            return _CL12 != null;
        }

        /// <summary>
        /// Returns true if the CLInterface has been set.
        /// </summary>
        public static bool IsOpenCL20Available()
        {
            return _CL20 != null;
        }

        /// <summary>
        /// Sets the underlying OpenCL implementation.
        /// </summary>
        public static void SetInterface(ICL20 cl)
        {
            _CL20 = cl;
            _CL12 = cl;
            _CL11 = cl;
            _CL10 = cl;
        }

        /// <summary>
        /// Sets the underlying OpenCL implementation.
        /// </summary>
        public static void SetInterface(ICL12 cl)
        {
            _CL20 = null;
            _CL12 = cl;
            _CL11 = cl;
            _CL10 = cl;
        }

        /// <summary>
        /// Sets the underlying OpenCL implementation.
        /// </summary>
        public static void SetInterface(ICL11 cl)
        {
            _CL20 = null;
            _CL12 = null;
            _CL11 = cl;
            _CL10 = cl;
        }

        /// <summary>
        /// Sets the underlying OpenCL implementation.
        /// </summary>
        public static void SetInterface(ICL10 cl)
        {
            _CL20 = null;
            _CL12 = null;
            _CL11 = null;
            _CL10 = cl;
        }
    }
}
