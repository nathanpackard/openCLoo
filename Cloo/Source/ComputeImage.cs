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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading;
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL image.
    /// </summary>
    /// <remarks> A memory object that stores a two- or three- dimensional structured array. Image data can only be accessed with read and write functions. The read functions use a sampler. </remarks>
    /// <seealso cref="ComputeMemory"/>
    /// <seealso cref="ComputeSampler"/>
    public class ComputeImage : ComputeMemory
    {
        #region Properties

        /// <summary>
        /// Gets or sets (protected) the depth in pixels of the <see cref="ComputeImage"/>.
        /// </summary>
        /// <value> The depth in pixels of the <see cref="ComputeImage"/>. </value>
        public int Depth { get; protected set; }

        /// <summary>
        /// Gets or sets (protected) the size of the elements (pixels) of the <see cref="ComputeImage"/>.
        /// </summary>
        /// <value> The size of the elements (pixels) of the <see cref="ComputeImage"/>. </value>
        public int ElementSize { get; protected set; }

        /// <summary>
        /// Gets or sets (protected) the height in pixels of the <see cref="ComputeImage"/>.
        /// </summary>
        /// <value> The height in pixels of the <see cref="ComputeImage"/>. </value>
        public int Height { get; protected set; }

        /// <summary>
        /// Gets or sets (protected) the size in bytes of a row of elements of the <see cref="ComputeImage"/>.
        /// </summary>
        /// <value> The size in bytes of a row of elements of the <see cref="ComputeImage"/>. </value>
        public long RowPitch { get; protected set; }

        /// <summary>
        /// Gets or sets (protected) the size in bytes of a 2D slice of a <see cref="ComputeImage3D"/>.
        /// </summary>
        /// <value> The size in bytes of a 2D slice of a <see cref="ComputeImage3D"/>. For a <see cref="ComputeImage2D"/> this value is 0. </value>
        public long SlicePitch { get; protected set; }

        /// <summary>
        /// Gets or sets (protected) the width in pixels of the <see cref="ComputeImage"/>.
        /// </summary>
        /// <value> The width in pixels of the <see cref="ComputeImage"/>. </value>
        public int Width { get; protected set; }

        /// <summary>
        /// Creates a new <see cref="ComputeImage"/> from an OpenGL texture object.
        /// </summary>
        /// <param name="context"> A <see cref="ComputeContext"/> with enabled CL/GL sharing. </param>
        /// <param name="flags"> A bit-field that is used to specify usage information about the <see cref="ComputeImage2D"/>. Only <c>ComputeMemoryFlags.ReadOnly</c>, <c>ComputeMemoryFlags.WriteOnly</c> and <c>ComputeMemoryFlags.ReadWrite</c> are allowed. </param>
        /// <param name="textureTarget"> One of the following values: GL_TEXTURE_2D, GL_TEXTURE_CUBE_MAP_POSITIVE_X, GL_TEXTURE_CUBE_MAP_POSITIVE_Y, GL_TEXTURE_CUBE_MAP_POSITIVE_Z, GL_TEXTURE_CUBE_MAP_NEGATIVE_X, GL_TEXTURE_CUBE_MAP_NEGATIVE_Y, GL_TEXTURE_CUBE_MAP_NEGATIVE_Z, or GL_TEXTURE_RECTANGLE. Using GL_TEXTURE_RECTANGLE for texture_target requires OpenGL 3.1. Alternatively, GL_TEXTURE_RECTANGLE_ARB may be specified if the OpenGL extension GL_ARB_texture_rectangle is supported. </param>
        /// <param name="mipLevel"> The mipmap level of the OpenGL 2D texture object to be used. </param>
        /// <param name="textureId"> The OpenGL 2D texture object id to use. </param>
        /// <returns> The created <see cref="ComputeImage2D"/>. </returns>
        public static ComputeImage CreateFromGLTexture(ComputeContext context, ComputeMemoryFlags flags, int textureTarget, int mipLevel, int textureId)
        {
            ComputeErrorCode error = ComputeErrorCode.Success;
            CLMemoryHandle image = CLInterface.CL12.CreateFromGLTexture(context.Handle, flags, textureTarget, mipLevel, textureId, out error);
            ComputeException.ThrowOnError(error);
            return new ComputeImage(image, context, flags);
        }

        /// <summary>
        /// Creates a new <see cref="ComputeImage2D"/> from an OpenGL renderbuffer object.
        /// </summary>
        /// <param name="context"> A <see cref="ComputeContext"/> with enabled CL/GL sharing. </param>
        /// <param name="flags"> A bit-field that is used to specify usage information about the <see cref="ComputeImage2D"/>. Only <c>ComputeMemoryFlags.ReadOnly</c>, <c>ComputeMemoryFlags.WriteOnly</c> and <c>ComputeMemoryFlags.ReadWrite</c> are allowed. </param>
        /// <param name="renderbufferId"> The OpenGL renderbuffer object id to use. </param>
        /// <returns> The created <see cref="ComputeImage2D"/>. </returns>
        public static ComputeImage CreateFromGLRenderbuffer(ComputeContext context, ComputeMemoryFlags flags, int renderbufferId)
        {
            ComputeErrorCode error = ComputeErrorCode.Success;
            CLMemoryHandle image = CLInterface.CL10.CreateFromGLRenderbuffer(context.Handle, flags, renderbufferId, out error);
            ComputeException.ThrowOnError(error);

            return new ComputeImage(image, context, flags);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ICollection<ComputeImageFormat> GetSupportedFormats(ComputeContext context, ComputeMemoryFlags flags, ComputeMemoryType type)
        {
            int formatCountRet = 0;
            ComputeErrorCode error = CLInterface.CL10.GetSupportedImageFormats(context.Handle, flags, type, 0, null, out formatCountRet);
            ComputeException.ThrowOnError(error);

            ComputeImageFormat[] formats = new ComputeImageFormat[formatCountRet];
            error = CLInterface.CL10.GetSupportedImageFormats(context.Handle, flags, type, formatCountRet, formats, out formatCountRet);
            ComputeException.ThrowOnError(error);

            return new Collection<ComputeImageFormat>(formats);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        public ComputeImage(ComputeContext context, ComputeMemoryFlags flags)
            : base(context, flags)
        { }

        /// <summary>
        /// Creates a new <see cref="ComputeImage"/>.
        /// </summary>
        /// <param name="context"> A valid <see cref="ComputeContext"/> in which the <see cref="ComputeImage"/> is created. </param>
        /// <param name="flags"> A bit-field that is used to specify allocation and usage information about the <see cref="ComputeImage"/>. </param>
        /// <param name="format"> A structure that describes the format properties of the <see cref="ComputeImage"/>. </param>
        /// <param name="imageDescription"> A structure that describes the <see cref="ComputeImage"/>. </param>
        /// <param name="data"> The data to initialize the <see cref="ComputeImage"/>. Can be <c>IntPtr.Zero</c>. </param>
        public ComputeImage(ComputeContext context, ComputeMemoryFlags flags, ComputeImageFormat format, ComputeImageDescription imageDescription, IntPtr data)
            : base(context, flags)
        {
            ComputeErrorCode error = ComputeErrorCode.Success;
            Handle = CLInterface.CL12.CreateImage(context.Handle, flags, ref format, ref imageDescription, data, out error);
            ComputeException.ThrowOnError(error);

            Init();
        }

        private ComputeImage(CLMemoryHandle handle, ComputeContext context, ComputeMemoryFlags flags)
            : base(context, flags)
        {
            Handle = handle;

            Init();
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// 
        /// </summary>
        protected void Init()
        {
            SetID(Handle.Value);

            Depth = (int)GetInfo<CLMemoryHandle, ComputeImageInfo, IntPtr>(Handle, ComputeImageInfo.Depth, CLInterface.CL10.GetImageInfo);
            ElementSize = (int)GetInfo<CLMemoryHandle, ComputeImageInfo, IntPtr>(Handle, ComputeImageInfo.ElementSize, CLInterface.CL10.GetImageInfo);
            Height = (int)GetInfo<CLMemoryHandle, ComputeImageInfo, IntPtr>(Handle, ComputeImageInfo.Height, CLInterface.CL10.GetImageInfo);
            RowPitch = (long)GetInfo<CLMemoryHandle, ComputeImageInfo, IntPtr>(Handle, ComputeImageInfo.RowPitch, CLInterface.CL10.GetImageInfo);
            Size = (long)GetInfo<CLMemoryHandle, ComputeMemoryInfo, IntPtr>(Handle, ComputeMemoryInfo.Size, CLInterface.CL10.GetMemObjectInfo);
            SlicePitch = (long)GetInfo<CLMemoryHandle, ComputeImageInfo, IntPtr>(Handle, ComputeImageInfo.SlicePitch, CLInterface.CL10.GetImageInfo);
            Width = (int)GetInfo<CLMemoryHandle, ComputeImageInfo, IntPtr>(Handle, ComputeImageInfo.Width, CLInterface.CL10.GetImageInfo);
        }

        #endregion
    }
}
