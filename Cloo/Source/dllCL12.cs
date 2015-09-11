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
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Security;

    /// <summary>
    /// Contains bindings to the OpenCL 1.2 functions.
    /// </summary>
    /// <remarks> See the OpenCL specification for documentation regarding these functions. </remarks>
    [SuppressUnmanagedCodeSecurity]
    public class CL12 : CL11, ICL20
    {
        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateSubDevices")]
        public static extern ComputeErrorCode StaticCreateSubDevices(CLDeviceHandle device, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, System.Int32 num_devices, [Out, MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices, out IntPtr num_devices_ret);
        new public ComputeErrorCode CreateSubDevices(CLDeviceHandle device, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, System.Int32 num_devices, [Out, MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices, out IntPtr num_devices_ret) { return StaticCreateSubDevices(device, properties, num_devices, devices, out num_devices_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clRetainDevice")]
        public static extern ComputeErrorCode StaticRetainDevice(CLDeviceHandle device);
        new public ComputeErrorCode RetainDevice(CLDeviceHandle device) { return StaticRetainDevice(device); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clReleaseDevice")]
        public static extern ComputeErrorCode StaticReleaseDevice(CLDeviceHandle device);
        new public ComputeErrorCode ReleaseDevice(CLDeviceHandle device) { return StaticReleaseDevice(device); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateImage")]
        public static extern CLMemoryHandle StaticCreateImage(CLContextHandle context, ComputeMemoryFlags flags, ref ComputeImageFormat image_format, ref ComputeImageDescription image_desc, IntPtr host_ptr, out ComputeErrorCode errcode_ret);
        new public CLMemoryHandle CreateImage(CLContextHandle context, ComputeMemoryFlags flags, ref ComputeImageFormat image_format, ref ComputeImageDescription image_desc, IntPtr host_ptr, out ComputeErrorCode errcode_ret) { return StaticCreateImage(context, flags, ref image_format, ref image_desc, host_ptr, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateProgramWithBuiltInKernels")]
        public static extern CLProgramHandle StaticCreateProgramWithBuiltInKernels(CLContextHandle context, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, String kernel_names, out ComputeErrorCode errcode_ret);
        new public CLProgramHandle CreateProgramWithBuiltInKernels(CLContextHandle context, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, String kernel_names, out ComputeErrorCode errcode_ret) { return StaticCreateProgramWithBuiltInKernels(context, num_devices, device_list, kernel_names, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCompileProgram")]
        public static extern ComputeErrorCode StaticCompileProgram(CLProgramHandle program, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, String options, Int32 num_input_headers, [MarshalAs(UnmanagedType.LPArray)] CLProgramHandle[] input_headers, String[] header_include_names, ComputeProgramBuildNotifier pfn_notify, IntPtr user_data);
        new public ComputeErrorCode CompileProgram(CLProgramHandle program, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, String options, Int32 num_input_headers, [MarshalAs(UnmanagedType.LPArray)] CLProgramHandle[] input_headers, String[] header_include_names, ComputeProgramBuildNotifier pfn_notify, IntPtr user_data) { return StaticCompileProgram(program, num_devices, device_list, options, num_input_headers, input_headers, header_include_names, pfn_notify, user_data); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clLinkProgram")]
        public static extern ComputeErrorCode StaticLinkProgram(CLContextHandle context, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, String options, Int32 num_input_programs, ComputeProgramBuildNotifier pfn_notify, IntPtr user_data, out ComputeErrorCode errcode_ret);
        new public ComputeErrorCode LinkProgram(CLContextHandle context, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, String options, Int32 num_input_programs, ComputeProgramBuildNotifier pfn_notify, IntPtr user_data, out ComputeErrorCode errcode_ret) { return StaticLinkProgram(context, num_devices, device_list, options, num_input_programs, pfn_notify, user_data, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clUnloadPlatformCompiler")]
        public static extern ComputeErrorCode StaticUnloadPlatformCompiler(CLProgramHandle program);
        new public ComputeErrorCode UnloadPlatformCompiler(CLProgramHandle program) { return StaticUnloadPlatformCompiler(program); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetKernelArgInfo")]
        public static extern ComputeErrorCode StaticGetKernelArgInfo(CLKernelHandle kernel, Int32 arg_indx, ComputeKernelArgInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        new public ComputeErrorCode GetKernelArgInfo(CLKernelHandle kernel, Int32 arg_indx, ComputeKernelArgInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetKernelArgInfo(kernel, arg_indx, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueFillBuffer")]
        public static extern ComputeErrorCode StaticEnqueueFillBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, IntPtr pattern, IntPtr pattern_size, IntPtr offset, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueFillBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, IntPtr pattern, IntPtr pattern_size, IntPtr offset, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueFillBuffer(command_queue, buffer, pattern, pattern_size, offset, size, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueFillImage")]
        public static extern ComputeErrorCode StaticEnqueueFillImage(CLCommandQueueHandle command_queue, CLMemoryHandle image, IntPtr fill_color, ref SysIntX3 origin, ref SysIntX3 region, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueFillImage(CLCommandQueueHandle command_queue, CLMemoryHandle image, IntPtr fill_color, ref SysIntX3 origin, ref SysIntX3 region, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, CLEventHandle new_event) { return StaticEnqueueFillImage(command_queue, image, fill_color, ref origin, ref region, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueMigrateMemObjects")]
        public static extern ComputeErrorCode StaticEnqueueMigrateMemObjects(CLCommandQueueHandle command_queue, Int32 num_mem_objects, [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects, ComputeMemoryMigrationFlags flags, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueMigrateMemObjects(CLCommandQueueHandle command_queue, Int32 num_mem_objects, [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects, ComputeMemoryMigrationFlags flags, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueMigrateMemObjects(command_queue, num_mem_objects, mem_objects, flags, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueMarkerWithWaitList")]
        public static extern ComputeErrorCode StaticEnqueueMarkerWithWaitList(CLCommandQueueHandle command_queue, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueMarkerWithWaitList(CLCommandQueueHandle command_queue, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueMarkerWithWaitList(command_queue, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueBarrierWithWaitList")]
        public static extern ComputeErrorCode StaticEnqueueBarrierWithWaitList(CLCommandQueueHandle command_queue, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueBarrierWithWaitList(CLCommandQueueHandle command_queue, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueBarrierWithWaitList(command_queue, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetExtensionFunctionAddressForPlatform")]
        public static extern IntPtr StaticGetExtensionFunctionAddressForPlatform(CLPlatformHandle platform, String func_name);
        new public IntPtr GetExtensionFunctionAddressForPlatform(CLPlatformHandle platform, String func_name) { return StaticGetExtensionFunctionAddressForPlatform(platform, func_name); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateFromGLTexture")]
        public static extern CLMemoryHandle StaticCreateFromGLTexture(CLContextHandle context, ComputeMemoryFlags flags, Int32 texture_target, Int32 miplevel, Int32 texture, out ComputeErrorCode errcode_ret);
        new public CLMemoryHandle CreateFromGLTexture(CLContextHandle context, ComputeMemoryFlags flags, Int32 texture_target, Int32 miplevel, Int32 texture, out ComputeErrorCode errcode_ret) { return StaticCreateFromGLTexture(context, flags, texture_target, miplevel, texture, out errcode_ret); }

        #region Deprecated functions

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new CLMemoryHandle CreateFromGLTexture2D(CLContextHandle context, ComputeMemoryFlags flags, Int32 target, Int32 miplevel, Int32 texture, out ComputeErrorCode errcode_ret)
        {
            Trace.WriteLine("WARNING! CreateFromGLTexture2D has been deprecated in OpenCL 1.2.");
            return CL11.StaticCreateFromGLTexture2D(context, flags, target, miplevel, texture, out errcode_ret);
        }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new CLMemoryHandle CreateFromGLTexture3D(CLContextHandle context, ComputeMemoryFlags flags, Int32 target, Int32 miplevel, Int32 texture, out ComputeErrorCode errcode_ret)
        {
            Trace.WriteLine("WARNING! CreateFromGLTexture3D has been deprecated in OpenCL 1.2.");
            return CL11.StaticCreateFromGLTexture3D(context, flags, target, miplevel, texture, out errcode_ret);
        }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new CLMemoryHandle CreateImage2D(CLContextHandle context, ComputeMemoryFlags flags, ref ComputeImageFormat image_format, IntPtr image_width, IntPtr image_height, IntPtr image_row_pitch, IntPtr host_ptr, out ComputeErrorCode errcode_ret)
        {
            Trace.WriteLine("WARNING! CreateImage2D has been deprecated in OpenCL 1.2.");
            return CL11.StaticCreateImage2D(context, flags, ref image_format, image_width, image_height, image_row_pitch, host_ptr, out errcode_ret);
        }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new CLMemoryHandle CreateImage3D(CLContextHandle context, ComputeMemoryFlags flags, ref ComputeImageFormat image_format, IntPtr image_width, IntPtr image_height, IntPtr image_depth, IntPtr image_row_pitch, IntPtr image_slice_pitch, IntPtr host_ptr, out ComputeErrorCode errcode_ret)
        {
            Trace.WriteLine("WARNING! CreateImage3D has been deprecated in OpenCL 1.2.");
            return CL11.StaticCreateImage3D(context, flags, ref image_format, image_width, image_height, image_depth, image_row_pitch, image_slice_pitch, host_ptr, out errcode_ret);
        }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new ComputeErrorCode EnqueueBarrier(CLCommandQueueHandle command_queue)
        {
            Trace.WriteLine("WARNING! EnqueueBarrier has been deprecated in OpenCL 1.2.");
            return CL11.StaticEnqueueBarrier(command_queue);
        }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new ComputeErrorCode EnqueueMarker(CLCommandQueueHandle command_queue, out CLEventHandle new_event)
        {
            Trace.WriteLine("WARNING! EnqueueMarker has been deprecated in OpenCL 1.2.");
            return CL11.StaticEnqueueMarker(command_queue, out new_event);
        }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new ComputeErrorCode EnqueueWaitForEvents(CLCommandQueueHandle command_queue, Int32 num_events, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list)
        {
            Trace.WriteLine("WARNING! EnqueueWaitForEvents has been deprecated in OpenCL 1.2.");
            return CL11.StaticEnqueueWaitForEvents(command_queue, num_events, event_list);
        }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new IntPtr GetExtensionFunctionAddress(String func_name)
        {
            Trace.WriteLine("WARNING! GetExtensionFunctionAddress has been deprecated in OpenCL 1.2.");
            return CL11.StaticGetExtensionFunctionAddress(func_name);
        }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new public ComputeErrorCode UnloadCompiler()
        {
            Trace.WriteLine("WARNING! UnloadCompiler has been deprecated in OpenCL 1.2.");
            return CL11.StaticUnloadCompiler();
        }

        #endregion

    }
}
