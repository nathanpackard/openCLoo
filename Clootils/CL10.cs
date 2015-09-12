#region License

/*

Copyright (c) 2009 - 2011 Fatjon Sakiqi

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
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
    /// Contains bindings to the OpenCL 1.0 functions.
    /// </summary>
    /// <remarks> See the OpenCL specification for documentation regarding these functions. </remarks>
    [SuppressUnmanagedCodeSecurity]
    public class CL10 : ICL20
    {
        /// <summary>
        /// The name of the library that contains the available OpenCL function points.
        /// </summary>
        protected const string libName = "OpenCL.dll";

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetPlatformIDs")]
        public extern static ComputeErrorCode StaticGetPlatformIDs(Int32 num_entries, [Out, MarshalAs(UnmanagedType.LPArray)] CLPlatformHandle[] platforms, out Int32 num_platforms);
        public ComputeErrorCode GetPlatformIDs(Int32 num_entries, [Out, MarshalAs(UnmanagedType.LPArray)] CLPlatformHandle[] platforms, out Int32 num_platforms) { return StaticGetPlatformIDs(num_entries, platforms, out num_platforms); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetPlatformInfo")]
        public extern static ComputeErrorCode StaticGetPlatformInfo(CLPlatformHandle platform, ComputePlatformInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetPlatformInfo(CLPlatformHandle platform, ComputePlatformInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetPlatformInfo(platform, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetDeviceIDs")]
        public extern static ComputeErrorCode StaticGetDeviceIDs(CLPlatformHandle platform, ComputeDeviceTypes device_type, Int32 num_entries, [Out, MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices, out Int32 num_devices);
        public ComputeErrorCode GetDeviceIDs(CLPlatformHandle platform, ComputeDeviceTypes device_type, Int32 num_entries, [Out, MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices, out Int32 num_devices) { return StaticGetDeviceIDs(platform, device_type, num_entries, devices, out num_devices); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetDeviceInfo")]
        public extern static ComputeErrorCode StaticGetDeviceInfo(CLDeviceHandle device, ComputeDeviceInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetDeviceInfo(CLDeviceHandle device, ComputeDeviceInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetDeviceInfo(device, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateContext")]
        public extern static CLContextHandle StaticCreateContext([MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices, ComputeContextNotifier pfn_notify, IntPtr user_data, out ComputeErrorCode errcode_ret);
        public CLContextHandle CreateContext([MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] devices, ComputeContextNotifier pfn_notify, IntPtr user_data, out ComputeErrorCode errcode_ret) { return StaticCreateContext(properties, num_devices, devices, pfn_notify, user_data, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateContextFromType")]
        public extern static CLContextHandle StaticCreateContextFromType([MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, ComputeDeviceTypes device_type, ComputeContextNotifier pfn_notify, IntPtr user_data, out ComputeErrorCode errcode_ret);
        public CLContextHandle CreateContextFromType([MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, ComputeDeviceTypes device_type, ComputeContextNotifier pfn_notify, IntPtr user_data, out ComputeErrorCode errcode_ret) { return StaticCreateContextFromType(properties, device_type, pfn_notify, user_data, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clRetainContext")]
        public extern static ComputeErrorCode StaticRetainContext(CLContextHandle context);
        public ComputeErrorCode RetainContext(CLContextHandle context) { return StaticRetainContext(context); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clReleaseContext")]
        public extern static ComputeErrorCode StaticReleaseContext(CLContextHandle context);
        public ComputeErrorCode ReleaseContext(CLContextHandle context) { return StaticReleaseContext(context); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetContextInfo")]
        public extern static ComputeErrorCode StaticGetContextInfo(CLContextHandle context, ComputeContextInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetContextInfo(CLContextHandle context, ComputeContextInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetContextInfo(context, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateCommandQueue")]
        public extern static CLCommandQueueHandle StaticCreateCommandQueue(CLContextHandle context, CLDeviceHandle device, ComputeCommandQueueFlags properties, out ComputeErrorCode errcode_ret);
        public CLCommandQueueHandle CreateCommandQueue(CLContextHandle context, CLDeviceHandle device, ComputeCommandQueueFlags properties, out ComputeErrorCode errcode_ret) { return StaticCreateCommandQueue(context, device, properties, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clRetainCommandQueue")]
        public extern static ComputeErrorCode StaticRetainCommandQueue(CLCommandQueueHandle command_queue);
        public ComputeErrorCode RetainCommandQueue(CLCommandQueueHandle command_queue) { return StaticRetainCommandQueue(command_queue); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clReleaseCommandQueue")]
        public extern static ComputeErrorCode StaticReleaseCommandQueue(CLCommandQueueHandle command_queue);
        public ComputeErrorCode ReleaseCommandQueue(CLCommandQueueHandle command_queue) { return StaticReleaseCommandQueue(command_queue); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetCommandQueueInfo")]
        public extern static ComputeErrorCode StaticGetCommandQueueInfo(CLCommandQueueHandle command_queue, ComputeCommandQueueInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetCommandQueueInfo(CLCommandQueueHandle command_queue, ComputeCommandQueueInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetCommandQueueInfo(command_queue, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clSetCommandQueueProperty")]
        public extern static ComputeErrorCode StaticSetCommandQueueProperty(CLCommandQueueHandle command_queue, ComputeCommandQueueFlags properties, [MarshalAs(UnmanagedType.Bool)] bool enable, out ComputeCommandQueueFlags old_properties);
        public ComputeErrorCode SetCommandQueueProperty(CLCommandQueueHandle command_queue, ComputeCommandQueueFlags properties, [MarshalAs(UnmanagedType.Bool)] bool enable, out ComputeCommandQueueFlags old_properties) { return StaticSetCommandQueueProperty(command_queue, properties, enable, out old_properties); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateBuffer")]
        public extern static CLMemoryHandle StaticCreateBuffer(CLContextHandle context, ComputeMemoryFlags flags, IntPtr size, IntPtr host_ptr, out ComputeErrorCode errcode_ret);
        public CLMemoryHandle CreateBuffer(CLContextHandle context, ComputeMemoryFlags flags, IntPtr size, IntPtr host_ptr, out ComputeErrorCode errcode_ret) { return StaticCreateBuffer(context, flags, size, host_ptr, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateImage2D")]
        public extern static CLMemoryHandle StaticCreateImage2D(CLContextHandle context, ComputeMemoryFlags flags, ref ComputeImageFormat image_format, IntPtr image_width, IntPtr image_height, IntPtr image_row_pitch, IntPtr host_ptr, out ComputeErrorCode errcode_ret);
        public CLMemoryHandle CreateImage2D(CLContextHandle context, ComputeMemoryFlags flags, ref ComputeImageFormat image_format, IntPtr image_width, IntPtr image_height, IntPtr image_row_pitch, IntPtr host_ptr, out ComputeErrorCode errcode_ret) { return StaticCreateImage2D(context, flags,ref image_format, image_width, image_height, image_row_pitch, host_ptr, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateImage3D")]
        public extern static CLMemoryHandle StaticCreateImage3D(CLContextHandle context, ComputeMemoryFlags flags, ref ComputeImageFormat image_format, IntPtr image_width, IntPtr image_height, IntPtr image_depth, IntPtr image_row_pitch, IntPtr image_slice_pitch, IntPtr host_ptr, out ComputeErrorCode errcode_ret);
        public CLMemoryHandle CreateImage3D(CLContextHandle context, ComputeMemoryFlags flags, ref ComputeImageFormat image_format, IntPtr image_width, IntPtr image_height, IntPtr image_depth, IntPtr image_row_pitch, IntPtr image_slice_pitch, IntPtr host_ptr, out ComputeErrorCode errcode_ret) { return StaticCreateImage3D(context, flags, ref image_format, image_width, image_height, image_depth, image_row_pitch, image_slice_pitch, host_ptr, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clRetainMemObject")]
        public extern static ComputeErrorCode StaticRetainMemObject(CLMemoryHandle memobj);
        public ComputeErrorCode RetainMemObject(CLMemoryHandle memobj) { return StaticRetainMemObject(memobj); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clReleaseMemObject")]
        public extern static ComputeErrorCode StaticReleaseMemObject(CLMemoryHandle memobj);
        public ComputeErrorCode ReleaseMemObject(CLMemoryHandle memobj) { return StaticReleaseMemObject(memobj); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetSupportedImageFormats")]
        public extern static ComputeErrorCode StaticGetSupportedImageFormats(CLContextHandle context, ComputeMemoryFlags flags, ComputeMemoryType image_type, Int32 num_entries, [Out, MarshalAs(UnmanagedType.LPArray)] ComputeImageFormat[] image_formats, out Int32 num_image_formats);
        public ComputeErrorCode GetSupportedImageFormats(CLContextHandle context, ComputeMemoryFlags flags, ComputeMemoryType image_type, Int32 num_entries, [Out, MarshalAs(UnmanagedType.LPArray)] ComputeImageFormat[] image_formats, out Int32 num_image_formats) { return StaticGetSupportedImageFormats(context, flags, image_type, num_entries, image_formats, out num_image_formats); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetMemObjectInfo")]
        public extern static ComputeErrorCode StaticGetMemObjectInfo(CLMemoryHandle memobj, ComputeMemoryInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetMemObjectInfo(CLMemoryHandle memobj, ComputeMemoryInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetMemObjectInfo(memobj, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetImageInfo")]
        public extern static ComputeErrorCode StaticGetImageInfo(CLMemoryHandle image, ComputeImageInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetImageInfo(CLMemoryHandle image, ComputeImageInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetImageInfo(image, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateSampler")]
        public extern static CLSamplerHandle StaticCreateSampler(CLContextHandle context, [MarshalAs(UnmanagedType.Bool)] bool normalized_coords, ComputeImageAddressing addressing_mode, ComputeImageFiltering filter_mode, out ComputeErrorCode errcode_ret);
        public CLSamplerHandle CreateSampler(CLContextHandle context, [MarshalAs(UnmanagedType.Bool)] bool normalized_coords, ComputeImageAddressing addressing_mode, ComputeImageFiltering filter_mode, out ComputeErrorCode errcode_ret) { return StaticCreateSampler(context, normalized_coords, addressing_mode, filter_mode, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clRetainSampler")]
        public extern static ComputeErrorCode StaticRetainSampler(CLSamplerHandle sample);
        public ComputeErrorCode RetainSampler(CLSamplerHandle sample) { return StaticRetainSampler(sample); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clReleaseSampler")]
        public extern static ComputeErrorCode StaticReleaseSampler(CLSamplerHandle sample);
        public ComputeErrorCode ReleaseSampler(CLSamplerHandle sample) { return StaticReleaseSampler(sample); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetSamplerInfo")]
        public extern static ComputeErrorCode StaticGetSamplerInfo(CLSamplerHandle sample, ComputeSamplerInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetSamplerInfo(CLSamplerHandle sample, ComputeSamplerInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetSamplerInfo(sample, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateProgramWithSource")]
        public extern static CLProgramHandle StaticCreateProgramWithSource(CLContextHandle context, Int32 count, String[] strings, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths, out ComputeErrorCode errcode_ret);
        public CLProgramHandle CreateProgramWithSource(CLContextHandle context, Int32 count, String[] strings, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths, out ComputeErrorCode errcode_ret) { return StaticCreateProgramWithSource(context, count, strings, lengths, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateProgramWithBinary")]
        public extern static CLProgramHandle StaticCreateProgramWithBinary(CLContextHandle context, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] binaries, [MarshalAs(UnmanagedType.LPArray)] Int32[] binary_status, out ComputeErrorCode errcode_ret);
        public CLProgramHandle CreateProgramWithBinary(CLContextHandle context, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] binaries, [MarshalAs(UnmanagedType.LPArray)] Int32[] binary_status, out ComputeErrorCode errcode_ret) { return StaticCreateProgramWithBinary(context, num_devices, device_list, lengths, binaries, binary_status, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clRetainProgram")]
        public extern static ComputeErrorCode StaticRetainProgram(CLProgramHandle program);
        public ComputeErrorCode RetainProgram(CLProgramHandle program) { return StaticRetainProgram(program); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clReleaseProgram")]
        public extern static ComputeErrorCode StaticReleaseProgram(CLProgramHandle program);
        public ComputeErrorCode ReleaseProgram(CLProgramHandle program) { return StaticReleaseProgram(program); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clBuildProgram")]
        public extern static ComputeErrorCode StaticBuildProgram(CLProgramHandle program, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, String options, ComputeProgramBuildNotifier pfn_notify, IntPtr user_data);
        public ComputeErrorCode BuildProgram(CLProgramHandle program, Int32 num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, String options, ComputeProgramBuildNotifier pfn_notify, IntPtr user_data) { return StaticBuildProgram(program, num_devices, device_list, options, pfn_notify, user_data); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clUnloadCompiler")]
        public extern static ComputeErrorCode StaticUnloadCompiler();
        public ComputeErrorCode UnloadCompiler() { return StaticUnloadCompiler(); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetProgramInfo")]
        public extern static ComputeErrorCode StaticGetProgramInfo(CLProgramHandle program, ComputeProgramInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetProgramInfo(CLProgramHandle program, ComputeProgramInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetProgramInfo(program, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetProgramBuildInfo")]
        public extern static ComputeErrorCode StaticGetProgramBuildInfo(CLProgramHandle program, CLDeviceHandle device, ComputeProgramBuildInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetProgramBuildInfo(CLProgramHandle program, CLDeviceHandle device, ComputeProgramBuildInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetProgramBuildInfo(program, device, param_name, param_value_size, param_value,out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateKernel")]
        public extern static CLKernelHandle StaticCreateKernel(CLProgramHandle program, String kernel_name, out ComputeErrorCode errcode_ret);
        public CLKernelHandle CreateKernel(CLProgramHandle program, String kernel_name, out ComputeErrorCode errcode_ret) { return StaticCreateKernel(program, kernel_name, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateKernelsInProgram")]
        public extern static ComputeErrorCode StaticCreateKernelsInProgram(CLProgramHandle program, Int32 num_kernels, [Out, MarshalAs(UnmanagedType.LPArray)] CLKernelHandle[] kernels, out Int32 num_kernels_ret);
        public ComputeErrorCode CreateKernelsInProgram(CLProgramHandle program, Int32 num_kernels, [Out, MarshalAs(UnmanagedType.LPArray)] CLKernelHandle[] kernels, out Int32 num_kernels_ret) { return StaticCreateKernelsInProgram(program, num_kernels, kernels, out num_kernels_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clRetainKernel")]
        public extern static ComputeErrorCode StaticRetainKernel(CLKernelHandle kernel);
        public ComputeErrorCode RetainKernel(CLKernelHandle kernel) { return StaticRetainKernel(kernel); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clReleaseKernel")]
        public extern static ComputeErrorCode StaticReleaseKernel(CLKernelHandle kernel);
        public ComputeErrorCode ReleaseKernel(CLKernelHandle kernel) { return StaticReleaseKernel(kernel); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clSetKernelArg")]
        public extern static ComputeErrorCode StaticSetKernelArg(CLKernelHandle kernel, Int32 arg_index, IntPtr arg_size, IntPtr arg_value);
        public ComputeErrorCode SetKernelArg(CLKernelHandle kernel, Int32 arg_index, IntPtr arg_size, IntPtr arg_value) { return StaticSetKernelArg(kernel, arg_index, arg_size, arg_value); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetKernelInfo")]
        public extern static ComputeErrorCode StaticGetKernelInfo(CLKernelHandle kernel, ComputeKernelInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetKernelInfo(CLKernelHandle kernel, ComputeKernelInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetKernelInfo(kernel, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetKernelWorkGroupInfo")]
        public extern static ComputeErrorCode StaticGetKernelWorkGroupInfo(CLKernelHandle kernel, CLDeviceHandle device, ComputeKernelWorkGroupInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetKernelWorkGroupInfo(CLKernelHandle kernel, CLDeviceHandle device, ComputeKernelWorkGroupInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetKernelWorkGroupInfo(kernel, device, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clWaitForEvents")]
        public extern static ComputeErrorCode StaticWaitForEvents(Int32 num_events, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list);
        public ComputeErrorCode WaitForEvents(Int32 num_events, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list) { return StaticWaitForEvents(num_events, event_list); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetEventInfo")]
        public extern static ComputeErrorCode StaticGetEventInfo(CLEventHandle @event, ComputeEventInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetEventInfo(CLEventHandle @event, ComputeEventInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetEventInfo(@event, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clRetainEvent")]
        public extern static ComputeErrorCode StaticRetainEvent(CLEventHandle @event);
        public ComputeErrorCode RetainEvent(CLEventHandle @event) { return StaticRetainEvent(@event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clReleaseEvent")]
        public extern static ComputeErrorCode StaticReleaseEvent(CLEventHandle @event);
        public ComputeErrorCode ReleaseEvent(CLEventHandle @event) { return StaticReleaseEvent(@event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetEventProfilingInfo")]
        public extern static ComputeErrorCode StaticGetEventProfilingInfo(CLEventHandle @event, ComputeCommandProfilingInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetEventProfilingInfo(CLEventHandle @event, ComputeCommandProfilingInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetEventProfilingInfo(@event, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clFlush")]
        public extern static ComputeErrorCode StaticFlush(CLCommandQueueHandle command_queue);
        public ComputeErrorCode Flush(CLCommandQueueHandle command_queue) { return StaticFlush(command_queue); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clFinish")]
        public extern static ComputeErrorCode StaticFinish(CLCommandQueueHandle command_queue);
        public ComputeErrorCode Finish(CLCommandQueueHandle command_queue) { return StaticFinish(command_queue); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueReadBuffer")]
        public extern static ComputeErrorCode StaticEnqueueReadBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_read, IntPtr offset, IntPtr cb, IntPtr ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueReadBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_read, IntPtr offset, IntPtr cb, IntPtr ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueReadBuffer(command_queue, buffer, blocking_read, offset, cb, ptr, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueWriteBuffer")]
        public extern static ComputeErrorCode StaticEnqueueWriteBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_write, IntPtr offset, IntPtr cb, IntPtr ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueWriteBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_write, IntPtr offset, IntPtr cb, IntPtr ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueWriteBuffer(command_queue, buffer, blocking_write, offset, cb, ptr, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueCopyBuffer")]
        public extern static ComputeErrorCode StaticEnqueueCopyBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle src_buffer, CLMemoryHandle dst_buffer, IntPtr src_offset, IntPtr dst_offset, IntPtr cb, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueCopyBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle src_buffer, CLMemoryHandle dst_buffer, IntPtr src_offset, IntPtr dst_offset, IntPtr cb, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueCopyBuffer(command_queue, src_buffer, dst_buffer, src_offset, dst_offset, cb, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueReadImage")]
        public extern static ComputeErrorCode StaticEnqueueReadImage(CLCommandQueueHandle command_queue, CLMemoryHandle image, [MarshalAs(UnmanagedType.Bool)] bool blocking_read, ref SysIntX3 origin, ref SysIntX3 region, IntPtr row_pitch, IntPtr slice_pitch, IntPtr ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueReadImage(CLCommandQueueHandle command_queue, CLMemoryHandle image, [MarshalAs(UnmanagedType.Bool)] bool blocking_read, ref SysIntX3 origin, ref SysIntX3 region, IntPtr row_pitch, IntPtr slice_pitch, IntPtr ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueReadImage(command_queue, image, blocking_read, ref origin, ref region, row_pitch, slice_pitch, ptr, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueWriteImage")]
        public extern static ComputeErrorCode StaticEnqueueWriteImage(CLCommandQueueHandle command_queue, CLMemoryHandle image, [MarshalAs(UnmanagedType.Bool)] bool blocking_write, ref SysIntX3 origin, ref SysIntX3 region, IntPtr input_row_pitch, IntPtr input_slice_pitch, IntPtr ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueWriteImage(CLCommandQueueHandle command_queue, CLMemoryHandle image, [MarshalAs(UnmanagedType.Bool)] bool blocking_write, ref SysIntX3 origin, ref SysIntX3 region, IntPtr input_row_pitch, IntPtr input_slice_pitch, IntPtr ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueWriteImage(command_queue, image, blocking_write, ref origin, ref region, input_row_pitch, input_slice_pitch, ptr, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueCopyImage")]
        public extern static ComputeErrorCode StaticEnqueueCopyImage(CLCommandQueueHandle command_queue, CLMemoryHandle src_image, CLMemoryHandle dst_image, ref SysIntX3 src_origin, ref SysIntX3 dst_origin, ref SysIntX3 region, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueCopyImage(CLCommandQueueHandle command_queue, CLMemoryHandle src_image, CLMemoryHandle dst_image, ref SysIntX3 src_origin, ref SysIntX3 dst_origin, ref SysIntX3 region, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueCopyImage(command_queue, src_image, dst_image, ref src_origin, ref dst_origin,ref region, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueCopyImageToBuffer")]
        public extern static ComputeErrorCode StaticEnqueueCopyImageToBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle src_image, CLMemoryHandle dst_buffer, ref SysIntX3 src_origin, ref SysIntX3 region, IntPtr dst_offset, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueCopyImageToBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle src_image, CLMemoryHandle dst_buffer, ref SysIntX3 src_origin, ref SysIntX3 region, IntPtr dst_offset, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueCopyImageToBuffer(command_queue, src_image, dst_buffer, ref src_origin, ref region, dst_offset, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueCopyBufferToImage")]
        public extern static ComputeErrorCode StaticEnqueueCopyBufferToImage(CLCommandQueueHandle command_queue, CLMemoryHandle src_buffer, CLMemoryHandle dst_image, IntPtr src_offset, ref SysIntX3 dst_origin, ref SysIntX3 region, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueCopyBufferToImage(CLCommandQueueHandle command_queue, CLMemoryHandle src_buffer, CLMemoryHandle dst_image, IntPtr src_offset, ref SysIntX3 dst_origin, ref SysIntX3 region, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueCopyBufferToImage(command_queue, src_buffer, dst_image, src_offset, ref dst_origin, ref region, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueMapBuffer")]
        public extern static IntPtr StaticEnqueueMapBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_map, ComputeMemoryMappingFlags map_flags, IntPtr offset, IntPtr cb, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event, out ComputeErrorCode errcode_ret);
        public IntPtr EnqueueMapBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_map, ComputeMemoryMappingFlags map_flags, IntPtr offset, IntPtr cb, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event, out ComputeErrorCode errcode_ret) { return StaticEnqueueMapBuffer(command_queue, buffer, blocking_map, map_flags, offset, cb, num_events_in_wait_list, event_wait_list, out new_event, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueMapImage")]
        public extern static IntPtr StaticEnqueueMapImage(CLCommandQueueHandle command_queue, CLMemoryHandle image, [MarshalAs(UnmanagedType.Bool)] bool blocking_map, ComputeMemoryMappingFlags map_flags, ref SysIntX3 origin, ref SysIntX3 region, out IntPtr image_row_pitch, out IntPtr image_slice_pitch, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event, out ComputeErrorCode errcode_ret);
        public IntPtr EnqueueMapImage(CLCommandQueueHandle command_queue, CLMemoryHandle image, [MarshalAs(UnmanagedType.Bool)] bool blocking_map, ComputeMemoryMappingFlags map_flags, ref SysIntX3 origin, ref SysIntX3 region, out IntPtr image_row_pitch, out IntPtr image_slice_pitch, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event, out ComputeErrorCode errcode_ret) { return StaticEnqueueMapImage(command_queue, image, blocking_map, map_flags, ref origin, ref region, out image_row_pitch, out image_slice_pitch, num_events_in_wait_list, event_wait_list, out new_event, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueUnmapMemObject")]
        public extern static ComputeErrorCode StaticEnqueueUnmapMemObject(CLCommandQueueHandle command_queue, CLMemoryHandle memobj, IntPtr mapped_ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueUnmapMemObject(CLCommandQueueHandle command_queue, CLMemoryHandle memobj, IntPtr mapped_ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueUnmapMemObject(command_queue, memobj, mapped_ptr, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueNDRangeKernel")]
        public extern static ComputeErrorCode StaticEnqueueNDRangeKernel(CLCommandQueueHandle command_queue, CLKernelHandle kernel, Int32 work_dim, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] global_work_offset, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] global_work_size, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] local_work_size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueNDRangeKernel(CLCommandQueueHandle command_queue, CLKernelHandle kernel, Int32 work_dim, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] global_work_offset, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] global_work_size, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] local_work_size, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueNDRangeKernel(command_queue, kernel, work_dim, global_work_offset, global_work_size, local_work_size, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueTask")]
        public extern static ComputeErrorCode StaticEnqueueTask(CLCommandQueueHandle command_queue, CLKernelHandle kernel, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueTask(CLCommandQueueHandle command_queue, CLKernelHandle kernel, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueTask(command_queue, kernel, num_events_in_wait_list, event_wait_list, out new_event); }

        // <summary>
        // See the OpenCL specification.
        // </summary>
        /*
        [DllImport(libName, EntryPoint = "clEnqueueNativeKernel")]
        public extern static ComputeErrorCode EnqueueNativeKernel(            CLCommandQueueHandle command_queue,            IntPtr user_func,            IntPtr args,            IntPtr cb_args,            Int32 num_mem_objects,            IntPtr* mem_list,            IntPtr* args_mem_loc,            Int32 num_events_in_wait_list,            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event);
        */

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueMarker")]
        public extern static ComputeErrorCode StaticEnqueueMarker(CLCommandQueueHandle command_queue, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueMarker(CLCommandQueueHandle command_queue, out CLEventHandle new_event) { return StaticEnqueueMarker(command_queue, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueWaitForEvents")]
        public extern static ComputeErrorCode StaticEnqueueWaitForEvents(CLCommandQueueHandle command_queue, Int32 num_events, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list);
        public ComputeErrorCode EnqueueWaitForEvents(CLCommandQueueHandle command_queue, Int32 num_events, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_list) { return StaticEnqueueWaitForEvents(command_queue, num_events, event_list); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueBarrier")]
        public extern static ComputeErrorCode StaticEnqueueBarrier(CLCommandQueueHandle command_queue);
        public ComputeErrorCode EnqueueBarrier(CLCommandQueueHandle command_queue) { return StaticEnqueueBarrier(command_queue); }

        /// <summary>
        /// Gets the extension function address for the given function name,        /// or NULL if a valid function can not be found. The client must
        /// check to make sure the address is not NULL, before using or 
        /// calling the returned function address.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetExtensionFunctionAddress")]
        public extern static IntPtr StaticGetExtensionFunctionAddress(String func_name);
        public IntPtr GetExtensionFunctionAddress(String func_name) { return StaticGetExtensionFunctionAddress(func_name); }

        /**************************************************************************************/
        // CL/GL Sharing API

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateFromGLBuffer")]
        public extern static CLMemoryHandle StaticCreateFromGLBuffer(CLContextHandle context, ComputeMemoryFlags flags, Int32 bufobj, out ComputeErrorCode errcode_ret);
        public CLMemoryHandle CreateFromGLBuffer(CLContextHandle context, ComputeMemoryFlags flags, Int32 bufobj, out ComputeErrorCode errcode_ret) { return StaticCreateFromGLBuffer(context, flags, bufobj, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateFromGLTexture2D")]
        public extern static CLMemoryHandle StaticCreateFromGLTexture2D(CLContextHandle context, ComputeMemoryFlags flags, Int32 target, Int32 miplevel, Int32 texture, out ComputeErrorCode errcode_ret);
        public CLMemoryHandle CreateFromGLTexture2D(CLContextHandle context, ComputeMemoryFlags flags, Int32 target, Int32 miplevel, Int32 texture, out ComputeErrorCode errcode_ret) { return StaticCreateFromGLTexture2D(context, flags, target, miplevel, texture, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateFromGLTexture3D")]
        public extern static CLMemoryHandle StaticCreateFromGLTexture3D(CLContextHandle context, ComputeMemoryFlags flags, Int32 target, Int32 miplevel, Int32 texture, out ComputeErrorCode errcode_ret);
        public CLMemoryHandle CreateFromGLTexture3D(CLContextHandle context, ComputeMemoryFlags flags, Int32 target, Int32 miplevel, Int32 texture, out ComputeErrorCode errcode_ret) { return StaticCreateFromGLTexture3D(context, flags, target, miplevel, texture, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateFromGLRenderbuffer")]
        public extern static CLMemoryHandle StaticCreateFromGLRenderbuffer(CLContextHandle context, ComputeMemoryFlags flags, Int32 renderbuffer, out ComputeErrorCode errcode_ret);
        public CLMemoryHandle CreateFromGLRenderbuffer(CLContextHandle context, ComputeMemoryFlags flags, Int32 renderbuffer, out ComputeErrorCode errcode_ret) { return StaticCreateFromGLRenderbuffer(context, flags, renderbuffer, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetGLObjectInfo")]
        public extern static ComputeErrorCode StaticGetGLObjectInfo(CLMemoryHandle memobj, out ComputeGLObjectType gl_object_type, out Int32 gl_object_name);
        public ComputeErrorCode GetGLObjectInfo(CLMemoryHandle memobj, out ComputeGLObjectType gl_object_type, out Int32 gl_object_name) { return StaticGetGLObjectInfo(memobj, out gl_object_type, out gl_object_name); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clGetGLTextureInfo")]
        public extern static ComputeErrorCode StaticGetGLTextureInfo(CLMemoryHandle memobj, ComputeGLTextureInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret);
        public ComputeErrorCode GetGLTextureInfo(CLMemoryHandle memobj, ComputeGLTextureInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret) { return StaticGetGLTextureInfo(memobj, param_name, param_value_size, param_value, out param_value_size_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueAcquireGLObjects")]
        public extern static ComputeErrorCode StaticEnqueueAcquireGLObjects(CLCommandQueueHandle command_queue, Int32 num_objects, [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueAcquireGLObjects(CLCommandQueueHandle command_queue, Int32 num_objects, [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueAcquireGLObjects(command_queue, num_objects, mem_objects, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueReleaseGLObjects")]
        public extern static ComputeErrorCode StaticEnqueueReleaseGLObjects(CLCommandQueueHandle command_queue, Int32 num_objects, [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        public ComputeErrorCode EnqueueReleaseGLObjects(CLCommandQueueHandle command_queue, Int32 num_objects, [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueReleaseGLObjects(command_queue, num_objects, mem_objects, num_events_in_wait_list, event_wait_list, out new_event); }

        public CLMemoryHandle CreateSubBuffer(CLMemoryHandle buffer, ComputeMemoryFlags flags, ComputeBufferCreateType buffer_create_type, ref SysIntX2 buffer_create_info, out ComputeErrorCode errcode_ret)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode SetMemObjectDestructorCallback(CLMemoryHandle memobj, ComputeMemoryDestructorNotifer pfn_notify, IntPtr user_data)
        {
            throw new NotImplementedException();
        }

        public CLEventHandle CreateUserEvent(CLContextHandle context, out ComputeErrorCode errcode_ret)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode SetUserEventStatus(CLEventHandle @event, int execution_status)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode SetEventCallback(CLEventHandle @event, int command_exec_callback_type, ComputeEventCallback pfn_notify, IntPtr user_data)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode CreateSubDevices(CLDeviceHandle device, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, int num_devices, [MarshalAs(UnmanagedType.LPArray), Out] CLDeviceHandle[] devices, out int num_devices_ret)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode RetainDevice(CLDeviceHandle device)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode ReleaseDevice(CLDeviceHandle device)
        {
            throw new NotImplementedException();
        }

        public CLMemoryHandle CreateImage(CLContextHandle context, ComputeMemoryFlags flags, ref ComputeImageFormat image_format, ref ComputeImageDescription image_desc, IntPtr host_ptr, out ComputeErrorCode errcode_ret)
        {
            throw new NotImplementedException();
        }

        public CLProgramHandle CreateProgramWithBuiltInKernels(CLContextHandle context, int num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, string kernel_names, out ComputeErrorCode errcode_ret)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode CompileProgram(CLProgramHandle program, int num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, string options, int num_input_headers, [MarshalAs(UnmanagedType.LPArray)] CLProgramHandle[] input_headers, string[] header_include_names, ComputeProgramBuildNotifier pfn_notify, IntPtr user_data)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode LinkProgram(CLContextHandle context, int num_devices, [MarshalAs(UnmanagedType.LPArray)] CLDeviceHandle[] device_list, string options, int num_input_programs, ComputeProgramBuildNotifier pfn_notify, IntPtr user_data, out ComputeErrorCode errcode_ret)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode UnloadPlatformCompiler(CLProgramHandle program)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode GetKernelArgInfo(CLKernelHandle kernel, int arg_indx, ComputeKernelArgInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret)
        {
            throw new NotImplementedException();
        }

        public IntPtr GetExtensionFunctionAddressForPlatform(CLPlatformHandle platform, string func_name)
        {
            throw new NotImplementedException();
        }

        public CLMemoryHandle CreateFromGLTexture(CLContextHandle context, ComputeMemoryFlags flags, int texture_target, int miplevel, int texture, out ComputeErrorCode errcode_ret)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueFillBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, IntPtr pattern, IntPtr pattern_size, IntPtr offset, IntPtr size, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueFillImage(CLCommandQueueHandle command_queue, CLMemoryHandle image, IntPtr fill_color, ref SysIntX3 origin, ref SysIntX3 region, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueMigrateMemObjects(CLCommandQueueHandle command_queue, int num_mem_objects, [MarshalAs(UnmanagedType.LPArray)] CLMemoryHandle[] mem_objects, ComputeMemoryMigrationFlags flags, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueMarkerWithWaitList(CLCommandQueueHandle command_queue, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueBarrierWithWaitList(CLCommandQueueHandle command_queue, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueWriteBuffer(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_write, IntPtr offset, IntPtr cb, IntPtr ptr, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1), Out] CLEventHandle[] new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueReadBufferRect(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_read, ref SysIntX3 buffer_offset, ref SysIntX3 host_offset, ref SysIntX3 region, IntPtr buffer_row_pitch, IntPtr buffer_slice_pitch, IntPtr host_row_pitch, IntPtr host_slice_pitch, IntPtr ptr, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1), Out] CLEventHandle[] new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueWriteBufferRect(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_write, ref SysIntX3 buffer_offset, ref SysIntX3 host_offset, ref SysIntX3 region, IntPtr buffer_row_pitch, IntPtr buffer_slice_pitch, IntPtr host_row_pitch, IntPtr host_slice_pitch, IntPtr ptr, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1), Out] CLEventHandle[] new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueCopyBufferRect(CLCommandQueueHandle command_queue, CLMemoryHandle src_buffer, CLMemoryHandle dst_buffer, ref SysIntX3 src_origin, ref SysIntX3 dst_origin, ref SysIntX3 region, IntPtr src_row_pitch, IntPtr src_slice_pitch, IntPtr dst_row_pitch, IntPtr dst_slice_pitch, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1), Out] CLEventHandle[] new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueReadBufferRect(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_read, ref SysIntX3 buffer_offset, ref SysIntX3 host_offset, ref SysIntX3 region, IntPtr buffer_row_pitch, IntPtr buffer_slice_pitch, IntPtr host_row_pitch, IntPtr host_slice_pitch, IntPtr ptr, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueWriteBufferRect(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_write, ref SysIntX3 buffer_offset, ref SysIntX3 host_offset, ref SysIntX3 region, IntPtr buffer_row_pitch, IntPtr buffer_slice_pitch, IntPtr host_row_pitch, IntPtr host_slice_pitch, IntPtr ptr, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueCopyBufferRect(CLCommandQueueHandle command_queue, CLMemoryHandle src_buffer, CLMemoryHandle dst_buffer, ref SysIntX3 src_origin, ref SysIntX3 dst_origin, ref SysIntX3 region, IntPtr src_row_pitch, IntPtr src_slice_pitch, IntPtr dst_row_pitch, IntPtr dst_slice_pitch, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueWriteImage(CLCommandQueueHandle command_queue, CLMemoryHandle image, [MarshalAs(UnmanagedType.Bool)] bool blocking_write, ref SysIntX3 origin, ref SysIntX3 region, IntPtr input_row_pitch, IntPtr input_slice_pitch, IntPtr ptr, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public CLCommandQueueHandle CreateCommandQueueWithProperties(CLContextHandle context, CLDeviceHandle device, ComputeCommandQueueFlags properties, out ComputeErrorCode errcode_ret)
        {
            throw new NotImplementedException();
        }

        public CLMemoryHandle CreatePipe(CLContextHandle context, ComputeMemoryFlags flags, int pipe_packet_size, int pipe_max_packets, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, out ComputeErrorCode errcode_ret)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode GetPipeInfo(CLMemoryHandle pipe, ComputePipeInfo param_name, IntPtr param_value_size, IntPtr param_value, out IntPtr param_value_size_ret)
        {
            throw new NotImplementedException();
        }

        public CLMemoryHandle ComputeSvmAlloc(CLContextHandle context, ComputeMemoryFlags flags, IntPtr size, int alignment)
        {
            throw new NotImplementedException();
        }

        public void ComputeSvmFree(CLContextHandle context, CLMemoryHandle svm_pointer)
        {
            throw new NotImplementedException();
        }

        public CLSamplerHandle CreateSamplerWithProperties(CLContextHandle context, [MarshalAs(UnmanagedType.LPArray)] ComputeSamplerInfo[] normalized_coords, out ComputeErrorCode errcode_ret)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode SetKernelArgSvmPointer(CLKernelHandle kernel, int arg_index, IntPtr arg_value)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode SetKernelExecInfo(CLKernelHandle kernel, ComputeExecInfo param_name, IntPtr param_value_size, IntPtr param_value)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueSVMFree(CLCommandQueueHandle command_queue, int num_svm_pointers, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] svm_pointers, ComputeFreeFunctionCallback pfn_free, IntPtr user_data, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueSVMMemcpy(CLCommandQueueHandle command_queue, ComputeBoolean blocking_copy, IntPtr dst_ptr, IntPtr src_ptr, IntPtr size, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueSVMMemFill(CLCommandQueueHandle command_queue, IntPtr svm_ptr, IntPtr pattern, IntPtr pattern_size, IntPtr size, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueSVMMap(CLCommandQueueHandle command_queue, ComputeBoolean blocking_map, ComputeMemoryMappingFlags flags, IntPtr svm_ptr, IntPtr size, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

        public ComputeErrorCode EnqueueSVMUnMap(CLCommandQueueHandle command_queue, IntPtr svm_ptr, IntPtr size, int num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event)
        {
            throw new NotImplementedException();
        }

    }
}