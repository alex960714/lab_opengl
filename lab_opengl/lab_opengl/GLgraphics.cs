using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.Drawing;

namespace lab_opengl
{
    class GLgraphics
    {
        Vector3 cameraPosition = new Vector3(2, 3, 4);
        Vector3 cameraDirection = new Vector3(0, 0, 0);
        Vector3 cameraUp = new Vector3(0, 0, 1);

        public float latitude = 47.98f;
        public float longitude = 60.41f;
        public float radius = 5.385f;

        public float rotateAngle = 0;
        public float rotateAngleX = 0;
        public float rotateAngleY = 0;
        public float dx = 0, dx1 = 0;
        public float dy = 0, dy1 = 0;
        public float dz = 0, dz1 = 0;

        public List<int> texturesIDs = new List<int>();

        public void Setup(int width, int height)
        {
            GL.ClearColor(Color.DarkGray);
            GL.ShadeModel(ShadingModel.Smooth);
            GL.Enable(EnableCap.DepthTest);

            Matrix4 perspectiveMat =
                Matrix4.CreatePerspectiveFieldOfView(
                    MathHelper.PiOver4,
                    width / (float)height, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiveMat);

            SetupLightning();
        }

        public void Update()
        {
            rotateAngleX = rotateAngleX + (float)((dx - dx1) * 180 / (0.5f * Math.PI));
            rotateAngleY = rotateAngleY - (float)((dy - dy1) * 180 / (0.5f * Math.PI));
            GL.Clear(ClearBufferMask.ColorBufferBit |
                ClearBufferMask.DepthBufferBit);
            Matrix4 viewMat = Matrix4.LookAt(
                cameraPosition, cameraDirection,
                cameraUp);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMat);
            Render();


            cameraPosition = new Vector3(//dx+
                (float)(radius * Math.Cos(Math.PI / 180.0f * latitude) *
                Math.Cos(Math.PI / 180.0f * longitude)),//dy+
                (float)(radius * Math.Cos(Math.PI / 180.0f * latitude) *
                Math.Sin(Math.PI / 180.0f * longitude)),//0.3f+
                (float)(radius * Math.Sin(Math.PI / 180.0f * latitude)));


            dx1 = dx;
            dy1 = dy;

        }

        private void drawTestQuad()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            GL.Vertex3(-1.0f, -1.0f, 0.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(-1.0f, 1.0f, 0.0f);
            GL.Color3(Color.White);
            GL.Vertex3(1.0f, 1.0f, 0.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(1.0f, -1.0f, 0.0f);
            GL.End();
        }

        public void Render()
        {
            /*GL.PushMatrix();
            GL.Translate(0, 0, 3);
            GL.Rotate(10*rotateAngle, Vector3.UnitZ);
            GL.Scale(1.0f, 1.5f, 2.5f);
            GL.Color3(Color.LightSeaGreen);
            drawTriangle();
            GL.PopMatrix();*/

            GL.PushMatrix();
            GL.Translate(0, 0, 0);
            //GL.Rotate(rotateAngle, Vector3.UnitZ);
            GL.Scale(2.5f, 2.5f, 2.5f);
            drawTestQuad();
            GL.PopMatrix();

            /*GL.PushMatrix();
            GL.Translate(1, 1, 1);
            GL.Rotate(rotateAngle, Vector3.UnitZ);
            GL.Scale(0.5f, 0.5f, 0.5f);
            drawTexturedQuad();
            GL.PopMatrix();*/

            GL.PushMatrix();
            GL.Translate(dx, dy, 0.3f);
            GL.Rotate(rotateAngleX, Vector3d.UnitY);
            GL.Rotate(rotateAngleY, Vector3d.UnitX);
            GL.Scale(0.5f, 0.5f, 0.5f);
            GL.Color3(Color.BlueViolet);
            drawTexturedSphere(0.5f, 50, 50);
            GL.PopMatrix();

            /*GL.PushMatrix();
            GL.Translate(5* Math.Sin(rotateAngle/2), 2* Math.Cos(rotateAngle/2), 0);
            GL.Rotate(3*rotateAngle, Vector3.UnitZ);
            GL.Scale(0.5f, 0.5f, 0.5f);
            GL.Color3(Color.BlueViolet);
            drawTexturedSphere(2.0f, 50, 50);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0, 0, 3*Math.Sin(rotateAngle));
            GL.Scale(0.5f, 0.5f, 0.5f);
            GL.Color3(Color.Black);
            drawPoint();
            GL.PopMatrix();*/
            //drawTexturedQuad();
        }

        public int LoadTexture(String filePath)
        {
            try
            {
                Bitmap image = new Bitmap(filePath);
                int texID = GL.GenTexture();

                GL.BindTexture(TextureTarget.Texture2D, texID);
                BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0,
                    image.Width, image.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                    data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                    PixelType.UnsignedByte, data.Scan0);
                image.UnlockBits(data);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                return texID;
            }
            catch (System.IO.FileNotFoundException e)
            {
                return -1;
            }
        }

        private void drawTexturedQuad()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[0]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Red);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Color3(Color.White);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Green);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        public void SetupLightning()
        {
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Light1);

            Vector4 lightPosition = new Vector4(3.1f, 2.0f, 6.0f, 0.0f);
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);

            Vector4 ambientColor = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Ambient, ambientColor);

            Vector4 diffuseColor = new Vector4(0.6f, 0.6f, 0.6f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Diffuse, diffuseColor);

            Vector4 lightPosition1 = new Vector4(3.0f, 2.0f, 6.0f, 0.0f);
            GL.Light(LightName.Light1, LightParameter.Position, lightPosition1);

            Vector4 ambientColor1 = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Light(LightName.Light1, LightParameter.Ambient, ambientColor1);

            Vector4 diffuseColor1 = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);
            GL.Light(LightName.Light1, LightParameter.Diffuse, diffuseColor1);

            Vector4 materialSpecular = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, materialSpecular);
            float materialShininess = 1000;
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, materialShininess);
        }

        private void drawSphere(double r, int nx, int ny)
        {
            int ix, iy;
            double x, y, z;
            for (iy = 0; iy < ny; ++iy)
            {
                //GL.Enable(EnableCap.Texture2D);
                //GL.BindTexture(TextureTarget.Texture2D, texturesIDs[0]);
                GL.Begin(PrimitiveType.QuadStrip);
                for (ix = 0; ix <= nx; ++ix)
                {
                    x = r * Math.Sin(iy * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin(iy * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos(iy * Math.PI / ny);
                    GL.Normal3(x, y, z);
                    GL.Vertex3(x, y, z);

                    x = r * Math.Sin((iy + 1) * (Math.PI / ny)) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos((iy + 1) * Math.PI / ny);
                    GL.Normal3(x, y, z);
                    GL.Vertex3(x, y, z);
                }
                GL.End();
                //GL.Disable(EnableCap.Texture2D);
            }
        }

        private void drawTexturedSphere(double r, int nx, int ny)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[0]);
            int ix, iy;
            double x, y, z;
            for (iy = 0; iy < ny; ++iy)
            {
                //GL.Enable(EnableCap.Texture2D);
                //GL.BindTexture(TextureTarget.Texture2D, texturesIDs[0]);
                GL.Begin(PrimitiveType.QuadStrip);
                for (ix = 0; ix <= nx; ++ix)
                {
                    x = r * Math.Sin(iy * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin(iy * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos(iy * Math.PI / ny);
                    GL.TexCoord2((float)ix / nx, (float)iy / ny);
                    GL.Normal3(x, y, z);
                    GL.Vertex3(x, y, z);

                    x = r * Math.Sin((iy + 1) * (Math.PI / ny)) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos((iy + 1) * Math.PI / ny);
                    GL.TexCoord2((float)ix / nx, (float)iy / ny + 1.0 / ny);
                    GL.Normal3(x, y, z);
                    GL.Vertex3(x, y, z);
                }
                GL.End();
                //GL.Disable(EnableCap.Texture2D);
            }
            GL.Disable(EnableCap.Texture2D);
        }

        private void drawPoint()
        {
            drawSphere(0.05f, 20, 20);
        }

        private void drawTriangle()
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.Vertex3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(0.5f, 1.0f, 0.0f);
            GL.End();
        }
    }
}
