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
    /// Contains bindings to the OpenCL 2.0 functions.
    /// </summary>
    /// <remarks> See the OpenCL specification for documentation regarding these functions. </remarks>
    [SuppressUnmanagedCodeSecurity]
    public class CL20 : CL12, ICL20
    {
        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateCommandQueueWithProperties")]
        public static extern CLCommandQueueHandle StaticCreateCommandQueueWithProperties(CLContextHandle context, CLDeviceHandle device, ComputeCommandQueueFlags properties, out ComputeErrorCode errcode_ret);
        new public CLCommandQueueHandle CreateCommandQueueWithProperties(CLContextHandle context, CLDeviceHandle device, ComputeCommandQueueFlags properties, out ComputeErrorCode errcode_ret) { return StaticCreateCommandQueueWithProperties(context, device, properties, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreatePipe")]
        public static extern CLMemoryHandle StaticCreatePipe(CLContextHandle context, ComputeMemoryFlags flags, Int32 pipe_packet_size, Int32 pipe_max_packets, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, out ComputeErrorCode errcode_ret);
        new public CLMemoryHandle CreatePipe(CLContextHandle context, ComputeMemoryFlags flags, Int32 pipe_packet_size, Int32 pipe_max_packets, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, out ComputeErrorCode errcode_ret) { return StaticCreatePipe(context, flags, pipe_packet_size, pipe_max_packets, properties, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetPipeInfo")]
        public static extern ComputeErrorCode StaticGetPipeInfo(ComputeMemory pipe, ComputePipeInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        new public ComputeErrorCode GetPipeInfo(ComputeMemory pipe, ComputePipeInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetPipeInfo(pipe, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clSVMAlloc")]
        public static extern IntPtr StaticComputeSvmAlloc(CLContextHandle context, ComputeMemoryFlags flags, IntPtr size, Int32 alignment);
        new public IntPtr ComputeSvmAlloc(CLContextHandle context, ComputeMemoryFlags flags, IntPtr size, Int32 alignment) { return StaticComputeSvmAlloc(context, flags, size, alignment); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clSvmFree")]
        public static extern void StaticComputeSvmFree(CLContextHandle context, IntPtr svm_pointer);
        new public void ComputeSvmFree(CLContextHandle context, IntPtr svm_pointer) { StaticComputeSvmFree(context, svm_pointer); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateSamplerWithProperties")]
        public static extern CLSamplerHandle StaticCreateSamplerWithProperties(CLContextHandle context, [MarshalAs(UnmanagedType.LPArray)] ComputeSamplerInfo[] sampler_properties, out ComputeErrorCode errcode_ret);
        new public CLSamplerHandle CreateSamplerWithProperties(CLContextHandle context, [MarshalAs(UnmanagedType.LPArray)] ComputeSamplerInfo[] sampler_properties, out ComputeErrorCode errcode_ret) { return StaticCreateSamplerWithProperties(context, sampler_properties, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clSetKernelArgSVMPointer")]
        public static extern ComputeErrorCode StaticSetKernelArgSvmPointer(CLKernelHandle kernel, Int32 arg_index, IntPtr arg_value);
        new public ComputeErrorCode SetKernelArgSvmPointer(CLKernelHandle kernel, Int32 arg_index, IntPtr arg_value) { return StaticSetKernelArgSvmPointer(kernel, arg_index, arg_value); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clSetKernelExecInfo")]
        public static extern ComputeErrorCode StaticSetKernelExecInfo(CLKernelHandle kernel, ComputeExecInfo param_name, IntPtr param_value_size, IntPtr param_value);
        new public ComputeErrorCode SetKernelExecInfo(CLKernelHandle kernel, ComputeExecInfo param_name, IntPtr param_value_size, IntPtr param_value) { return StaticSetKernelExecInfo(kernel, param_name, param_value_size, param_value); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueSVMFree")]
        public static extern ComputeErrorCode StaticEnqueueSVMFree(CLCommandQueueHandle command_queue, Int32 num_svm_pointers, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] svm_pointers, ComputeFreeFunctionCallback pfn_free, IntPtr user_data, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueSVMFree(CLCommandQueueHandle command_queue, Int32 num_svm_pointers, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] svm_pointers, ComputeFreeFunctionCallback pfn_free, IntPtr user_data, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueSVMFree(command_queue, num_svm_pointers, svm_pointers, pfn_free, user_data, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueSVMMemcpy")]
        public static extern ComputeErrorCode StaticEnqueueSVMMemcpy(CLCommandQueueHandle command_queue, ComputeBoolean blocking_copy, IntPtr dst_ptr, IntPtr src_ptr, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueSVMMemcpy(CLCommandQueueHandle command_queue, ComputeBoolean blocking_copy, IntPtr dst_ptr, IntPtr src_ptr, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueSVMMemcpy(command_queue, blocking_copy, dst_ptr, src_ptr, size, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueSVMMemFill")]
        public static extern ComputeErrorCode StaticEnqueueSVMMemFill(CLCommandQueueHandle command_queue, IntPtr svm_ptr, IntPtr pattern, IntPtr pattern_size, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueSVMMemFill(CLCommandQueueHandle command_queue, IntPtr svm_ptr, IntPtr pattern, IntPtr pattern_size, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueSVMMemFill(command_queue, svm_ptr, pattern, pattern_size, size, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueSVMMap")]
        public static extern ComputeErrorCode StaticEnqueueSVMMap(CLCommandQueueHandle command_queue, ComputeBoolean blocking_map, ComputeMemoryMappingFlags flags, IntPtr svm_ptr, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueSVMMap(CLCommandQueueHandle command_queue, ComputeBoolean blocking_map, ComputeMemoryMappingFlags flags, IntPtr svm_ptr, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueSVMMap(command_queue, blocking_map, flags, svm_ptr, size, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueSVMUnMap")]
        public static extern ComputeErrorCode StaticEnqueueSVMUnMap(CLCommandQueueHandle command_queue, IntPtr svm_ptr, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueSVMUnMap(CLCommandQueueHandle command_queue, IntPtr svm_ptr, IntPtr size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueSVMUnMap(command_queue, svm_ptr, size, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 2.0.")]
        new CLCommandQueueHandle CreateCommandQueue(CLContextHandle context, CLDeviceHandle device, ComputeCommandQueueFlags properties, out ComputeErrorCode errcode_ret)
        {
            Trace.WriteLine("WARNING! GetExtensionFunctionAddress has been deprecated in OpenCL 1.2.");
            return CL12.StaticCreateCommandQueue(context, device, properties, out errcode_ret);
        }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 2.0.")]
        new CLSamplerHandle CreateSampler(CLContextHandle context, [MarshalAs(UnmanagedType.Bool)] bool normalized_coords, ComputeImageAddressing addressing_mode, ComputeImageFiltering filter_mode, out ComputeErrorCode errcode_ret)
        {
            Trace.WriteLine("WARNING! GetExtensionFunctionAddress has been deprecated in OpenCL 1.2.");
            return CL12.StaticCreateSampler(context, normalized_coords, addressing_mode, filter_mode, out errcode_ret);
        }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 2.0.")]
        new ComputeErrorCode EnqueueTask(CLCommandQueueHandle command_queue, CLKernelHandle kernel, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            Trace.WriteLine("WARNING! GetExtensionFunctionAddress has been deprecated in OpenCL 1.2.");
            return CL12.StaticEnqueueTask(command_queue, kernel, num_events_in_wait_list, event_wait_list, out new_event);
        }
    }
}
