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

    /// <summary>
    /// Contains bindings to the OpenCL 1.2 functions.
    /// </summary>
    /// <remarks> See the OpenCL specification for documentation regarding these functions. </remarks>
    public interface ICL12 : ICL11
    {
        ComputeErrorCode CreateSubDevices(CLDeviceHandle device, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, System.Int32 num_devices, [Out, MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices, out IntPtr num_devices_ret);
        ComputeErrorCode RetainDevice(CLDeviceHandle device);
        ComputeErrorCode ReleaseDevice(CLDeviceHandle device);
        CLMemoryHandle CreateImage(CLContextHandle context, ComputeMemoryFlags flags, ref ComputeImageFormat image_format, ref ComputeImageDescription image_desc, IntPtr host_ptr, out ComputeErrorCode errcode_ret);
        CLProgramHandle CreateProgramWithBuiltInKernels(CLContextHandle context, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, String kernel_names, out ComputeErrorCode errcode_ret);
        ComputeErrorCode CompileProgram(CLProgramHandle program, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, String options, Int32 num_input_headers, [MarshalAs(UnmanagedType.LPArray)] CLProgramHandle[] input_headers, String[] header_include_names, ComputeProgramBuildNotifier pfn_notify, IntPtr user_data);
        ComputeErrorCode LinkProgram(CLContextHandle context, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, String options, Int32 num_input_programs, ComputeProgramBuildNotifier pfn_notify, IntPtr user_data, out ComputeErrorCode errcode_ret);
        ComputeErrorCode UnloadPlatformCompiler(CLProgramHandle program);
        ComputeErrorCode GetKernelArgInfo(CLKernelHandle kernel, Int32 arg_indx, ComputeKernelArgInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        ComputeErrorCode EnqueueFillBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, IntPtr pattern, IntPtr pattern_size, IntPtr offset, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        ComputeErrorCode EnqueueFillImage(CLCommandQueueHandle command_queue, CLMemoryHandle image, IntPtr fill_color, ref SysIntX3 origin, ref SysIntX3 region, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        ComputeErrorCode EnqueueMigrateMemObjects(CLCommandQueueHandle command_queue, Int32 num_mem_objects, [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects, ComputeMemoryMigrationFlags flags, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        ComputeErrorCode EnqueueMarkerWithWaitList(CLCommandQueueHandle command_queue, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        ComputeErrorCode EnqueueBarrierWithWaitList(CLCommandQueueHandle command_queue, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        IntPtr GetExtensionFunctionAddressForPlatform(CLPlatformHandle platform, String func_name);
        CLMemoryHandle CreateFromGLTexture(CLContextHandle context, ComputeMemoryFlags flags, Int32 texture_target, Int32 miplevel, Int32 texture, out ComputeErrorCode errcode_ret);

        #region Deprecated functions

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new CLMemoryHandle CreateFromGLTexture2D(CLContextHandle context, ComputeMemoryFlags flags, Int32 target, Int32 miplevel, Int32 texture, out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new CLMemoryHandle CreateFromGLTexture3D(CLContextHandle context, ComputeMemoryFlags flags, Int32 target, Int32 miplevel, Int32 texture, out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new CLMemoryHandle CreateImage2D(CLContextHandle context, ComputeMemoryFlags flags, ref ComputeImageFormat image_format, IntPtr image_width, IntPtr image_height, IntPtr image_row_pitch, IntPtr host_ptr, out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new CLMemoryHandle CreateImage3D(CLContextHandle context, ComputeMemoryFlags flags, ref ComputeImageFormat image_format, IntPtr image_width, IntPtr image_height, IntPtr image_depth, IntPtr image_row_pitch, IntPtr image_slice_pitch, IntPtr host_ptr, out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new ComputeErrorCode EnqueueBarrier(CLCommandQueueHandle command_queue);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new ComputeErrorCode EnqueueMarker(CLCommandQueueHandle command_queue, out CLEventHandle new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new ComputeErrorCode EnqueueWaitForEvents(CLCommandQueueHandle command_queue, Int32 num_events, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new IntPtr GetExtensionFunctionAddress(String func_name);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.2.")]
        new ComputeErrorCode UnloadCompiler();

        #endregion
    }
}
