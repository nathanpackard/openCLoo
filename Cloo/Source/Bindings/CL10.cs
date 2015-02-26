#region License

/*

Copyright (c) 2009 - 2013 Fatjon Sakiqi

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

*/

#endregion

namespace Cloo.Bindings
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    /// <summary>
    /// Represents the platform to use for OpenCL bindings. This is a workaround for poor Dllmap support within the Unity engine.
    /// </summary>
    public enum BindingPlatform
    {
        /// <summary>
        /// Use Mac bindings.
        /// </summary>
        Mac = 0,
        /// <summary>
        /// Use Windows bindings.
        /// </summary>
        Windows = 1,       
        /// <summary>
        /// Use Unix bindings.
        /// </summary>
        Unix = 2
    }
    /// <summary>
    /// Contains bindings to the OpenCL 1.0 functions.
    /// </summary>
    /// <remarks> See the OpenCL specification for documentation regarding these functions. </remarks>
    public class CL10
    {
        /// <summary>
        /// The name of the library (on Windows) that contains the available OpenCL function points.
        /// </summary>
        protected const string libNameWindows = "OpenCL.dll";
        /// <summary>
        /// The name of the library (on Mac) that contains the available OpenCL function points.
        /// </summary>
        protected const string libNameMac = "/System/Library/Frameworks/OpenCL.framework/Versions/Current/OpenCL";
        /// <summary>
        /// The name of the library (on UNIX) that contains the available OpenCL function points.
        /// </summary>
        protected const string libNameUnix = "libOpenCL.so";
        /// <summary>
        /// Get the currently used binding platform.
        /// </summary> 
        protected static readonly BindingPlatform bindingPlatform;

        static CL10()
        {
            bindingPlatform = GetBindingPlatform();
        }

        static BindingPlatform GetBindingPlatform()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    return IsRunningOnMac() ? BindingPlatform.Mac : BindingPlatform.Unix;
                case PlatformID.MacOSX:
                    return BindingPlatform.Mac;

                default:
                    return BindingPlatform.Windows;
            }
        }

        [DllImport("libc")]
        extern static int uname(IntPtr buf);

        static bool IsRunningOnMac()
        {
            var buf = IntPtr.Zero;
            try
            {
                buf = Marshal.AllocHGlobal(8192);
                // This is a hacktastic way of getting sysname from uname ()
                if (uname(buf) == 0)
                {
                    var os = Marshal.PtrToStringAnsi(buf);
                    if (os == "Darwin")
                        return true;
                }
            }
            catch {}
            finally
            {
                if (buf != IntPtr.Zero)
                    Marshal.FreeHGlobal(buf);
            }
            return false;
        }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetPlatformIDs(
            Int32 num_entries,
            [Out, MarshalAs(UnmanagedType.LPArray)] CLPlatformHandle[] platforms,
            out Int32 num_platforms)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetPlatformIDs_Mac(num_entries, platforms, out num_platforms);
                case BindingPlatform.Windows:
                    return clGetPlatformIDs_Windows(num_entries, platforms, out num_platforms);               
                default:
                    return clGetPlatformIDs_Unix(num_entries, platforms, out num_platforms);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetPlatformIDs")]
        extern static ComputeErrorCode clGetPlatformIDs_Mac(
            Int32 num_entries,
            [Out, MarshalAs(UnmanagedType.LPArray)] CLPlatformHandle[] platforms,
            out Int32 num_platforms);
        [DllImport(libNameWindows, EntryPoint = "clGetPlatformIDs")]
        extern static ComputeErrorCode clGetPlatformIDs_Windows(
            Int32 num_entries,
            [Out, MarshalAs(UnmanagedType.LPArray)] CLPlatformHandle[] platforms,
            out Int32 num_platforms);
        [DllImport(libNameUnix, EntryPoint = "clGetPlatformIDs")]
        extern static ComputeErrorCode clGetPlatformIDs_Unix(
            Int32 num_entries,
            [Out, MarshalAs(UnmanagedType.LPArray)] CLPlatformHandle[] platforms,
            out Int32 num_platforms);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetPlatformInfo(
            CLPlatformHandle platform,
            ComputePlatformInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetPlatformInfo_Mac(platform, param_name, param_value_size, param_value, out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetPlatformInfo_Windows(platform, param_name, param_value_size, param_value, out param_value_size_ret);
                default:
                    return clGetPlatformInfo_Unix(platform, param_name, param_value_size, param_value, out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetPlatformInfo")]
        extern static ComputeErrorCode clGetPlatformInfo_Mac(
            CLPlatformHandle platform,
            ComputePlatformInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetPlatformInfo")]
        extern static ComputeErrorCode clGetPlatformInfo_Windows(
            CLPlatformHandle platform,
            ComputePlatformInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetPlatformInfo")]
        extern static ComputeErrorCode clGetPlatformInfo_Unix(
            CLPlatformHandle platform,
            ComputePlatformInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetDeviceIDs(
            CLPlatformHandle platform,
            ComputeDeviceTypes device_type,
            Int32 num_entries,
            [Out, MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices,
            out Int32 num_devices)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetDeviceIDs_Mac(platform, device_type, num_entries, devices, out num_devices);
                case BindingPlatform.Windows:
                    return clGetDeviceIDs_Windows(platform, device_type, num_entries, devices, out num_devices);
                default:
                    return clGetDeviceIDs_Unix(platform, device_type, num_entries, devices, out num_devices);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetDeviceIDs")]       
        extern static ComputeErrorCode clGetDeviceIDs_Mac(
            CLPlatformHandle platform,
            ComputeDeviceTypes device_type,
            Int32 num_entries,
            [Out, MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices,
            out Int32 num_devices);
        [DllImport(libNameWindows, EntryPoint = "clGetDeviceIDs")]
        extern static ComputeErrorCode clGetDeviceIDs_Windows(
            CLPlatformHandle platform,
            ComputeDeviceTypes device_type,
            Int32 num_entries,
            [Out, MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices,
            out Int32 num_devices);
        [DllImport(libNameUnix, EntryPoint = "clGetDeviceIDs")]
        extern static ComputeErrorCode clGetDeviceIDs_Unix(
            CLPlatformHandle platform,
            ComputeDeviceTypes device_type,
            Int32 num_entries,
            [Out, MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices,
            out Int32 num_devices);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetDeviceInfo(
            CLDeviceHandle device,
            ComputeDeviceInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetDeviceInfo_Mac(device, param_name, param_value_size, param_value, out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetDeviceInfo_Windows(device, param_name, param_value_size, param_value, out param_value_size_ret);
                default:
                    return clGetDeviceInfo_Unix(device, param_name, param_value_size, param_value, out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetDeviceInfo")]
        extern static ComputeErrorCode clGetDeviceInfo_Mac(
            CLDeviceHandle device,
            ComputeDeviceInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetDeviceInfo")]
        extern static ComputeErrorCode clGetDeviceInfo_Windows(
            CLDeviceHandle device,
            ComputeDeviceInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetDeviceInfo")]
        extern static ComputeErrorCode clGetDeviceInfo_Unix(
            CLDeviceHandle device,
            ComputeDeviceInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLContextHandle CreateContext(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices,
            ComputeContextNotifier pfn_notify,
            IntPtr user_data,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateContext_Mac(properties, num_devices, devices, pfn_notify, user_data, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateContext_Windows(properties, num_devices, devices, pfn_notify, user_data, out errcode_ret);           
                default:
                    return clCreateContext_Unix(properties, num_devices, devices, pfn_notify, user_data, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateContext")]
        extern static CLContextHandle clCreateContext_Mac(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices,
            ComputeContextNotifier pfn_notify,
            IntPtr user_data,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateContext")]
        extern static CLContextHandle clCreateContext_Windows(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices,
            ComputeContextNotifier pfn_notify,
            IntPtr user_data,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateContext")]
        extern static CLContextHandle clCreateContext_Unix(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices,
            ComputeContextNotifier pfn_notify,
            IntPtr user_data,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLContextHandle CreateContextFromType(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties,
            ComputeDeviceTypes device_type,
            ComputeContextNotifier pfn_notify,
            IntPtr user_data,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateContextFromType_Mac(properties, device_type, pfn_notify, user_data, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateContextFromType_Windows(properties, device_type, pfn_notify, user_data, out errcode_ret);
                default:
                    return clCreateContextFromType_Unix(properties, device_type, pfn_notify, user_data, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateContextFromType")]
        extern static CLContextHandle clCreateContextFromType_Mac(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties,
            ComputeDeviceTypes device_type,
            ComputeContextNotifier pfn_notify,
            IntPtr user_data,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateContextFromType")]
        extern static CLContextHandle clCreateContextFromType_Windows(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties,
            ComputeDeviceTypes device_type,
            ComputeContextNotifier pfn_notify,
            IntPtr user_data,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateContextFromType")]
        extern static CLContextHandle clCreateContextFromType_Unix(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties,
            ComputeDeviceTypes device_type,
            ComputeContextNotifier pfn_notify,
            IntPtr user_data,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode RetainContext(
            CLContextHandle context)
        {
            {
                switch (bindingPlatform)
                {
                    case BindingPlatform.Mac:
                        return clRetainContext_Mac(context);
                    case BindingPlatform.Windows:
                        return clRetainContext_Windows(context);
                    default:
                        return clRetainContext_Unix(context);
                }
            }
        }
        [DllImport(libNameMac, EntryPoint = "clRetainContext")]
        extern static ComputeErrorCode clRetainContext_Mac(
            CLContextHandle context);
        [DllImport(libNameWindows, EntryPoint = "clRetainContext")]
        extern static ComputeErrorCode clRetainContext_Windows(
            CLContextHandle context);
        [DllImport(libNameUnix, EntryPoint = "clRetainContext")]
        extern static ComputeErrorCode clRetainContext_Unix(
            CLContextHandle context);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode ReleaseContext(
            CLContextHandle context)
        {
            {
                switch (bindingPlatform)
                {
                    case BindingPlatform.Mac:
                        return clReleaseContext_Mac(context);
                    case BindingPlatform.Windows:
                        return clReleaseContext_Windows(context);
                    default:
                        return clReleaseContext_Unix(context);
                }
            }
        }
        [DllImport(libNameMac, EntryPoint = "clReleaseContext")]
        static extern ComputeErrorCode clReleaseContext_Mac(
            CLContextHandle context);
        [DllImport(libNameWindows, EntryPoint = "clReleaseContext")]
        extern static ComputeErrorCode clReleaseContext_Windows(
            CLContextHandle context);
        [DllImport(libNameUnix, EntryPoint = "clReleaseContext")]
        extern static ComputeErrorCode clReleaseContext_Unix(
            CLContextHandle context);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetContextInfo(
            CLContextHandle context,
            ComputeContextInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetContextInfo_Mac(context, param_name, param_value_size, param_value, out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetContextInfo_Windows(context, param_name, param_value_size, param_value, out param_value_size_ret);;
                default:
                    return clGetContextInfo_Unix(context, param_name, param_value_size, param_value, out param_value_size_ret);;
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetContextInfo")]
        extern static ComputeErrorCode clGetContextInfo_Mac(
            CLContextHandle context,
            ComputeContextInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetContextInfo")]
        extern static ComputeErrorCode clGetContextInfo_Windows(
            CLContextHandle context,
            ComputeContextInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetContextInfo")]
        extern static ComputeErrorCode clGetContextInfo_Unix(
            CLContextHandle context,
            ComputeContextInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLCommandQueueHandle CreateCommandQueue(
            CLContextHandle context,
            CLDeviceHandle device,
            ComputeCommandQueueFlags properties,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateCommandQueue_Mac(context, device, properties, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateCommandQueue_Windows(context, device, properties, out errcode_ret);
                default:
                    return clCreateCommandQueue_Unix(context, device, properties, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateCommandQueue")]
        extern static CLCommandQueueHandle clCreateCommandQueue_Mac(
            CLContextHandle context,
            CLDeviceHandle device,
            ComputeCommandQueueFlags properties,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateCommandQueue")]
        extern static CLCommandQueueHandle clCreateCommandQueue_Windows(
            CLContextHandle context,
            CLDeviceHandle device,
            ComputeCommandQueueFlags properties,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateCommandQueue")]
        extern static CLCommandQueueHandle clCreateCommandQueue_Unix(
            CLContextHandle context,
            CLDeviceHandle device,
            ComputeCommandQueueFlags properties,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode RetainCommandQueue(
            CLCommandQueueHandle command_queue)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clRetainCommandQueue_Mac(command_queue);
                case BindingPlatform.Windows:
                    return clRetainCommandQueue_Windows(command_queue);
                default:
                    return clRetainCommandQueue_Unix(command_queue);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clRetainCommandQueue")]
        extern static ComputeErrorCode clRetainCommandQueue_Mac(
            CLCommandQueueHandle command_queue);
        [DllImport(libNameWindows, EntryPoint = "clRetainCommandQueue")]
        extern static ComputeErrorCode clRetainCommandQueue_Windows(
            CLCommandQueueHandle command_queue);
        [DllImport(libNameUnix, EntryPoint = "clRetainCommandQueue")]
        extern static ComputeErrorCode clRetainCommandQueue_Unix(
            CLCommandQueueHandle command_queue);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode ReleaseCommandQueue(
            CLCommandQueueHandle command_queue)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clReleaseCommandQueue_Mac(command_queue);
                case BindingPlatform.Windows:
                    return clReleaseCommandQueue_Windows(command_queue);
                default:
                    return clReleaseCommandQueue_Unix(command_queue);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clReleaseCommandQueue")]
        extern static ComputeErrorCode clReleaseCommandQueue_Mac(
            CLCommandQueueHandle command_queue);
        [DllImport(libNameWindows, EntryPoint = "clReleaseCommandQueue")]
        extern static ComputeErrorCode clReleaseCommandQueue_Windows(
            CLCommandQueueHandle command_queue);
        [DllImport(libNameUnix, EntryPoint = "clReleaseCommandQueue")]
        extern static ComputeErrorCode clReleaseCommandQueue_Unix(
            CLCommandQueueHandle command_queue);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetCommandQueueInfo(
            CLCommandQueueHandle command_queue,
            ComputeCommandQueueInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetCommandQueueInfo_Mac(command_queue, param_name, param_value_size, param_value, out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetCommandQueueInfo_Windows(command_queue, param_name, param_value_size, param_value, out param_value_size_ret);
                default:
                    return clGetCommandQueueInfo_Unix(command_queue, param_name, param_value_size, param_value, out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetCommandQueueInfo")]
        extern static ComputeErrorCode clGetCommandQueueInfo_Mac(
            CLCommandQueueHandle command_queue,
            ComputeCommandQueueInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetCommandQueueInfo")]
        extern static ComputeErrorCode clGetCommandQueueInfo_Windows(
            CLCommandQueueHandle command_queue,
            ComputeCommandQueueInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetCommandQueueInfo")]
        extern static ComputeErrorCode clGetCommandQueueInfo_Unix(
            CLCommandQueueHandle command_queue,
            ComputeCommandQueueInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode SetCommandQueueProperty(
            CLCommandQueueHandle command_queue,
            ComputeCommandQueueFlags properties,
            [MarshalAs(UnmanagedType.Bool)] bool enable,
            out ComputeCommandQueueFlags old_properties)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clSetCommandQueueProperty_Mac(command_queue, properties, enable, out old_properties);
                case BindingPlatform.Windows:
                    return clSetCommandQueueProperty_Windows(command_queue, properties, enable, out old_properties);
                default:
                    return clSetCommandQueueProperty_Unix(command_queue, properties, enable, out old_properties);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clSetCommandQueueProperty")]
        extern static ComputeErrorCode clSetCommandQueueProperty_Mac(
            CLCommandQueueHandle command_queue,
            ComputeCommandQueueFlags properties,
            [MarshalAs(UnmanagedType.Bool)] bool enable,
            out ComputeCommandQueueFlags old_properties);
        [DllImport(libNameWindows, EntryPoint = "clSetCommandQueueProperty")]
        extern static ComputeErrorCode clSetCommandQueueProperty_Windows(
            CLCommandQueueHandle command_queue,
            ComputeCommandQueueFlags properties,
            [MarshalAs(UnmanagedType.Bool)] bool enable,
            out ComputeCommandQueueFlags old_properties);
        [DllImport(libNameUnix, EntryPoint = "clSetCommandQueueProperty")]
        extern static ComputeErrorCode clSetCommandQueueProperty_Unix(
            CLCommandQueueHandle command_queue,
            ComputeCommandQueueFlags properties,
            [MarshalAs(UnmanagedType.Bool)] bool enable,
            out ComputeCommandQueueFlags old_properties);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLMemoryHandle CreateBuffer(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            IntPtr size,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateBuffer_Mac(context, flags, size, host_ptr, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateBuffer_Windows(context, flags, size, host_ptr, out errcode_ret);
                default:
                    return clCreateBuffer_Unix(context, flags, size, host_ptr, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateBuffer")]
        extern static CLMemoryHandle clCreateBuffer_Mac(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            IntPtr size,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateBuffer")]
        extern static CLMemoryHandle clCreateBuffer_Windows(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            IntPtr size,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateBuffer")]
        extern static CLMemoryHandle clCreateBuffer_Unix(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            IntPtr size,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLMemoryHandle CreateImage2D(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            ref ComputeImageFormat image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_row_pitch,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateImage2D_Mac(context, flags, ref image_format, image_width, image_height, image_row_pitch,
                                               host_ptr, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateImage2D_Windows(context, flags, ref image_format, image_width, image_height,
                                                   image_row_pitch, host_ptr, out errcode_ret);
                default:
                    return clCreateImage2D_Unix(context, flags, ref image_format, image_width, image_height, image_row_pitch,
                                               host_ptr, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateImage2D")]
        extern static CLMemoryHandle clCreateImage2D_Mac(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            ref ComputeImageFormat image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_row_pitch,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateImage2D")]
        extern static CLMemoryHandle clCreateImage2D_Windows(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            ref ComputeImageFormat image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_row_pitch,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateImage2D")]
        extern static CLMemoryHandle clCreateImage2D_Unix(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            ref ComputeImageFormat image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_row_pitch,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLMemoryHandle CreateImage3D(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            ref ComputeImageFormat image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_depth,
            IntPtr image_row_pitch,
            IntPtr image_slice_pitch,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateImage3D_Mac(context, flags, ref image_format, image_width, image_height, image_depth,
                                               image_row_pitch, image_slice_pitch, host_ptr, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateImage3D_Windows(context, flags, ref image_format, image_width, image_height, image_depth,
                                               image_row_pitch, image_slice_pitch, host_ptr, out errcode_ret);
                default:
                    return clCreateImage3D_Unix(context, flags, ref image_format, image_width, image_height, image_depth,
                                               image_row_pitch, image_slice_pitch, host_ptr, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateImage3D")]
        extern static CLMemoryHandle clCreateImage3D_Mac(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            ref ComputeImageFormat image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_depth,
            IntPtr image_row_pitch,
            IntPtr image_slice_pitch,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateImage3D")]
        extern static CLMemoryHandle clCreateImage3D_Windows(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            ref ComputeImageFormat image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_depth,
            IntPtr image_row_pitch,
            IntPtr image_slice_pitch,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateImage3D")]
        extern static CLMemoryHandle clCreateImage3D_Unix(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            ref ComputeImageFormat image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_depth,
            IntPtr image_row_pitch,
            IntPtr image_slice_pitch,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode RetainMemObject(
            CLMemoryHandle memobj)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clRetainMemObject_Mac(memobj);
                case BindingPlatform.Windows:
                    return clRetainMemObject_Windows(memobj);
                default:
                    return clRetainMemObject_Unix(memobj);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clRetainMemObject")]
        extern static ComputeErrorCode clRetainMemObject_Mac(
            CLMemoryHandle memobj);
        [DllImport(libNameWindows, EntryPoint = "clRetainMemObject")]
        extern static ComputeErrorCode clRetainMemObject_Windows(
            CLMemoryHandle memobj);
        [DllImport(libNameUnix, EntryPoint = "clRetainMemObject")]
        extern static ComputeErrorCode clRetainMemObject_Unix(
            CLMemoryHandle memobj);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode ReleaseMemObject(
            CLMemoryHandle memobj)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clReleaseMemObject_Mac(memobj);
                case BindingPlatform.Windows:
                    return clReleaseMemObject_Windows(memobj);
                default:
                    return clReleaseMemObject_Unix(memobj);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clReleaseMemObject")]
        extern static ComputeErrorCode clReleaseMemObject_Mac(
            CLMemoryHandle memobj);
        [DllImport(libNameWindows, EntryPoint = "clReleaseMemObject")]
        extern static ComputeErrorCode clReleaseMemObject_Windows(
            CLMemoryHandle memobj);
        [DllImport(libNameUnix, EntryPoint = "clReleaseMemObject")]
        extern static ComputeErrorCode clReleaseMemObject_Unix(
            CLMemoryHandle memobj);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetSupportedImageFormats(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            ComputeMemoryType image_type,
            Int32 num_entries,
            [Out, MarshalAs(UnmanagedType.LPArray)] ComputeImageFormat[] image_formats,
            out Int32 num_image_formats)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetSupportedImageFormats_Mac(context, flags, image_type, num_entries, image_formats,
                                                          out num_image_formats);
                case BindingPlatform.Windows:
                    return clGetSupportedImageFormats_Windows(context, flags, image_type, num_entries, image_formats,
                                                          out num_image_formats);
                default:
                    return clGetSupportedImageFormats_Unix(context, flags, image_type, num_entries, image_formats,
                                                          out num_image_formats);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetSupportedImageFormats")]
        extern static ComputeErrorCode clGetSupportedImageFormats_Mac(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            ComputeMemoryType image_type,
            Int32 num_entries,
            [Out, MarshalAs(UnmanagedType.LPArray)] ComputeImageFormat[] image_formats,
            out Int32 num_image_formats);
        [DllImport(libNameWindows, EntryPoint = "clGetSupportedImageFormats")]
        extern static ComputeErrorCode clGetSupportedImageFormats_Windows(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            ComputeMemoryType image_type,
            Int32 num_entries,
            [Out, MarshalAs(UnmanagedType.LPArray)] ComputeImageFormat[] image_formats,
            out Int32 num_image_formats);
        [DllImport(libNameUnix, EntryPoint = "clGetSupportedImageFormats")]
        extern static ComputeErrorCode clGetSupportedImageFormats_Unix(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            ComputeMemoryType image_type,
            Int32 num_entries,
            [Out, MarshalAs(UnmanagedType.LPArray)] ComputeImageFormat[] image_formats,
            out Int32 num_image_formats);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetMemObjectInfo(
            CLMemoryHandle memobj,
            ComputeMemoryInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetMemObjectInfo_Mac(memobj, param_name, param_value_size, param_value,
                                                  out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetMemObjectInfo_Windows(memobj, param_name, param_value_size, param_value,
                                                  out param_value_size_ret);
                default:
                    return clGetMemObjectInfo_Unix(memobj, param_name, param_value_size, param_value,
                                                  out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetMemObjectInfo")]
        extern static ComputeErrorCode clGetMemObjectInfo_Mac(
            CLMemoryHandle memobj,
            ComputeMemoryInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetMemObjectInfo")]
        extern static ComputeErrorCode clGetMemObjectInfo_Windows(
            CLMemoryHandle memobj,
            ComputeMemoryInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetMemObjectInfo")]
        extern static ComputeErrorCode clGetMemObjectInfo_Unix(
            CLMemoryHandle memobj,
            ComputeMemoryInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetImageInfo(
            CLMemoryHandle image,
            ComputeImageInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetImageInfo_Mac(image, param_name, param_value_size, param_value,
                                                  out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetImageInfo_Windows(image, param_name, param_value_size, param_value,
                                                  out param_value_size_ret);
                default:
                    return clGetImageInfo_Unix(image, param_name, param_value_size, param_value,
                                                  out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetImageInfo")]
        extern static ComputeErrorCode clGetImageInfo_Mac(
            CLMemoryHandle image,
            ComputeImageInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetImageInfo")]
       extern static ComputeErrorCode clGetImageInfo_Windows(
            CLMemoryHandle image,
            ComputeImageInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetImageInfo")]
        extern static ComputeErrorCode clGetImageInfo_Unix(
            CLMemoryHandle image,
            ComputeImageInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLSamplerHandle CreateSampler(
            CLContextHandle context,
            [MarshalAs(UnmanagedType.Bool)] bool normalized_coords,
            ComputeImageAddressing addressing_mode,
            ComputeImageFiltering filter_mode,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateSampler_Mac(context, normalized_coords, addressing_mode, filter_mode, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateSampler_Windows(context, normalized_coords, addressing_mode, filter_mode, out errcode_ret);
                default:
                    return clCreateSampler_Unix(context, normalized_coords, addressing_mode, filter_mode, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateSampler")]
        extern static CLSamplerHandle clCreateSampler_Mac(
            CLContextHandle context,
            [MarshalAs(UnmanagedType.Bool)] bool normalized_coords,
            ComputeImageAddressing addressing_mode,
            ComputeImageFiltering filter_mode,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateSampler")]
        extern static CLSamplerHandle clCreateSampler_Windows(
            CLContextHandle context,
            [MarshalAs(UnmanagedType.Bool)] bool normalized_coords,
            ComputeImageAddressing addressing_mode,
            ComputeImageFiltering filter_mode,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateSampler")]
        extern static CLSamplerHandle clCreateSampler_Unix(
            CLContextHandle context,
            [MarshalAs(UnmanagedType.Bool)] bool normalized_coords,
            ComputeImageAddressing addressing_mode,
            ComputeImageFiltering filter_mode,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode RetainSampler(
            CLSamplerHandle sample)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clRetainSampler_Mac(sample);
                case BindingPlatform.Windows:
                    return clRetainSampler_Windows(sample);
                default:
                    return clRetainSampler_Unix(sample);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clRetainSampler")]
        extern static ComputeErrorCode clRetainSampler_Mac(
            CLSamplerHandle sample);
        [DllImport(libNameWindows, EntryPoint = "clRetainSampler")]
        extern static ComputeErrorCode clRetainSampler_Windows(
            CLSamplerHandle sample);
        [DllImport(libNameUnix, EntryPoint = "clRetainSampler")]
        extern static ComputeErrorCode clRetainSampler_Unix(
            CLSamplerHandle sample);


        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode ReleaseSampler(
            CLSamplerHandle sample)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clReleaseSampler_Mac(sample);
                case BindingPlatform.Windows:
                    return clReleaseSampler_Windows(sample);
                default:
                    return clReleaseSampler_Unix(sample);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clReleaseSampler")]
        extern static ComputeErrorCode clReleaseSampler_Mac(
            CLSamplerHandle sample);
        [DllImport(libNameWindows, EntryPoint = "clReleaseSampler")]
        extern static ComputeErrorCode clReleaseSampler_Windows(
            CLSamplerHandle sample);
        [DllImport(libNameUnix, EntryPoint = "clReleaseSampler")]
        extern static ComputeErrorCode clReleaseSampler_Unix(
            CLSamplerHandle sample);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetSamplerInfo(
            CLSamplerHandle sample,
            ComputeSamplerInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetSamplerInfo_Mac(sample, param_name, param_value_size, param_value,
                                                out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetSamplerInfo_Windows(sample, param_name, param_value_size, param_value,
                                                out param_value_size_ret);
                default:
                    return clGetSamplerInfo_Unix(sample, param_name, param_value_size, param_value,
                                                out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetSamplerInfo")]
        extern static ComputeErrorCode clGetSamplerInfo_Mac(
            CLSamplerHandle sample,
            ComputeSamplerInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetSamplerInfo")]
        extern static ComputeErrorCode clGetSamplerInfo_Windows(
            CLSamplerHandle sample,
            ComputeSamplerInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetSamplerInfo")]
        extern static ComputeErrorCode clGetSamplerInfo_Unix(
            CLSamplerHandle sample,
            ComputeSamplerInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLProgramHandle CreateProgramWithSource(
            CLContextHandle context,
            Int32 count,
            String[] strings,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateProgramWithSource_Mac(context, count, strings, lengths, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateProgramWithSource_Windows(context, count, strings, lengths, out errcode_ret);
                default:
                    return clCreateProgramWithSource_Unix(context, count, strings, lengths, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateProgramWithSource")]
        extern static CLProgramHandle clCreateProgramWithSource_Mac(
            CLContextHandle context,
            Int32 count,
            String[] strings,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateProgramWithSource")]
        extern static CLProgramHandle clCreateProgramWithSource_Windows(
            CLContextHandle context,
            Int32 count,
            String[] strings,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateProgramWithSource")]
        extern static CLProgramHandle clCreateProgramWithSource_Unix(
            CLContextHandle context,
            Int32 count,
            String[] strings,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLProgramHandle CreateProgramWithBinary(
            CLContextHandle context,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] binaries,
            [MarshalAs(UnmanagedType.LPArray)] Int32[] binary_status,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateProgramWithBinary_Mac(context, num_devices, device_list, lengths, binaries,
                                                         binary_status, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateProgramWithBinary_Windows(context, num_devices, device_list, lengths, binaries,
                                                         binary_status, out errcode_ret);
                default:
                    return clCreateProgramWithBinary_Unix(context, num_devices, device_list, lengths, binaries,
                                                         binary_status, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateProgramWithBinary")]
        extern static CLProgramHandle clCreateProgramWithBinary_Mac(
            CLContextHandle context,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] binaries,
            [MarshalAs(UnmanagedType.LPArray)] Int32[] binary_status,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateProgramWithBinary")]
        extern static CLProgramHandle clCreateProgramWithBinary_Windows(
            CLContextHandle context,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] binaries,
            [MarshalAs(UnmanagedType.LPArray)] Int32[] binary_status,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateProgramWithBinary")]
        extern static CLProgramHandle clCreateProgramWithBinary_Unix(
            CLContextHandle context,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] binaries,
            [MarshalAs(UnmanagedType.LPArray)] Int32[] binary_status,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode RetainProgram(
            CLProgramHandle program)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clRetainProgram_Mac(program);
                case BindingPlatform.Windows:
                    return clRetainProgram_Windows(program);
                default:
                    return clRetainProgram_Unix(program);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clRetainProgram")]
        extern static ComputeErrorCode clRetainProgram_Mac(
            CLProgramHandle program);
        [DllImport(libNameWindows, EntryPoint = "clRetainProgram")]
        extern static ComputeErrorCode clRetainProgram_Windows(
            CLProgramHandle program);
        [DllImport(libNameUnix, EntryPoint = "clRetainProgram")]
        extern static ComputeErrorCode clRetainProgram_Unix(
            CLProgramHandle program);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode ReleaseProgram(
            CLProgramHandle program)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clReleaseProgram_Mac(program);
                case BindingPlatform.Windows:
                    return clReleaseProgram_Windows(program);
                default:
                    return clReleaseProgram_Unix(program);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clReleaseProgram")]
        extern static ComputeErrorCode clReleaseProgram_Mac(
            CLProgramHandle program);
        [DllImport(libNameWindows, EntryPoint = "clReleaseProgram")]
        extern static ComputeErrorCode clReleaseProgram_Windows(
            CLProgramHandle program);
        [DllImport(libNameUnix, EntryPoint = "clReleaseProgram")]
        extern static ComputeErrorCode clReleaseProgram_Unix(
            CLProgramHandle program);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode BuildProgram(
            CLProgramHandle program,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list,
            String options,
            ComputeProgramBuildNotifier pfn_notify,
            IntPtr user_data)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clBuildProgram_Mac(program, num_devices, device_list, options, pfn_notify, user_data);
                case BindingPlatform.Windows:
                    return clBuildProgram_Windows(program, num_devices, device_list, options, pfn_notify, user_data);
                default:
                    return clBuildProgram_Unix(program, num_devices, device_list, options, pfn_notify, user_data);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clBuildProgram")]
        extern static ComputeErrorCode clBuildProgram_Mac(
            CLProgramHandle program,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list,
            String options,
            ComputeProgramBuildNotifier pfn_notify,
            IntPtr user_data);
        [DllImport(libNameWindows, EntryPoint = "clBuildProgram")]
        extern static ComputeErrorCode clBuildProgram_Windows(
            CLProgramHandle program,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list,
            String options,
            ComputeProgramBuildNotifier pfn_notify,
            IntPtr user_data);
        [DllImport(libNameUnix, EntryPoint = "clBuildProgram")]
        extern static ComputeErrorCode clBuildProgram_Unix(
            CLProgramHandle program,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list,
            String options,
            ComputeProgramBuildNotifier pfn_notify,
            IntPtr user_data);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode UnloadCompiler()
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clUnloadCompiler_Mac();
                case BindingPlatform.Windows:
                    return clUnloadCompiler_Windows();
                default:
                    return clUnloadCompiler_Unix();
            }
        }
        [DllImport(libNameMac, EntryPoint = "clUnloadCompiler")]
        extern static ComputeErrorCode clUnloadCompiler_Mac();
        [DllImport(libNameWindows, EntryPoint = "clUnloadCompiler")]
        extern static ComputeErrorCode clUnloadCompiler_Windows();
        [DllImport(libNameUnix, EntryPoint = "clUnloadCompiler")]
        extern static ComputeErrorCode clUnloadCompiler_Unix();

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetProgramInfo(
            CLProgramHandle program,
            ComputeProgramInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetProgramInfo_Mac(program, param_name, param_value_size, param_value, out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetProgramInfo_Windows(program, param_name, param_value_size, param_value, out param_value_size_ret);
                default:
                    return clGetProgramInfo_Unix(program, param_name, param_value_size, param_value, out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetProgramInfo")]
        extern static ComputeErrorCode clGetProgramInfo_Mac(
            CLProgramHandle program,
            ComputeProgramInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetProgramInfo")]
        extern static ComputeErrorCode clGetProgramInfo_Windows(
            CLProgramHandle program,
            ComputeProgramInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetProgramInfo")]
        extern static ComputeErrorCode clGetProgramInfo_Unix(
            CLProgramHandle program,
            ComputeProgramInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetProgramBuildInfo(
            CLProgramHandle program,
            CLDeviceHandle device,
            ComputeProgramBuildInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetProgramBuildInfo_Mac(program, device, param_name, param_value_size, param_value,
                                                     out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetProgramBuildInfo_Windows(program, device, param_name, param_value_size, param_value,
                                                         out param_value_size_ret);
                default:
                    return clGetProgramBuildInfo_Unix(program, device, param_name, param_value_size, param_value,
                                                      out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetProgramBuildInfo")]
        extern static ComputeErrorCode clGetProgramBuildInfo_Mac(
            CLProgramHandle program,
            CLDeviceHandle device,
            ComputeProgramBuildInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetProgramBuildInfo")]
        extern static ComputeErrorCode clGetProgramBuildInfo_Windows(
            CLProgramHandle program,
            CLDeviceHandle device,
            ComputeProgramBuildInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetProgramBuildInfo")]
        extern static ComputeErrorCode clGetProgramBuildInfo_Unix(
            CLProgramHandle program,
            CLDeviceHandle device,
            ComputeProgramBuildInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLKernelHandle CreateKernel(
            CLProgramHandle program,
            String kernel_name,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateKernel_Mac(program, kernel_name, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateKernel_Windows(program, kernel_name, out errcode_ret);
                default:
                    return clCreateKernel_Unix(program, kernel_name, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateKernel")]
        extern static CLKernelHandle clCreateKernel_Mac(
            CLProgramHandle program,
            String kernel_name,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateKernel")]
        extern static CLKernelHandle clCreateKernel_Windows(
            CLProgramHandle program,
            String kernel_name,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateKernel")]
        extern static CLKernelHandle clCreateKernel_Unix(
            CLProgramHandle program,
            String kernel_name,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode CreateKernelsInProgram(
            CLProgramHandle program,
            Int32 num_kernels,
            [Out, MarshalAs(UnmanagedType.LPArray)] CLKernelHandle[] kernels,
            out Int32 num_kernels_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateKernelsInProgram_Mac(program, num_kernels, kernels, out num_kernels_ret);
                case BindingPlatform.Windows:
                    return clCreateKernelsInProgram_Windows(program, num_kernels, kernels, out num_kernels_ret);
                default:
                    return clCreateKernelsInProgram_Unix(program, num_kernels, kernels, out num_kernels_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateKernelsInProgram")]
        extern static ComputeErrorCode clCreateKernelsInProgram_Mac(
            CLProgramHandle program,
            Int32 num_kernels,
            [Out, MarshalAs(UnmanagedType.LPArray)] CLKernelHandle[] kernels,
            out Int32 num_kernels_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateKernelsInProgram")]
        extern static ComputeErrorCode clCreateKernelsInProgram_Windows(
            CLProgramHandle program,
            Int32 num_kernels,
            [Out, MarshalAs(UnmanagedType.LPArray)] CLKernelHandle[] kernels,
            out Int32 num_kernels_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateKernelsInProgram")]
        extern static ComputeErrorCode clCreateKernelsInProgram_Unix(
            CLProgramHandle program,
            Int32 num_kernels,
            [Out, MarshalAs(UnmanagedType.LPArray)] CLKernelHandle[] kernels,
            out Int32 num_kernels_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode RetainKernel(
            CLKernelHandle kernel)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clRetainKernel_Mac(kernel);
                case BindingPlatform.Windows:
                    return clRetainKernel_Windows(kernel);
                default:
                    return clRetainKernel_Unix(kernel);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clRetainKernel")]
        extern static ComputeErrorCode clRetainKernel_Mac(
            CLKernelHandle kernel);
        [DllImport(libNameWindows, EntryPoint = "clRetainKernel")]
        extern static ComputeErrorCode clRetainKernel_Windows(
            CLKernelHandle kernel);
        [DllImport(libNameUnix, EntryPoint = "clRetainKernel")]
        extern static ComputeErrorCode clRetainKernel_Unix(
            CLKernelHandle kernel);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode ReleaseKernel(
            CLKernelHandle kernel)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clReleaseKernel_Mac(kernel);
                case BindingPlatform.Windows:
                    return clReleaseKernel_Windows(kernel);
                default:
                    return clReleaseKernel_Unix(kernel);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clReleaseKernel")]
        extern static ComputeErrorCode clReleaseKernel_Mac(
            CLKernelHandle kernel);
        [DllImport(libNameWindows, EntryPoint = "clReleaseKernel")]
        extern static ComputeErrorCode clReleaseKernel_Windows(
            CLKernelHandle kernel);
        [DllImport(libNameUnix, EntryPoint = "clReleaseKernel")]
        extern static ComputeErrorCode clReleaseKernel_Unix(
            CLKernelHandle kernel);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode SetKernelArg(
            CLKernelHandle kernel,
            Int32 arg_index,
            IntPtr arg_size,
            IntPtr arg_value)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clSetKernelArg_Mac(kernel, arg_index, arg_size, arg_value);
                case BindingPlatform.Windows:
                    return clSetKernelArg_Windows(kernel, arg_index, arg_size, arg_value);
                default:
                    return clSetKernelArg_Unix(kernel, arg_index, arg_size, arg_value);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clSetKernelArg")]
        extern static ComputeErrorCode clSetKernelArg_Mac(
            CLKernelHandle kernel,
            Int32 arg_index,
            IntPtr arg_size,
            IntPtr arg_value);
        [DllImport(libNameWindows, EntryPoint = "clSetKernelArg")]
        extern static ComputeErrorCode clSetKernelArg_Windows(
            CLKernelHandle kernel,
            Int32 arg_index,
            IntPtr arg_size,
            IntPtr arg_value);
        [DllImport(libNameUnix, EntryPoint = "clSetKernelArg")]
        extern static ComputeErrorCode clSetKernelArg_Unix(
            CLKernelHandle kernel,
            Int32 arg_index,
            IntPtr arg_size,
            IntPtr arg_value);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetKernelInfo(
            CLKernelHandle kernel,
            ComputeKernelInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetKernelInfo_Mac(kernel, param_name, param_value_size, param_value,
                                                     out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetKernelInfo_Windows(kernel, param_name, param_value_size, param_value,
                                                         out param_value_size_ret);
                default:
                    return clGetKernelInfo_Unix(kernel, param_name, param_value_size, param_value,
                                                      out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetKernelInfo")]
        extern static ComputeErrorCode clGetKernelInfo_Mac(
            CLKernelHandle kernel,
            ComputeKernelInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetKernelInfo")]
        extern static ComputeErrorCode clGetKernelInfo_Windows(
            CLKernelHandle kernel,
            ComputeKernelInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetKernelInfo")]
        extern static ComputeErrorCode clGetKernelInfo_Unix(
            CLKernelHandle kernel,
            ComputeKernelInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetKernelWorkGroupInfo(
            CLKernelHandle kernel,
            CLDeviceHandle device,
            ComputeKernelWorkGroupInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetKernelWorkGroupInfo_Mac(kernel, device, param_name, param_value_size, param_value,
                                                        out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetKernelWorkGroupInfo_Windows(kernel, device, param_name, param_value_size, param_value,
                                                         out param_value_size_ret);
                default:
                    return clGetKernelWorkGroupInfo_Unix(kernel, device, param_name, param_value_size, param_value,
                                                      out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetKernelWorkGroupInfo")]
        extern static ComputeErrorCode clGetKernelWorkGroupInfo_Mac(
            CLKernelHandle kernel,
            CLDeviceHandle device,
            ComputeKernelWorkGroupInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetKernelWorkGroupInfo")]
        extern static ComputeErrorCode clGetKernelWorkGroupInfo_Windows(
            CLKernelHandle kernel,
            CLDeviceHandle device,
            ComputeKernelWorkGroupInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetKernelWorkGroupInfo")]
        extern static ComputeErrorCode clGetKernelWorkGroupInfo_Unix(
            CLKernelHandle kernel,
            CLDeviceHandle device,
            ComputeKernelWorkGroupInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode WaitForEvents(
            Int32 num_events,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clWaitForEvents_Mac(num_events, event_list);
                case BindingPlatform.Windows:
                    return clWaitForEvents_Windows(num_events, event_list);
                default:
                    return clWaitForEvents_Unix(num_events, event_list);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clWaitForEvents")]
        extern static ComputeErrorCode clWaitForEvents_Mac(
            Int32 num_events,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list);
        [DllImport(libNameWindows, EntryPoint = "clWaitForEvents")]
        extern static ComputeErrorCode clWaitForEvents_Windows(
            Int32 num_events,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list);
        [DllImport(libNameUnix, EntryPoint = "clWaitForEvents")]
        extern static ComputeErrorCode clWaitForEvents_Unix(
            Int32 num_events,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetEventInfo(
            CLEventHandle @event,
            ComputeEventInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetEventInfo_Mac(@event, param_name, param_value_size, param_value,
                                              out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetEventInfo_Windows(@event, param_name, param_value_size, param_value,
                                              out param_value_size_ret);
                default:
                    return clGetEventInfo_Unix(@event, param_name, param_value_size, param_value,
                                              out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetEventInfo")]
        extern static ComputeErrorCode clGetEventInfo_Mac(
            CLEventHandle @event,
            ComputeEventInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetEventInfo")]
        extern static ComputeErrorCode clGetEventInfo_Windows(
            CLEventHandle @event,
            ComputeEventInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetEventInfo")]
        extern static ComputeErrorCode clGetEventInfo_Unix(
            CLEventHandle @event,
            ComputeEventInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode RetainEvent(
            CLEventHandle @event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clRetainEvent_Mac(@event);
                case BindingPlatform.Windows:
                    return clRetainEvent_Windows(@event);
                default:
                    return clRetainEvent_Unix(@event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clRetainEvent")]
        extern static ComputeErrorCode clRetainEvent_Mac(
            CLEventHandle @event);
        [DllImport(libNameWindows, EntryPoint = "clRetainEvent")]
        extern static ComputeErrorCode clRetainEvent_Windows(
            CLEventHandle @event);
        [DllImport(libNameUnix, EntryPoint = "clRetainEvent")]
        extern static ComputeErrorCode clRetainEvent_Unix(
            CLEventHandle @event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode ReleaseEvent(
            CLEventHandle @event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clReleaseEvent_Mac(@event);
                case BindingPlatform.Windows:
                    return clReleaseEvent_Windows(@event);
                default:
                    return clReleaseEvent_Unix(@event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clReleaseEvent")]
        extern static ComputeErrorCode clReleaseEvent_Mac(
            CLEventHandle @event);
        [DllImport(libNameWindows, EntryPoint = "clReleaseEvent")]
        extern static ComputeErrorCode clReleaseEvent_Windows(
            CLEventHandle @event);
        [DllImport(libNameUnix, EntryPoint = "clReleaseEvent")]
        extern static ComputeErrorCode clReleaseEvent_Unix(
            CLEventHandle @event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetEventProfilingInfo(
            CLEventHandle @event,
            ComputeCommandProfilingInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetEventProfilingInfo_Mac(@event, param_name, param_value_size, param_value,
                                                       out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetEventProfilingInfo_Windows(@event, param_name, param_value_size, param_value,
                                                       out param_value_size_ret);
                default:
                    return clGetEventProfilingInfo_Unix(@event, param_name, param_value_size, param_value,
                                                       out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetEventProfilingInfo")]
        extern static ComputeErrorCode clGetEventProfilingInfo_Mac(
            CLEventHandle @event,
            ComputeCommandProfilingInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetEventProfilingInfo")]
        extern static ComputeErrorCode clGetEventProfilingInfo_Windows(
            CLEventHandle @event,
            ComputeCommandProfilingInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetEventProfilingInfo")]
        extern static ComputeErrorCode clGetEventProfilingInfo_Unix(
            CLEventHandle @event,
            ComputeCommandProfilingInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode Flush(
            CLCommandQueueHandle command_queue)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clFlush_Mac(command_queue);
                case BindingPlatform.Windows:
                    return clFlush_Windows(command_queue);
                default:
                    return clFlush_Unix(command_queue);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clFlush")]
        extern static ComputeErrorCode clFlush_Mac(
            CLCommandQueueHandle command_queue);
        [DllImport(libNameWindows, EntryPoint = "clFlush")]
        extern static ComputeErrorCode clFlush_Windows(
            CLCommandQueueHandle command_queue);
        [DllImport(libNameUnix, EntryPoint = "clFlush")]
        extern static ComputeErrorCode clFlush_Unix(
            CLCommandQueueHandle command_queue);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode Finish(
            CLCommandQueueHandle command_queue)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clFinish_Mac(command_queue);
                case BindingPlatform.Windows:
                    return clFinish_Windows(command_queue);
                default:
                    return clFinish_Unix(command_queue);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clFinish")]
        extern static ComputeErrorCode clFinish_Mac(
            CLCommandQueueHandle command_queue);
        [DllImport(libNameWindows, EntryPoint = "clFinish")]
        extern static ComputeErrorCode clFinish_Windows(
            CLCommandQueueHandle command_queue);
        [DllImport(libNameUnix, EntryPoint = "clFinish")]
        extern static ComputeErrorCode clFinish_Unix(
            CLCommandQueueHandle command_queue);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueReadBuffer(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_read,
            IntPtr offset,
            IntPtr cb,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueReadBuffer_Mac(command_queue, buffer, blocking_read, offset, cb, ptr,
                                                   num_events_in_wait_list, event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueReadBuffer_Windows(command_queue, buffer, blocking_read, offset, cb, ptr,
                                                   num_events_in_wait_list, event_wait_list, new_event);
                default:
                    return clEnqueueReadBuffer_Unix(command_queue, buffer, blocking_read, offset, cb, ptr,
                                                   num_events_in_wait_list, event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueReadBuffer")]
        extern static ComputeErrorCode clEnqueueReadBuffer_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_read,
            IntPtr offset,
            IntPtr cb,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueReadBuffer")]
        extern static ComputeErrorCode clEnqueueReadBuffer_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_read,
            IntPtr offset,
            IntPtr cb,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueReadBuffer")]
        extern static ComputeErrorCode clEnqueueReadBuffer_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_read,
            IntPtr offset,
            IntPtr cb,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueWriteBuffer(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_write,
            IntPtr offset,
            IntPtr cb,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueWriteBuffer_Mac(command_queue, buffer, blocking_write, offset, cb, ptr,
                                                   num_events_in_wait_list, event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueWriteBuffer_Windows(command_queue, buffer, blocking_write, offset, cb, ptr,
                                                   num_events_in_wait_list, event_wait_list, new_event);
                default:
                    return clEnqueueWriteBuffer_Unix(command_queue, buffer, blocking_write, offset, cb, ptr,
                                                   num_events_in_wait_list, event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueWriteBuffer")]
        extern static ComputeErrorCode clEnqueueWriteBuffer_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_write,
            IntPtr offset,
            IntPtr cb,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueWriteBuffer")]
        extern static ComputeErrorCode clEnqueueWriteBuffer_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_write,
            IntPtr offset,
            IntPtr cb,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueWriteBuffer")]
        extern static ComputeErrorCode clEnqueueWriteBuffer_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_write,
            IntPtr offset,
            IntPtr cb,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueCopyBuffer(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_buffer,
            CLMemoryHandle dst_buffer,
            IntPtr src_offset,
            IntPtr dst_offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueCopyBuffer_Mac(command_queue, src_buffer, dst_buffer, src_offset, dst_offset, cb,
                                                   num_events_in_wait_list, event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueCopyBuffer_Windows(command_queue, src_buffer, dst_buffer, src_offset, dst_offset, cb,
                                                   num_events_in_wait_list, event_wait_list, new_event);
                default:
                    return clEnqueueCopyBuffer_Unix(command_queue, src_buffer, dst_buffer, src_offset, dst_offset, cb,
                                                   num_events_in_wait_list, event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueCopyBuffer")]
        extern static ComputeErrorCode clEnqueueCopyBuffer_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_buffer,
            CLMemoryHandle dst_buffer,
            IntPtr src_offset,
            IntPtr dst_offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueCopyBuffer")]
        extern static ComputeErrorCode clEnqueueCopyBuffer_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_buffer,
            CLMemoryHandle dst_buffer,
            IntPtr src_offset,
            IntPtr dst_offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueCopyBuffer")]
        extern static ComputeErrorCode clEnqueueCopyBuffer_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_buffer,
            CLMemoryHandle dst_buffer,
            IntPtr src_offset,
            IntPtr dst_offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueReadImage(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle image,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_read,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            IntPtr row_pitch,
            IntPtr slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueReadImage_Mac(command_queue, image, blocking_read, ref origin, ref region, row_pitch,
                                                  slice_pitch, ptr, num_events_in_wait_list, event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueReadImage_Windows(command_queue, image, blocking_read, ref origin, ref region, row_pitch,
                                                  slice_pitch, ptr, num_events_in_wait_list, event_wait_list, new_event);
                default:
                    return clEnqueueReadImage_Unix(command_queue, image, blocking_read, ref origin, ref region, row_pitch,
                                                  slice_pitch, ptr, num_events_in_wait_list, event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueReadImage")]
        extern static ComputeErrorCode clEnqueueReadImage_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle image,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_read,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            IntPtr row_pitch,
            IntPtr slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueReadImage")]
        extern static ComputeErrorCode clEnqueueReadImage_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle image,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_read,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            IntPtr row_pitch,
            IntPtr slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueReadImage")]
        extern static ComputeErrorCode clEnqueueReadImage_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle image,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_read,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            IntPtr row_pitch,
            IntPtr slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueWriteImage(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle image,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_write,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            IntPtr input_row_pitch,
            IntPtr input_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueWriteImage_Mac(command_queue, image, blocking_write, ref origin, ref region, input_row_pitch,
                                                  input_slice_pitch, ptr, num_events_in_wait_list, event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueWriteImage_Windows(command_queue, image, blocking_write, ref origin, ref region, input_row_pitch,
                                                  input_slice_pitch, ptr, num_events_in_wait_list, event_wait_list, new_event);
                default:
                    return clEnqueueWriteImage_Unix(command_queue, image, blocking_write, ref origin, ref region, input_row_pitch,
                                                  input_slice_pitch, ptr, num_events_in_wait_list, event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueWriteImage")]
        extern static ComputeErrorCode clEnqueueWriteImage_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle image,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_write,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            IntPtr input_row_pitch,
            IntPtr input_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueWriteImage")]
        extern static ComputeErrorCode clEnqueueWriteImage_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle image,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_write,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            IntPtr input_row_pitch,
            IntPtr input_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueWriteImage")]
        extern static ComputeErrorCode clEnqueueWriteImage_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle image,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_write,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            IntPtr input_row_pitch,
            IntPtr input_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueCopyImage(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_image,
            CLMemoryHandle dst_image,
            ref SysIntX3 src_origin,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueCopyImage_Mac(command_queue, src_image, dst_image, ref src_origin, ref dst_origin,
                                                  ref region, num_events_in_wait_list, event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueCopyImage_Windows(command_queue, src_image, dst_image, ref src_origin, ref dst_origin,
                                                  ref region, num_events_in_wait_list, event_wait_list, new_event);
                default:
                    return clEnqueueCopyImage_Unix(command_queue, src_image, dst_image, ref src_origin, ref dst_origin,
                                                  ref region, num_events_in_wait_list, event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueCopyImage")]
        extern static ComputeErrorCode clEnqueueCopyImage_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_image,
            CLMemoryHandle dst_image,
            ref SysIntX3 src_origin,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueCopyImage")]
        extern static ComputeErrorCode clEnqueueCopyImage_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_image,
            CLMemoryHandle dst_image,
            ref SysIntX3 src_origin,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueCopyImage")]
        extern static ComputeErrorCode clEnqueueCopyImage_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_image,
            CLMemoryHandle dst_image,
            ref SysIntX3 src_origin,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueCopyImageToBuffer(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_image,
            CLMemoryHandle dst_buffer,
            ref SysIntX3 src_origin,
            ref SysIntX3 region,
            IntPtr dst_offset,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueCopyImageToBuffer_Mac(command_queue, src_image, dst_buffer, ref src_origin,
                                                          ref region, dst_offset, num_events_in_wait_list,
                                                          event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueCopyImageToBuffer_Windows(command_queue, src_image, dst_buffer, ref src_origin,
                                                          ref region, dst_offset, num_events_in_wait_list,
                                                          event_wait_list, new_event);
                default:
                    return clEnqueueCopyImageToBuffer_Unix(command_queue, src_image, dst_buffer, ref src_origin,
                                                          ref region, dst_offset, num_events_in_wait_list,
                                                          event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueCopyImageToBuffer")]
        extern static ComputeErrorCode clEnqueueCopyImageToBuffer_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_image,
            CLMemoryHandle dst_buffer,
            ref SysIntX3 src_origin,
            ref SysIntX3 region,
            IntPtr dst_offset,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueCopyImageToBuffer")]
        extern static ComputeErrorCode clEnqueueCopyImageToBuffer_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_image,
            CLMemoryHandle dst_buffer,
            ref SysIntX3 src_origin,
            ref SysIntX3 region,
            IntPtr dst_offset,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueCopyImageToBuffer")]
        extern static ComputeErrorCode clEnqueueCopyImageToBuffer_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_image,
            CLMemoryHandle dst_buffer,
            ref SysIntX3 src_origin,
            ref SysIntX3 region,
            IntPtr dst_offset,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueCopyBufferToImage(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_buffer,
            CLMemoryHandle dst_image,
            IntPtr src_offset,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueCopyBufferToImage_Mac(command_queue, src_buffer, dst_image, src_offset,
                                                          ref dst_origin, ref region, num_events_in_wait_list,
                                                          event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueCopyBufferToImage_Windows(command_queue, src_buffer, dst_image, src_offset,
                                                          ref dst_origin, ref region, num_events_in_wait_list,
                                                          event_wait_list, new_event);
                default:
                    return clEnqueueCopyBufferToImage_Unix(command_queue, src_buffer, dst_image, src_offset,
                                                          ref dst_origin, ref region, num_events_in_wait_list,
                                                          event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueCopyBufferToImage")]
        extern static ComputeErrorCode clEnqueueCopyBufferToImage_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_buffer,
            CLMemoryHandle dst_image,
            IntPtr src_offset,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueCopyBufferToImage")]
        extern static ComputeErrorCode clEnqueueCopyBufferToImage_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_buffer,
            CLMemoryHandle dst_image,
            IntPtr src_offset,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueCopyBufferToImage")]
        extern static ComputeErrorCode clEnqueueCopyBufferToImage_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_buffer,
            CLMemoryHandle dst_image,
            IntPtr src_offset,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static IntPtr EnqueueMapBuffer(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_map,
            ComputeMemoryMappingFlags map_flags,
            IntPtr offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueMapBuffer_Mac(command_queue, buffer, blocking_map, map_flags, offset, cb,
                                              num_events_in_wait_list, event_wait_list, new_event, out errcode_ret);
                case BindingPlatform.Windows:
                    return clEnqueueMapBuffer_Windows(command_queue, buffer, blocking_map, map_flags, offset, cb,
                                              num_events_in_wait_list, event_wait_list, new_event, out errcode_ret);
                default:
                    return clEnqueueMapBuffer_Unix(command_queue, buffer, blocking_map, map_flags, offset, cb,
                                              num_events_in_wait_list, event_wait_list, new_event, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueMapBuffer")]
        extern static IntPtr clEnqueueMapBuffer_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_map,
            ComputeMemoryMappingFlags map_flags,
            IntPtr offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueMapBuffer")]
        extern static IntPtr clEnqueueMapBuffer_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_map,
            ComputeMemoryMappingFlags map_flags,
            IntPtr offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueMapBuffer")]
        extern static IntPtr clEnqueueMapBuffer_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_map,
            ComputeMemoryMappingFlags map_flags,
            IntPtr offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static IntPtr EnqueueMapImage(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle image,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_map,
            ComputeMemoryMappingFlags map_flags,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            out IntPtr image_row_pitch,
            out IntPtr image_slice_pitch,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueMapImage_Mac(command_queue, image, blocking_map, map_flags, ref origin, ref region,
                                                 out image_row_pitch, out image_slice_pitch, num_events_in_wait_list,
                                                 event_wait_list, new_event, out errcode_ret);
                case BindingPlatform.Windows:
                    return clEnqueueMapImage_Windows(command_queue, image, blocking_map, map_flags, ref origin, ref region,
                                                 out image_row_pitch, out image_slice_pitch, num_events_in_wait_list,
                                                 event_wait_list, new_event, out errcode_ret);
                default:
                    return clEnqueueMapImage_Unix(command_queue, image, blocking_map, map_flags, ref origin, ref region,
                                                 out image_row_pitch, out image_slice_pitch, num_events_in_wait_list,
                                                 event_wait_list, new_event, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueMapImage")]
        extern static IntPtr clEnqueueMapImage_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle image,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_map,
            ComputeMemoryMappingFlags map_flags,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            out IntPtr image_row_pitch,
            out IntPtr image_slice_pitch,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueMapImage")]
        extern static IntPtr clEnqueueMapImage_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle image,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_map,
            ComputeMemoryMappingFlags map_flags,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            out IntPtr image_row_pitch,
            out IntPtr image_slice_pitch,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueMapImage")]
        extern static IntPtr clEnqueueMapImage_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle image,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_map,
            ComputeMemoryMappingFlags map_flags,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            out IntPtr image_row_pitch,
            out IntPtr image_slice_pitch,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueUnmapMemObject(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle memobj,
            IntPtr mapped_ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueUnmapMemObject_Mac(command_queue, memobj, mapped_ptr, num_events_in_wait_list,
                                                       event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueUnmapMemObject_Windows(command_queue, memobj, mapped_ptr, num_events_in_wait_list,
                                                           event_wait_list, new_event);
                default:
                    return clEnqueueUnmapMemObject_Unix(command_queue, memobj, mapped_ptr, num_events_in_wait_list,
                                                        event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueUnmapMemObject")]
        extern static ComputeErrorCode clEnqueueUnmapMemObject_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle memobj,
            IntPtr mapped_ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueUnmapMemObject")]
        extern static ComputeErrorCode clEnqueueUnmapMemObject_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle memobj,
            IntPtr mapped_ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueUnmapMemObject")]
        extern static ComputeErrorCode clEnqueueUnmapMemObject_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle memobj,
            IntPtr mapped_ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueNDRangeKernel(
            CLCommandQueueHandle command_queue,
            CLKernelHandle kernel,
            Int32 work_dim,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] global_work_offset,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] global_work_size,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] local_work_size,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueNDRangeKernel_Mac(command_queue, kernel, work_dim, global_work_offset,
                                                      global_work_size, local_work_size, num_events_in_wait_list,
                                                      event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueNDRangeKernel_Windows(command_queue, kernel, work_dim, global_work_offset,
                                                      global_work_size, local_work_size, num_events_in_wait_list,
                                                      event_wait_list, new_event);
                default:
                    return clEnqueueNDRangeKernel_Unix(command_queue, kernel, work_dim, global_work_offset,
                                                      global_work_size, local_work_size, num_events_in_wait_list,
                                                      event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueNDRangeKernel")]
        extern static ComputeErrorCode clEnqueueNDRangeKernel_Mac(
            CLCommandQueueHandle command_queue,
            CLKernelHandle kernel,
            Int32 work_dim,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] global_work_offset,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] global_work_size,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] local_work_size,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueNDRangeKernel")]
        extern static ComputeErrorCode clEnqueueNDRangeKernel_Windows(
            CLCommandQueueHandle command_queue,
            CLKernelHandle kernel,
            Int32 work_dim,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] global_work_offset,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] global_work_size,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] local_work_size,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueNDRangeKernel")]
        extern static ComputeErrorCode clEnqueueNDRangeKernel_Unix(
            CLCommandQueueHandle command_queue,
            CLKernelHandle kernel,
            Int32 work_dim,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] global_work_offset,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] global_work_size,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] local_work_size,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueTask(
            CLCommandQueueHandle command_queue,
            CLKernelHandle kernel,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueTask_Mac(command_queue, kernel, num_events_in_wait_list, event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueTask_Windows(command_queue, kernel, num_events_in_wait_list, event_wait_list, new_event);
                default:
                    return clEnqueueTask_Unix(command_queue, kernel, num_events_in_wait_list, event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueTask")]
        extern static ComputeErrorCode clEnqueueTask_Mac(
            CLCommandQueueHandle command_queue,
            CLKernelHandle kernel,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueTask")]
        extern static ComputeErrorCode clEnqueueTask_Windows(
            CLCommandQueueHandle command_queue,
            CLKernelHandle kernel,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueTask")]
        extern static ComputeErrorCode clEnqueueTask_Unix(
            CLCommandQueueHandle command_queue,
            CLKernelHandle kernel,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        // <summary>
        // See the OpenCL specification.
        // </summary>
        /*
        [DllImport(libName, EntryPoint = "clEnqueueNativeKernel")]
        public static ComputeErrorCode EnqueueNativeKernel(
            CLCommandQueueHandle command_queue,
            IntPtr user_func,
            IntPtr args,
            IntPtr cb_args,
            Int32 num_mem_objects,
            IntPtr* mem_list,
            IntPtr* args_mem_loc,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event);
        */

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueMarker(
            CLCommandQueueHandle command_queue,
            out CLEventHandle new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueMarker_Mac(command_queue, out new_event);
                case BindingPlatform.Windows:
                    return clEnqueueMarker_Windows(command_queue, out new_event);
                default:
                    return clEnqueueMarker_Unix(command_queue, out new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueMarker")]
        extern static ComputeErrorCode clEnqueueMarker_Mac(
            CLCommandQueueHandle command_queue,
            out CLEventHandle new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueMarker")]
        extern static ComputeErrorCode clEnqueueMarker_Windows(
            CLCommandQueueHandle command_queue,
            out CLEventHandle new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueMarker")]
        extern static ComputeErrorCode clEnqueueMarker_Unix(
            CLCommandQueueHandle command_queue,
            out CLEventHandle new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueWaitForEvents(
            CLCommandQueueHandle command_queue,
            Int32 num_events,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueWaitForEvents_Mac(command_queue, num_events, event_list);
                case BindingPlatform.Windows:
                    return clEnqueueWaitForEvents_Windows(command_queue, num_events, event_list);
                default:
                    return clEnqueueWaitForEvents_Unix(command_queue, num_events, event_list);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueWaitForEvents")]
        extern static ComputeErrorCode clEnqueueWaitForEvents_Mac(
            CLCommandQueueHandle command_queue,
            Int32 num_events,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueWaitForEvents")]
        extern static ComputeErrorCode clEnqueueWaitForEvents_Windows(
            CLCommandQueueHandle command_queue,
            Int32 num_events,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueWaitForEvents")]
        extern static ComputeErrorCode clEnqueueWaitForEvents_Unix(
            CLCommandQueueHandle command_queue,
            Int32 num_events,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueBarrier(
            CLCommandQueueHandle command_queue)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueBarrier_Mac(command_queue);
                case BindingPlatform.Windows:
                    return clEnqueueBarrier_Windows(command_queue);
                default:
                    return clEnqueueBarrier_Unix(command_queue);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueBarrier")]
        extern static ComputeErrorCode clEnqueueBarrier_Mac(
            CLCommandQueueHandle command_queue);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueBarrier")]
        extern static ComputeErrorCode clEnqueueBarrier_Windows(
            CLCommandQueueHandle command_queue);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueBarrier")]
        extern static ComputeErrorCode clEnqueueBarrier_Unix(
            CLCommandQueueHandle command_queue);

        
        /// <summary>
        /// Gets the extension function address for the given function name,
        /// or NULL if a valid function can not be found. The client must
        /// check to make sure the address is not NULL, before using or 
        /// calling the returned function address.
        /// </summary>
        public static IntPtr GetExtensionFunctionAddress(
            String func_name)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetExtensionFunctionAddress_Mac(func_name);
                case BindingPlatform.Windows:
                    return clGetExtensionFunctionAddress_Windows(func_name);
                default:
                    return clGetExtensionFunctionAddress_Unix(func_name);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetExtensionFunctionAddress")]
        extern static IntPtr clGetExtensionFunctionAddress_Mac(
            String func_name);
        [DllImport(libNameWindows, EntryPoint = "clGetExtensionFunctionAddress")]
        extern static IntPtr clGetExtensionFunctionAddress_Windows(
            String func_name);
        [DllImport(libNameUnix, EntryPoint = "clGetExtensionFunctionAddress")]
        extern static IntPtr clGetExtensionFunctionAddress_Unix(
            String func_name);


        /**************************************************************************************/
        // CL/GL Sharing API

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLMemoryHandle CreateFromGLBuffer(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 bufobj,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateFromGLBuffer_Mac(context, flags, bufobj, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateFromGLBuffer_Windows(context, flags, bufobj, out errcode_ret);
                default:
                    return clCreateFromGLBuffer_Unix(context, flags, bufobj, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateFromGLBuffer")]
        extern static CLMemoryHandle clCreateFromGLBuffer_Mac(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 bufobj,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateFromGLBuffer")]
        extern static CLMemoryHandle clCreateFromGLBuffer_Windows(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 bufobj,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateFromGLBuffer")]
        extern static CLMemoryHandle clCreateFromGLBuffer_Unix(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 bufobj,
            out ComputeErrorCode errcode_ret);


        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLMemoryHandle CreateFromGLTexture2D(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 target,
            Int32 miplevel,
            Int32 texture,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateFromGLTexture2D_Mac(context, flags, target, miplevel, texture, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateFromGLTexture2D_Windows(context, flags, target, miplevel, texture, out errcode_ret);
                default:
                    return clCreateFromGLTexture2D_Unix(context, flags, target, miplevel, texture, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateFromGLTexture2D")]
        extern static CLMemoryHandle clCreateFromGLTexture2D_Mac(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 target,
            Int32 miplevel,
            Int32 texture,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateFromGLTexture2D")]
        extern static CLMemoryHandle clCreateFromGLTexture2D_Windows(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 target,
            Int32 miplevel,
            Int32 texture,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateFromGLTexture2D")]
        extern static CLMemoryHandle clCreateFromGLTexture2D_Unix(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 target,
            Int32 miplevel,
            Int32 texture,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLMemoryHandle CreateFromGLTexture3D(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 target,
            Int32 miplevel,
            Int32 texture,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateFromGLTexture3D_Mac(context, flags, target, miplevel, texture, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateFromGLTexture3D_Windows(context, flags, target, miplevel, texture, out errcode_ret);
                default:
                    return clCreateFromGLTexture3D_Unix(context, flags, target, miplevel, texture, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateFromGLTexture3D")]
        extern static CLMemoryHandle clCreateFromGLTexture3D_Mac(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 target,
            Int32 miplevel,
            Int32 texture,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateFromGLTexture3D")]
        extern static CLMemoryHandle clCreateFromGLTexture3D_Windows(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 target,
            Int32 miplevel,
            Int32 texture,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateFromGLTexture3D")]
        extern static CLMemoryHandle clCreateFromGLTexture3D_Unix(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 target,
            Int32 miplevel,
            Int32 texture,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLMemoryHandle CreateFromGLRenderbuffer(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 renderbuffer,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateFromGLRenderbuffer_Mac(context, flags, renderbuffer, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateFromGLRenderbuffer_Windows(context, flags, renderbuffer, out errcode_ret);
                default:
                    return clCreateFromGLRenderbuffer_Unix(context, flags, renderbuffer, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateFromGLRenderbuffer")]
        extern static CLMemoryHandle clCreateFromGLRenderbuffer_Mac(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 renderbuffer,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateFromGLRenderbuffer")]
        extern static CLMemoryHandle clCreateFromGLRenderbuffer_Windows(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 renderbuffer,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateFromGLRenderbuffer")]
        extern static CLMemoryHandle clCreateFromGLRenderbuffer_Unix(
            CLContextHandle context,
            ComputeMemoryFlags flags,
            Int32 renderbuffer,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetGLObjectInfo(
            CLMemoryHandle memobj,
            out ComputeGLObjectType gl_object_type,
            out Int32 gl_object_name)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetGLObjectInfo_Mac(memobj, out gl_object_type, out gl_object_name);
                case BindingPlatform.Windows:
                    return clGetGLObjectInfo_Windows(memobj, out gl_object_type, out gl_object_name);
                default:
                    return clGetGLObjectInfo_Unix(memobj, out gl_object_type, out gl_object_name);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetGLObjectInfo")]
        extern static ComputeErrorCode clGetGLObjectInfo_Mac(
            CLMemoryHandle memobj,
            out ComputeGLObjectType gl_object_type,
            out Int32 gl_object_name);
        [DllImport(libNameWindows, EntryPoint = "clGetGLObjectInfo")]
        extern static ComputeErrorCode clGetGLObjectInfo_Windows(
            CLMemoryHandle memobj,
            out ComputeGLObjectType gl_object_type,
            out Int32 gl_object_name);
        [DllImport(libNameUnix, EntryPoint = "clGetGLObjectInfo")]
        extern static ComputeErrorCode clGetGLObjectInfo_Unix(
            CLMemoryHandle memobj,
            out ComputeGLObjectType gl_object_type,
            out Int32 gl_object_name);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode GetGLTextureInfo(
            CLMemoryHandle memobj,
            ComputeGLTextureInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clGetGLTextureInfo_Mac(memobj, param_name, param_value_size, param_value,
                                                  out param_value_size_ret);
                case BindingPlatform.Windows:
                    return clGetGLTextureInfo_Windows(memobj, param_name, param_value_size, param_value,
                                                      out param_value_size_ret);
                default:
                    return clGetGLTextureInfo_Unix(memobj, param_name, param_value_size, param_value,
                                                   out param_value_size_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clGetGLTextureInfo")]
        extern static ComputeErrorCode clGetGLTextureInfo_Mac(
            CLMemoryHandle memobj,
            ComputeGLTextureInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameWindows, EntryPoint = "clGetGLTextureInfo")]
        extern static ComputeErrorCode clGetGLTextureInfo_Windows(
            CLMemoryHandle memobj,
            ComputeGLTextureInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);
        [DllImport(libNameUnix, EntryPoint = "clGetGLTextureInfo")]
        extern static ComputeErrorCode clGetGLTextureInfo_Unix(
            CLMemoryHandle memobj,
            ComputeGLTextureInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueAcquireGLObjects(
            CLCommandQueueHandle command_queue,
            Int32 num_objects,
            [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueAcquireGLObjects_Mac(command_queue, num_objects, mem_objects,
                                                         num_events_in_wait_list, event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueAcquireGLObjects_Windows(command_queue, num_objects, mem_objects,
                                                             num_events_in_wait_list, event_wait_list, new_event);
                default:
                    return clEnqueueAcquireGLObjects_Unix(command_queue, num_objects, mem_objects,
                                                          num_events_in_wait_list, event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueAcquireGLObjects")]
        extern static ComputeErrorCode clEnqueueAcquireGLObjects_Mac(
            CLCommandQueueHandle command_queue,
            Int32 num_objects,
            [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueAcquireGLObjects")]
        extern static ComputeErrorCode clEnqueueAcquireGLObjects_Windows(
            CLCommandQueueHandle command_queue,
            Int32 num_objects,
            [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueAcquireGLObjects")]
        extern static ComputeErrorCode clEnqueueAcquireGLObjects_Unix(
            CLCommandQueueHandle command_queue,
            Int32 num_objects,
            [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueReleaseGLObjects(
            CLCommandQueueHandle command_queue,
            Int32 num_objects,
            [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueReleaseGLObjects_Mac(command_queue, num_objects, mem_objects,
                                                         num_events_in_wait_list, event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueReleaseGLObjects_Windows(command_queue, num_objects, mem_objects,
                                                             num_events_in_wait_list, event_wait_list, new_event);
                default:
                    return clEnqueueReleaseGLObjects_Unix(command_queue, num_objects, mem_objects,
                                                          num_events_in_wait_list, event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueReleaseGLObjects")]
        extern static ComputeErrorCode clEnqueueReleaseGLObjects_Mac(
            CLCommandQueueHandle command_queue,
            Int32 num_objects,
            [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueReleaseGLObjects")]
        extern static ComputeErrorCode clEnqueueReleaseGLObjects_Windows(
            CLCommandQueueHandle command_queue,
            Int32 num_objects,
            [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueReleaseGLObjects")]
        extern static ComputeErrorCode clEnqueueReleaseGLObjects_Unix(
            CLCommandQueueHandle command_queue,
            Int32 num_objects,
            [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
    }

    /// <summary>
    /// A callback function that can be registered by the application to report information on errors that occur in the <see cref="ComputeContext"/>.
    /// </summary>
    /// <param name="errorInfo"> An error string. </param>
    /// <param name="clDataPtr"> A pointer to binary data that is returned by the OpenCL implementation that can be used to log additional information helpful in debugging the error.</param>
    /// <param name="clDataSize"> The size of the binary data that is returned by the OpenCL. </param>
    /// <param name="userDataPtr"> The pointer to the optional user data specified in <paramref name="userDataPtr"/> argument of <see cref="ComputeContext"/> constructor. </param>
    /// <remarks> This callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe. </remarks>
    public delegate void ComputeContextNotifier(String errorInfo, IntPtr clDataPtr, IntPtr clDataSize, IntPtr userDataPtr);

    /// <summary>
    /// A callback function that can be registered by the application to report the <see cref="ComputeProgram"/> build status.
    /// </summary>
    /// <param name="programHandle"> The handle of the <see cref="ComputeProgram"/> being built. </param>
    /// <param name="notifyDataPtr"> The pointer to the optional user data specified in <paramref name="notifyDataPtr"/> argument of <see cref="ComputeProgram.Build"/>. </param>
    /// <remarks> This callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe. </remarks>
    public delegate void ComputeProgramBuildNotifier(CLProgramHandle programHandle, IntPtr notifyDataPtr);
}
