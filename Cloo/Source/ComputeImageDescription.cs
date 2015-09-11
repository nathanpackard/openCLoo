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

namespace Cloo
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents an OpenCL image format.
    /// </summary>
    /// <remarks> This structure defines the type, count and size of the image channels. </remarks>
    /// <seealso cref="ComputeImage"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct ComputeImageDescription
    {
        #region Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ComputeMemoryType image_type;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IntPtr image_width;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IntPtr image_height;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IntPtr image_depth;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IntPtr image_array_size;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IntPtr image_row_pitch;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IntPtr image_slice_pitch;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        System.UInt32 num_mip_levels;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        System.UInt32 num_samples;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IntPtr buffer;

        #endregion

        #region Properties

        public ComputeMemoryType Type { get { return image_type; } }
        public int Width { get { return (int)image_width; } }
        public int Height { get { return (int)image_height; } }
        public int Depth { get { return (int)image_depth; } }
        public long ArraySize { get { return (int)image_array_size; } }
        public long RowPitch { get { return (int)image_row_pitch; } }
        public long SlicePitch { get { return (int)image_slice_pitch; } }
        public int NumMipLevels { get { return (int)num_mip_levels; } }
        public int NumSamples { get { return (int)num_samples; } }
        public IntPtr Buffer { get { return buffer; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="ComputeImageDescription"/>.
        /// </summary>
        public ComputeImageDescription(ComputeMemoryType image_type, long image_width, long image_height, long image_depth, long image_array_size, long image_row_pitch, long image_slice_pitch, long num_mip_levels, long num_samples, IntPtr buffer)
        {
            this.image_type = image_type;
            this.image_width = new IntPtr(image_width);
            this.image_height = new IntPtr(image_height);
            this.image_depth = new IntPtr(image_depth);
            this.image_array_size = new IntPtr(image_array_size);
            this.image_row_pitch = new IntPtr(image_row_pitch);
            this.image_slice_pitch = new IntPtr(image_slice_pitch);
            this.num_mip_levels = (uint)num_mip_levels;
            this.num_samples = (uint)num_samples;
            this.buffer = buffer;
        }

        /// <summary>
        /// Creates a new <see cref="ComputeImageDescription"/>.
        /// </summary>
        public ComputeImageDescription(ComputeMemoryType image_type, long image_width, long image_height, long image_depth) : this(image_type, image_width, image_height, image_depth, 0,0,0,0,0, IntPtr.Zero) { }

        /// <summary>
        /// Creates a new <see cref="ComputeImageDescription"/>.
        /// </summary>
        public ComputeImageDescription(ComputeMemoryType image_type, long image_width, long image_height) : this(image_type, image_width, image_height, 1, 0, 0, 0, 0, 0, IntPtr.Zero) { }

        #endregion
    }
}