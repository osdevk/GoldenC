using System;
using System.Diagnostics;
using System.Windows.Forms;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using System.Drawing;
using SharpDX.DXGI;
using SharpDX.Windows;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;

namespace BitterGFX3D
{
    public class B3DWindow //3D Window, supports 2D(/Later make functionality with BitterGFX(2D)/)
    {
        Factory dxgiFactory;
        RenderForm wndForm;
        DeviceContext ctx;
        Device device;
        SwapChain swapChain;
        SwapChainDescription swChainDesc;

        public void setVSPSShader(string PS, string VS)
        {
            var vertexShaderByteCode = ShaderBytecode.CompileFromFile(VS, "VS", "vs_4_0");
            var vertexShader = new VertexShader(device, vertexShaderByteCode);
            ctx.VertexShader.Set(vertexShader);
            var pixelShaderByteCode = ShaderBytecode.CompileFromFile(PS, "PS", "ps_4_0");
            var pixelShader = new PixelShader(device, pixelShaderByteCode);
            ctx.PixelShader.Set(pixelShader);
        }
        public void setVSShader(string path)
        {
            var vertexShaderByteCode = ShaderBytecode.CompileFromFile(path, "VS", "vs_4_0");
            var vertexShader = new VertexShader(device, vertexShaderByteCode);
            ctx.VertexShader.Set(vertexShader);
        }
        public void setPSShader(string ps)
        {
            var pixelShaderByteCode = ShaderBytecode.CompileFromFile(ps, "PS", "ps_4_0");
            var pixelShader = new PixelShader(device, pixelShaderByteCode);
            ctx.PixelShader.Set(pixelShader);
        }
        public void setGSShader(string GS)
        {
            var geoShaderBC = ShaderBytecode.CompileFromFile(GS, "GS", "gs_4_0");
            var geoShader = new GeometryShader(device, geoShaderBC);
            ctx.GeometryShader.Set(geoShader);
        }
        public B3DWindow(string name, int x, int y)
        {
            var form = new RenderForm(name);
            form.ClientSize = new Size(x, y);
            // SwapChain description
            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription =
                    new ModeDescription(form.ClientSize.Width, form.ClientSize.Height,
                                        new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
            swChainDesc = desc;
            Device device;
            SwapChain swapChain;
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, desc, out device, out swapChain);
            var context = device.ImmediateContext;

            // Ignore all windows events
            var factory = swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);
            dxgiFactory = factory;
            ctx = context;
            wndForm = form;
        }
    }
}
