﻿using Cloo;
using OpenTK.Compute.CL10;
using System;
using OpenTK;
namespace ClooTester
{
    public class KernelArgsTester: AbstractTester
    {
        string argsKernel = @"
kernel void argsKernel( 
    local float4* result )
{
    
}
";
        public KernelArgsTester()
            : base( "Kernel args test" )
        {
        }

        public override void Run()
        {
            StartRun();

            ComputeContext context = new ComputeContext( DeviceTypeFlags.DeviceTypeDefault, null, null );
            ComputeBuffer<Vector4> result = new ComputeBuffer<Vector4>( context, MemFlags.MemReadWrite, 1 );

            ComputeProgram program = new ComputeProgram( context, argsKernel );
            program.Build( null, null, null, IntPtr.Zero );
            ComputeKernel kernel = program.CreateKernel( "argsKernel" );

            //kernel.SetMemoryArg( 0, result );
            Vector4 num = new Vector4( 1, 0, 1, 0 );
            unsafe
            {                
                kernel.SetArg( 0, new IntPtr( 100 ), IntPtr.Zero );
            }

            ComputeJobQueue jobs = new ComputeJobQueue( context, context.Devices[ 0 ], ( CommandQueueFlags )0 );
            jobs.Execute( kernel, null );

            Vector4[] resArray = jobs.Read( result, true, 0, 1, null );

            EndRun();
        }
    }
}