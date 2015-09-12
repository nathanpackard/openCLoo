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
    /// Contains bindings to the OpenCL 2.0 functions.
    /// </summary>
    /// <remarks> See the OpenCL specification for documentation regarding these functions. </remarks>
    public interface ICL20 : ICL12
    {
        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        CLCommandQueueHandle CreateCommandQueueWithProperties(CLContextHandle context, CLDeviceHandle device, ComputeCommandQueueFlags properties, out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        CLMemoryHandle CreatePipe(CLContextHandle context, ComputeMemoryFlags flags, Int32 pipe_packet_size, Int32 pipe_max_packets, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        ComputeErrorCode GetPipeInfo(CLMemoryHandle pipe, ComputePipeInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        CLMemoryHandle ComputeSvmAlloc(CLContextHandle context, ComputeMemoryFlags flags, IntPtr size, Int32 alignment);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        void ComputeSvmFree(CLContextHandle context, CLMemoryHandle svm_pointer);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        CLSamplerHandle CreateSamplerWithProperties(CLContextHandle context, [MarshalAs(UnmanagedType.LPArray)] ComputeSamplerInfo[] sampler_properties, out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        ComputeErrorCode SetKernelArgSvmPointer(CLKernelHandle kernel, Int32 arg_index, IntPtr arg_value);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        ComputeErrorCode SetKernelExecInfo(CLKernelHandle kernel, ComputeExecInfo param_name, IntPtr param_value_size, IntPtr param_value);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        ComputeErrorCode EnqueueSVMFree(CLCommandQueueHandle command_queue, Int32 num_svm_pointers, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] svm_pointers, ComputeFreeFunctionCallback pfn_free, IntPtr user_data, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        ComputeErrorCode EnqueueSVMMemcpy(CLCommandQueueHandle command_queue, ComputeBoolean blocking_copy, IntPtr dst_ptr, IntPtr src_ptr, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        ComputeErrorCode EnqueueSVMMemFill(CLCommandQueueHandle command_queue, IntPtr svm_ptr, IntPtr pattern, IntPtr pattern_size, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        ComputeErrorCode EnqueueSVMMap(CLCommandQueueHandle command_queue, ComputeBoolean blocking_map, ComputeMemoryMappingFlags flags, IntPtr svm_ptr, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        ComputeErrorCode EnqueueSVMUnMap(CLCommandQueueHandle command_queue, IntPtr svm_ptr, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 2.0.")]
        new CLCommandQueueHandle CreateCommandQueue(CLContextHandle context, CLDeviceHandle device, ComputeCommandQueueFlags properties, out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 2.0.")]
        new CLSamplerHandle CreateSampler(CLContextHandle context, [MarshalAs(UnmanagedType.Bool)] bool normalized_coords, ComputeImageAddressing addressing_mode, ComputeImageFiltering filter_mode, out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 2.0.")]
        new ComputeErrorCode EnqueueTask(CLCommandQueueHandle command_queue, CLKernelHandle kernel, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queue"></param>
    /// <param name="num_svm_pointers"></param>
    /// <param name="svm_pointers"></param>
    /// <param name="user_data"></param>
    public delegate void ComputeFreeFunctionCallback(CLCommandQueueHandle queue, Int32 num_svm_pointers, IntPtr[] svm_pointers, IntPtr user_data);

}
