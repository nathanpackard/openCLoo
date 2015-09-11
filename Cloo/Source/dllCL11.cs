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
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Security;

    /// <summary>
    /// Contains bindings to the OpenCL 1.1 functions.
    /// </summary>
    /// <remarks> See the OpenCL specification for documentation regarding these functions. </remarks>
    [SuppressUnmanagedCodeSecurity]
    public class CL11 : CL10, ICL20
    {
        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateSubBuffer")]
        public extern static CLMemoryHandle StaticCreateSubBuffer(CLMemoryHandle buffer, ComputeMemoryFlags flags, ComputeBufferCreateType buffer_create_type, ref SysIntX2 buffer_create_info, out ComputeErrorCode errcode_ret);
        new public CLMemoryHandle CreateSubBuffer(CLMemoryHandle buffer, ComputeMemoryFlags flags, ComputeBufferCreateType buffer_create_type, ref SysIntX2 buffer_create_info, out ComputeErrorCode errcode_ret) { return StaticCreateSubBuffer(buffer, flags, buffer_create_type, ref buffer_create_info, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clSetMemObjectDestructorCallback")]
        public extern static ComputeErrorCode StaticSetMemObjectDestructorCallback(CLMemoryHandle memobj, ComputeMemoryDestructorNotifer pfn_notify, IntPtr user_data);
        new public ComputeErrorCode SetMemObjectDestructorCallback(CLMemoryHandle memobj, ComputeMemoryDestructorNotifer pfn_notify, IntPtr user_data) { return StaticSetMemObjectDestructorCallback(memobj, pfn_notify, user_data); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clCreateUserEvent")]
        public extern static CLEventHandle StaticCreateUserEvent(CLContextHandle context, out ComputeErrorCode errcode_ret);
        new public CLEventHandle CreateUserEvent(CLContextHandle context, out ComputeErrorCode errcode_ret) { return StaticCreateUserEvent(context, out errcode_ret); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clSetUserEventStatus")]
        public extern static ComputeErrorCode StaticSetUserEventStatus(CLEventHandle @event, Int32 execution_status);
        new public ComputeErrorCode SetUserEventStatus(CLEventHandle @event, Int32 execution_status) { return StaticSetUserEventStatus(@event, execution_status); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clSetEventCallback")]
        public extern static ComputeErrorCode StaticSetEventCallback(CLEventHandle @event, Int32 command_exec_callback_type, ComputeEventCallback pfn_notify, IntPtr user_data);
        new public ComputeErrorCode SetEventCallback(CLEventHandle @event, Int32 command_exec_callback_type, ComputeEventCallback pfn_notify, IntPtr user_data) { return StaticSetEventCallback(@event, command_exec_callback_type, pfn_notify, user_data); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueReadBufferRect")]
        public extern static ComputeErrorCode StaticEnqueueReadBufferRect(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_read, ref SysIntX3 buffer_offset, ref SysIntX3 host_offset, ref SysIntX3 region, IntPtr buffer_row_pitch, IntPtr buffer_slice_pitch, IntPtr host_row_pitch, IntPtr host_slice_pitch, IntPtr ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueReadBufferRect(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_read, ref SysIntX3 buffer_offset, ref SysIntX3 host_offset, ref SysIntX3 region, IntPtr buffer_row_pitch, IntPtr buffer_slice_pitch, IntPtr host_row_pitch, IntPtr host_slice_pitch, IntPtr ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueReadBufferRect(command_queue, buffer, blocking_read, ref buffer_offset, ref host_offset, ref region, buffer_row_pitch, buffer_slice_pitch, host_row_pitch, host_slice_pitch, ptr, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueWriteBufferRect")]
        public extern static ComputeErrorCode StaticEnqueueWriteBufferRect(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_write, ref SysIntX3 buffer_offset, ref SysIntX3 host_offset, ref SysIntX3 region, IntPtr buffer_row_pitch, IntPtr buffer_slice_pitch, IntPtr host_row_pitch, IntPtr host_slice_pitch, IntPtr ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueWriteBufferRect(CLCommandQueueHandle command_queue, CLMemoryHandle buffer, [MarshalAs(UnmanagedType.Bool)] bool blocking_write, ref SysIntX3 buffer_offset, ref SysIntX3 host_offset, ref SysIntX3 region, IntPtr buffer_row_pitch, IntPtr buffer_slice_pitch, IntPtr host_row_pitch, IntPtr host_slice_pitch, IntPtr ptr, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueWriteBufferRect(command_queue, buffer, blocking_write, ref buffer_offset, ref host_offset, ref region, buffer_row_pitch, buffer_slice_pitch, host_row_pitch, host_slice_pitch, ptr, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [DllImport(libName, EntryPoint = "clEnqueueCopyBufferRect")]
        public extern static ComputeErrorCode StaticEnqueueCopyBufferRect(CLCommandQueueHandle command_queue, CLMemoryHandle src_buffer, CLMemoryHandle dst_buffer, ref SysIntX3 src_origin, ref SysIntX3 dst_origin, ref SysIntX3 region, IntPtr src_row_pitch, IntPtr src_slice_pitch, IntPtr dst_row_pitch, IntPtr dst_slice_pitch, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event);
        new public ComputeErrorCode EnqueueCopyBufferRect(CLCommandQueueHandle command_queue, CLMemoryHandle src_buffer, CLMemoryHandle dst_buffer, ref SysIntX3 src_origin, ref SysIntX3 dst_origin, ref SysIntX3 region, IntPtr src_row_pitch, IntPtr src_slice_pitch, IntPtr dst_row_pitch, IntPtr dst_slice_pitch, Int32 num_events_in_wait_list, [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list, out CLEventHandle new_event) { return StaticEnqueueCopyBufferRect(command_queue, src_buffer, dst_buffer, ref src_origin, ref dst_origin, ref region, src_row_pitch, src_slice_pitch, dst_row_pitch, dst_slice_pitch, num_events_in_wait_list, event_wait_list, out new_event); }

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("This function has been deprecated in OpenCL 1.1.")]
        new public ComputeErrorCode SetCommandQueueProperty(CLCommandQueueHandle command_queue, ComputeCommandQueueFlags properties, [MarshalAs(UnmanagedType.Bool)] bool enable, out ComputeCommandQueueFlags old_properties)
        {
            Trace.WriteLine("WARNING! clSetCommandQueueProperty has been deprecated in OpenCL 1.1.");
            return CL10.StaticSetCommandQueueProperty(command_queue, properties, enable, out old_properties);
        }
    }
}