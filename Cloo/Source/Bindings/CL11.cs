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
    /// Contains bindings to the OpenCL 1.1 functions.
    /// </summary>
    /// <remarks> See the OpenCL specification for documentation regarding these functions. </remarks>
    [SuppressUnmanagedCodeSecurity]
    public class CL11 : CL10
    {
        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLMemoryHandle CreateSubBuffer(
            CLMemoryHandle buffer,
            ComputeMemoryFlags flags,
            ComputeBufferCreateType buffer_create_type,
            ref SysIntX2 buffer_create_info,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateSubBuffer_Mac(buffer, flags, buffer_create_type, ref buffer_create_info,
                                                 out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateSubBuffer_Windows(buffer, flags, buffer_create_type, ref buffer_create_info,
                                                     out errcode_ret);
                default:
                    return clCreateSubBuffer_Unix(buffer, flags, buffer_create_type, ref buffer_create_info,
                                                  out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateSubBuffer")]
        extern static CLMemoryHandle clCreateSubBuffer_Mac(
            CLMemoryHandle buffer,
            ComputeMemoryFlags flags,
            ComputeBufferCreateType buffer_create_type,
            ref SysIntX2 buffer_create_info,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateSubBuffer")]
        extern static CLMemoryHandle clCreateSubBuffer_Windows(
            CLMemoryHandle buffer,
            ComputeMemoryFlags flags,
            ComputeBufferCreateType buffer_create_type,
            ref SysIntX2 buffer_create_info,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateSubBuffer")]
        extern static CLMemoryHandle clCreateSubBuffer_Unix(
            CLMemoryHandle buffer,
            ComputeMemoryFlags flags,
            ComputeBufferCreateType buffer_create_type,
            ref SysIntX2 buffer_create_info,
            out ComputeErrorCode errcode_ret);


        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode SetMemObjectDestructorCallback( 
            CLMemoryHandle memobj, 
            ComputeMemoryDestructorNotifer pfn_notify, 
            IntPtr user_data)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clSetMemObjectDestructorCallback_Mac(memobj, pfn_notify, user_data);
                case BindingPlatform.Windows:
                    return clSetMemObjectDestructorCallback_Windows(memobj, pfn_notify, user_data);
                default:
                    return clSetMemObjectDestructorCallback_Unix(memobj, pfn_notify, user_data);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clSetMemObjectDestructorCallback")]
        extern static ComputeErrorCode clSetMemObjectDestructorCallback_Mac(
            CLMemoryHandle memobj,
            ComputeMemoryDestructorNotifer pfn_notify,
            IntPtr user_data);
        [DllImport(libNameWindows, EntryPoint = "clSetMemObjectDestructorCallback")]
        extern static ComputeErrorCode clSetMemObjectDestructorCallback_Windows(
            CLMemoryHandle memobj,
            ComputeMemoryDestructorNotifer pfn_notify,
            IntPtr user_data);
        [DllImport(libNameUnix, EntryPoint = "clSetMemObjectDestructorCallback")]
        extern static ComputeErrorCode clSetMemObjectDestructorCallback_Unix(
            CLMemoryHandle memobj,
            ComputeMemoryDestructorNotifer pfn_notify,
            IntPtr user_data);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static CLEventHandle CreateUserEvent(
            CLContextHandle context,
            out ComputeErrorCode errcode_ret)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clCreateUserEvent_Mac(context, out errcode_ret);
                case BindingPlatform.Windows:
                    return clCreateUserEvent_Windows(context, out errcode_ret);
                default:
                    return clCreateUserEvent_Unix(context, out errcode_ret);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clCreateUserEvent")]
        extern static CLEventHandle clCreateUserEvent_Mac(
            CLContextHandle context,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameWindows, EntryPoint = "clCreateUserEvent")]
        extern static CLEventHandle clCreateUserEvent_Windows(
            CLContextHandle context,
            out ComputeErrorCode errcode_ret);
        [DllImport(libNameUnix, EntryPoint = "clCreateUserEvent")]
        extern static CLEventHandle clCreateUserEvent_Unix(
            CLContextHandle context,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode SetUserEventStatus(
            CLEventHandle @event,
            Int32 execution_status)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clSetUserEventStatus_Mac(@event, execution_status);
                case BindingPlatform.Windows:
                    return clSetUserEventStatus_Windows(@event, execution_status);
                default:
                    return clSetUserEventStatus_Unix(@event, execution_status);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clSetUserEventStatus")]
        extern static ComputeErrorCode clSetUserEventStatus_Mac(
            CLEventHandle @event,
            Int32 execution_status);
        [DllImport(libNameWindows, EntryPoint = "clSetUserEventStatus")]
        extern static ComputeErrorCode clSetUserEventStatus_Windows(
            CLEventHandle @event,
            Int32 execution_status);
        [DllImport(libNameUnix, EntryPoint = "clSetUserEventStatus")]
        extern static ComputeErrorCode clSetUserEventStatus_Unix(
            CLEventHandle @event,
            Int32 execution_status);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode SetEventCallback(
            CLEventHandle @event,
            Int32 command_exec_callback_type,
            ComputeEventCallback pfn_notify,
            IntPtr user_data)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clSetEventCallback_Mac(@event, command_exec_callback_type, pfn_notify, user_data);
                case BindingPlatform.Windows:
                    return clSetEventCallback_Windows(@event, command_exec_callback_type, pfn_notify, user_data);
                default:
                    return clSetEventCallback_Unix(@event, command_exec_callback_type, pfn_notify, user_data);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clSetEventCallback")]
        extern static ComputeErrorCode clSetEventCallback_Mac(
            CLEventHandle @event,
            Int32 command_exec_callback_type,
            ComputeEventCallback pfn_notify,
            IntPtr user_data);
        [DllImport(libNameWindows, EntryPoint = "clSetEventCallback")]
        extern static ComputeErrorCode clSetEventCallback_Windows(
            CLEventHandle @event,
            Int32 command_exec_callback_type,
            ComputeEventCallback pfn_notify,
            IntPtr user_data);
        [DllImport(libNameUnix, EntryPoint = "clSetEventCallback")]
        extern static ComputeErrorCode clSetEventCallback_Unix(
            CLEventHandle @event,
            Int32 command_exec_callback_type,
            ComputeEventCallback pfn_notify,
            IntPtr user_data);


        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueReadBufferRect(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_read,
            ref SysIntX3 buffer_offset,
            ref SysIntX3 host_offset,
            ref SysIntX3 region,
            IntPtr buffer_row_pitch,
            IntPtr buffer_slice_pitch,
            IntPtr host_row_pitch,
            IntPtr host_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueReadBufferRect_Mac(command_queue, buffer, blocking_read, ref buffer_offset,
                                                       ref host_offset, ref region, buffer_row_pitch, buffer_slice_pitch,
                                                       host_row_pitch, host_slice_pitch, ptr, num_events_in_wait_list,
                                                       event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueReadBufferRect_Windows(command_queue, buffer, blocking_read, ref buffer_offset,
                                                           ref host_offset, ref region, buffer_row_pitch, buffer_slice_pitch,
                                                           host_row_pitch, host_slice_pitch, ptr, num_events_in_wait_list,
                                                           event_wait_list, new_event);
                default:
                    return clEnqueueReadBufferRect_Unix(command_queue, buffer, blocking_read, ref buffer_offset,
                                                           ref host_offset, ref region, buffer_row_pitch, buffer_slice_pitch,
                                                           host_row_pitch, host_slice_pitch, ptr, num_events_in_wait_list,
                                                           event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueReadBufferRect")]
        extern static ComputeErrorCode clEnqueueReadBufferRect_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_read,
            ref SysIntX3 buffer_offset,
            ref SysIntX3 host_offset,
            ref SysIntX3 region,
            IntPtr buffer_row_pitch,
            IntPtr buffer_slice_pitch,
            IntPtr host_row_pitch,
            IntPtr host_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueReadBufferRect")]
        extern static ComputeErrorCode clEnqueueReadBufferRect_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_read,
            ref SysIntX3 buffer_offset,
            ref SysIntX3 host_offset,
            ref SysIntX3 region,
            IntPtr buffer_row_pitch,
            IntPtr buffer_slice_pitch,
            IntPtr host_row_pitch,
            IntPtr host_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueReadBufferRect")]
        extern static ComputeErrorCode clEnqueueReadBufferRect_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_read,
            ref SysIntX3 buffer_offset,
            ref SysIntX3 host_offset,
            ref SysIntX3 region,
            IntPtr buffer_row_pitch,
            IntPtr buffer_slice_pitch,
            IntPtr host_row_pitch,
            IntPtr host_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueWriteBufferRect(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_write,
            ref SysIntX3 buffer_offset,
            ref SysIntX3 host_offset,
            ref SysIntX3 region,
            IntPtr buffer_row_pitch,
            IntPtr buffer_slice_pitch,
            IntPtr host_row_pitch,
            IntPtr host_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueWriteBufferRect_Mac(command_queue, buffer, blocking_write, ref buffer_offset,
                                                       ref host_offset, ref region, buffer_row_pitch, buffer_slice_pitch,
                                                       host_row_pitch, host_slice_pitch, ptr, num_events_in_wait_list,
                                                       event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueWriteBufferRect_Windows(command_queue, buffer, blocking_write, ref buffer_offset,
                                                           ref host_offset, ref region, buffer_row_pitch, buffer_slice_pitch,
                                                           host_row_pitch, host_slice_pitch, ptr, num_events_in_wait_list,
                                                           event_wait_list, new_event);
                default:
                    return clEnqueueWriteBufferRect_Unix(command_queue, buffer, blocking_write, ref buffer_offset,
                                                           ref host_offset, ref region, buffer_row_pitch, buffer_slice_pitch,
                                                           host_row_pitch, host_slice_pitch, ptr, num_events_in_wait_list,
                                                           event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueWriteBufferRect")]
        extern static ComputeErrorCode clEnqueueWriteBufferRect_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_write,
            ref SysIntX3 buffer_offset,
            ref SysIntX3 host_offset,
            ref SysIntX3 region,
            IntPtr buffer_row_pitch,
            IntPtr buffer_slice_pitch,
            IntPtr host_row_pitch,
            IntPtr host_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueWriteBufferRect")]
        extern static ComputeErrorCode clEnqueueWriteBufferRect_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_write,
            ref SysIntX3 buffer_offset,
            ref SysIntX3 host_offset,
            ref SysIntX3 region,
            IntPtr buffer_row_pitch,
            IntPtr buffer_slice_pitch,
            IntPtr host_row_pitch,
            IntPtr host_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueWriteBufferRect")]
        extern static ComputeErrorCode clEnqueueWriteBufferRect_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle buffer,
            [MarshalAs(UnmanagedType.Bool)] bool blocking_write,
            ref SysIntX3 buffer_offset,
            ref SysIntX3 host_offset,
            ref SysIntX3 region,
            IntPtr buffer_row_pitch,
            IntPtr buffer_slice_pitch,
            IntPtr host_row_pitch,
            IntPtr host_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        public static ComputeErrorCode EnqueueCopyBufferRect(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_buffer,
            CLMemoryHandle dst_buffer,
            ref SysIntX3 src_origin,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            IntPtr src_row_pitch,
            IntPtr src_slice_pitch,
            IntPtr dst_row_pitch,
            IntPtr dst_slice_pitch,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst=1)] CLEventHandle[] new_event)
        {
            switch (bindingPlatform)
            {
                case BindingPlatform.Mac:
                    return clEnqueueCopyBufferRect_Mac(command_queue, src_buffer, dst_buffer, ref src_origin,
                                                       ref dst_origin, ref region, src_row_pitch, src_slice_pitch,
                                                       dst_row_pitch, dst_slice_pitch, num_events_in_wait_list,
                                                       event_wait_list, new_event);
                case BindingPlatform.Windows:
                    return clEnqueueCopyBufferRect_Windows(command_queue, src_buffer, dst_buffer, ref src_origin,
                                                           ref dst_origin, ref region, src_row_pitch, src_slice_pitch,
                                                           dst_row_pitch, dst_slice_pitch, num_events_in_wait_list,
                                                           event_wait_list, new_event);
                default:
                    return clEnqueueCopyBufferRect_Unix(command_queue, src_buffer, dst_buffer, ref src_origin,
                                                        ref dst_origin, ref region, src_row_pitch, src_slice_pitch,
                                                        dst_row_pitch, dst_slice_pitch, num_events_in_wait_list,
                                                        event_wait_list, new_event);
            }
        }
        [DllImport(libNameMac, EntryPoint = "clEnqueueCopyBufferRect")]
        extern static ComputeErrorCode clEnqueueCopyBufferRect_Mac(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_buffer,
            CLMemoryHandle dst_buffer,
            ref SysIntX3 src_origin,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            IntPtr src_row_pitch,
            IntPtr src_slice_pitch,
            IntPtr dst_row_pitch,
            IntPtr dst_slice_pitch,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameWindows, EntryPoint = "clEnqueueCopyBufferRect")]
        extern static ComputeErrorCode clEnqueueCopyBufferRect_Windows(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_buffer,
            CLMemoryHandle dst_buffer,
            ref SysIntX3 src_origin,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            IntPtr src_row_pitch,
            IntPtr src_slice_pitch,
            IntPtr dst_row_pitch,
            IntPtr dst_slice_pitch,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);
        [DllImport(libNameUnix, EntryPoint = "clEnqueueCopyBufferRect")]
        extern static ComputeErrorCode clEnqueueCopyBufferRect_Unix(
            CLCommandQueueHandle command_queue,
            CLMemoryHandle src_buffer,
            CLMemoryHandle dst_buffer,
            ref SysIntX3 src_origin,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            IntPtr src_row_pitch,
            IntPtr src_slice_pitch,
            IntPtr dst_row_pitch,
            IntPtr dst_slice_pitch,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] CLEventHandle[] event_wait_list,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] CLEventHandle[] new_event);

        #region Deprecated functions

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [Obsolete("Deprecated in OpenCL 1.1.")]
        public new static ComputeErrorCode SetCommandQueueProperty(
            CLCommandQueueHandle command_queue,
            ComputeCommandQueueFlags properties,
            [MarshalAs(UnmanagedType.Bool)] bool enable,
            out ComputeCommandQueueFlags old_properties)
        {
            Trace.WriteLine("WARNING! clSetCommandQueueProperty has been deprecated in OpenCL 1.1.");
            return CL10.SetCommandQueueProperty(command_queue, properties, enable, out old_properties);
        }

        #endregion
    }

    /// <summary>
    /// A callback function that can be registered by the application.
    /// </summary>
    /// <param name="memobj"> The memory object being deleted. When the user callback is called, this memory object is not longer valid. <paramref name="memobj"/> is only provided for reference purposes. </param>
    /// <param name="user_data"> A pointer to user supplied data. </param>
    /// /// <remarks> This callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe. </remarks>
    public delegate void ComputeMemoryDestructorNotifer(CLMemoryHandle memobj, IntPtr user_data);

    /// <summary>
    /// The event callback function that can be registered by the application.
    /// </summary>
    /// <param name="eventHandle"> The event object for which the callback function is invoked. </param>
    /// <param name="cmdExecStatusOrErr"> Represents the execution status of the command for which this callback function is invoked. If the callback is called as the result of the command associated with the event being abnormally terminated, an appropriate error code for the error that caused the termination will be passed to <paramref name="cmdExecStatusOrErr"/> instead. </param>
    /// <param name="userData"> A pointer to user supplied data. </param>
    /// /// <remarks> This callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe. </remarks>
    public delegate void ComputeEventCallback(CLEventHandle eventHandle, int cmdExecStatusOrErr, IntPtr userData);
}
