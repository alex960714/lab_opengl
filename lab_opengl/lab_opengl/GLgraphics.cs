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
        public float longitude = 0;
        public float radius = 5.385f;
        
        public float rotateAngle = 0;
        public float dx = 0, dx1 = 0;
        public float dy = 0, dy1 = 0;
        public float dz = 0, dz1 = 0;

        public bool rotX = false;
        public bool rotY = false;
        public bool rotZ = false;

        public Vector3 rotate = new Vector3(Vector3.UnitY);

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
            //rotateAngleX = rotateAngleX + (float)((dx - dx1) * 180 / (0.5f * Math.PI));
            //rotateAngleY = rotateAngleY - (float)((dy - dy1) * 180 / (0.5f * Math.PI));
            GL.Clear(ClearBufferMask.ColorBufferBit |
                ClearBufferMask.DepthBufferBit);
            Matrix4 viewMat = Matrix4.LookAt(
                cameraPosition, cameraDirection,
                cameraUp);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMat);
            Render();


            cameraPosition = new Vector3(dx+
                (float)(radius * Math.Cos(Math.PI / 180.0f * latitude) *
                Math.Cos(Math.PI / 180.0f * longitude)),dy+
                (float)(radius * Math.Cos(Math.PI / 180.0f * latitude) *
                Math.Sin(Math.PI / 180.0f * longitude)),
                (float)(radius * Math.Sin(Math.PI / 180.0f * latitude)));

            cameraDirection = new Vector3(dx, dy, 0.25f);

            SetupLightning();

            dx1 = dx;
            dy1 = dy;

        }

        private void drawTestQuad()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            GL.Vertex3(-10.0f, -10.0f, 0.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(-10.0f, 10.0f, 0.0f);
            GL.Color3(Color.White);
            GL.Vertex3(10.0f, 10.0f, 0.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(10.0f, -10.0f, 0.0f);
            GL.End();
        }

        private void drawQuad()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            GL.Vertex3(-0.5f, -1.0f, 0.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(-0.5f, 1.0f, 0.0f);
            GL.Color3(Color.White);
            GL.Vertex3(0.5f, 1.0f, 0.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(0.5f, -1.0f, 0.0f);
            GL.End();
        }

        public void Render()
        {
            /*GL.PushMatrix();
            GL.Translate(0, 0, -0.01f);
            GL.Scale(2.5f, 2.5f, 2.5f);
            drawTestQuad();
            GL.PopMatrix();*/

            GL.PushMatrix();
            GL.Translate(0.2f, 0.2f, 0.2f);
            //GL.Rotate(rotateAngle, rotate);
            GL.Scale(0.5f, 0.5f, 0.5f);
            GL.Color3(Color.White);
            drawPoint();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.0f, 5.0f, 0.75f);
            //GL.Rotate(rotateAngle, rotate);
            GL.Scale(0.5f, 0.5f, 0.5f);
            GL.Color3(Color.White);
            drawPoint();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.0f, 5.0f, 5.0f);
            //GL.Rotate(rotateAngle, rotate);
            GL.Scale(0.5f, 0.5f, 0.5f);
            GL.Color3(Color.White);
            drawPoint();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0, 0, 2.5f);
            GL.Scale(5.0f, 2.5f, 2.5f);
            drawTexturedQuad2();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-5.0f, 0, 2.5f);
            GL.Scale(5.0f, 2.5f, 2.5f);
            drawTexturedQuad2();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-2.5f, 2.5f, 0.0f);
            GL.Rotate(180, Vector3.UnitZ);
            GL.Rotate(180, Vector3.UnitX);
            GL.Scale(5.0f, 2.5f, 2.5f);
            drawStonesTriangle();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(5.0f, 5.0f, 1.5f);
            GL.Rotate(270, Vector3.UnitX);
            GL.Rotate(90, Vector3.UnitY);
            GL.Scale(5.0f, 1.5f, 2.5f);
            drawTexturedQuad();
            GL.PopMatrix();
        
            GL.PushMatrix();
            GL.Translate(dx, dy, 0.25f);
            GL.Rotate(rotateAngle, rotate);
            GL.Scale(0.5f, 0.5f, 0.5f);
            GL.Color3(Color.BlueViolet);
            drawTexturedSphere(0.5f, 50, 50);
            GL.PopMatrix();

            GL.PushMatrix();
            //GL.Translate(dx, dy, 0.25f);
            //GL.Rotate(rotateAngle, rotate);
            GL.Scale(1.0f, 1.0f, 1.0f);
            GL.Color3(Color.White);
            drawTexturedSphere2(50.0f, 200, 200);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0, 5.0f, 2.5f);
            GL.Rotate(90, Vector3.UnitZ);
            GL.Scale(5.0f, 2.5f, 2.5f);
            drawWaterQuad();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-7.5f, 5.0f, 0.0f);
            //GL.Rotate(180, Vector3.UnitZ);
            GL.Rotate(180, Vector3.UnitX);
            GL.Scale(5.0f, 2.5f, 2.5f);
            drawWaterTriangle();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-5.0f, 6.25f, 2.5f);
            GL.Rotate(90, Vector3.UnitZ);
            GL.Scale(2.5f, 2.5f, 2.5f);
            drawWaterQuad();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.0f, -5.0f, 1.5f);
            GL.Rotate(270, Vector3.UnitX);
            GL.Rotate(180, Vector3.UnitY);
            GL.Scale(5.0f, 1.5f, 2.5f);
            drawRocksQuad();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(5.0f, 0, 1.5f);
            GL.Rotate(270, Vector3.UnitX);
            GL.Rotate(90, Vector3.UnitY);
            GL.Scale(5.0f, 1.5f, 2.5f);
            drawRocksQuad();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-2.5f, -6.0f, 1.5f);
            GL.Rotate(270, Vector3.UnitX);
            GL.Rotate(135, Vector3.UnitY);
            GL.Scale(5.0f, 1.5f, 2.5f);
            drawRocksQuad();
            GL.PopMatrix();
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
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[1]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Aquamarine);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-0.5f, -1.0f, -1.0f);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(0.5f, -1.0f, -1.0f);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(0.5f, 1.0f, -1.0f);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(-0.5f, 1.0f, -1.0f);
            
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        private void drawTexturedQuad2()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[2]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Aquamarine);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(0.5f, -1.0f, -1.0f);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-0.5f, -1.0f, -1.0f);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(-0.5f, 1.0f, -1.0f);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(0.5f, 1.0f, -1.0f);

            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        private void drawWaterQuad()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[4]);
            GL.Begin(PrimitiveType.Quads);
            //GL.Color3(Color.LightGreen);

            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(0.5f, 1.0f, -1.0f);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(0.5f, -1.0f, -1.0f);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-0.5f, -1.0f, -1.0f);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(-0.5f, 1.0f, -1.0f);
            GL.Color3(Color.Green);

            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        private void drawForestQuad()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[5]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.LightGreen);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-0.5f, -1.0f, -1.0f);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(0.5f, -1.0f, -1.0f);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(0.5f, 1.0f, -1.0f);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(-0.5f, 1.0f, -1.0f);

            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        private void drawSkyQuad()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[3]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.LightGreen);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(0.5f, -1.0f, -1.0f);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-0.5f, -1.0f, -1.0f);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(-0.5f, 1.0f, -1.0f);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(0.5f, 1.0f, -1.0f);

            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        private void drawRocksQuad()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[7]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Green);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-0.5f, -1.0f, -1.0f);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(0.5f, -1.0f, -1.0f);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(0.5f, 1.0f, -1.0f);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(-0.5f, 1.0f, -1.0f);

            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        public void SetupLightning()
        {
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Light1);
            GL.Enable(EnableCap.Light2);
           // GL.Enable(EnableCap.Light3);

            Vector4 lightPosition = new Vector4(dx+3.1f, dy+2.0f, 6.0f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);

            Vector4 ambientColor = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Ambient, ambientColor);

            Vector4 diffuseColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Diffuse, diffuseColor);

            Vector4 lightPosition1 = new Vector4(0.0f, 5.0f, 0.75f, 1.0f);
            GL.Light(LightName.Light1, LightParameter.Position, lightPosition1);

            Vector4 ambientColor1 = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Light(LightName.Light1, LightParameter.Ambient, ambientColor1);

            Vector4 diffuseColor1 = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);
            GL.Light(LightName.Light1, LightParameter.Diffuse, diffuseColor1);

            Vector4 lightPosition2 = new Vector4(0.0f, 0.0f, 10.0f, 1.0f);
            GL.Light(LightName.Light2, LightParameter.Position, lightPosition1);

            Vector4 ambientColor2 = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Light(LightName.Light2, LightParameter.Ambient, ambientColor1);

            Vector4 diffuseColor2 = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);
            GL.Light(LightName.Light2, LightParameter.Diffuse, diffuseColor1);

            /*Vector4 lightPosition3 = new Vector4(0, 0, 60.0f, 0.0f);
            GL.Light(LightName.Light3, LightParameter.Position, lightPosition1);

            Vector4 ambientColor3 = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Light(LightName.Light3, LightParameter.Ambient, ambientColor1);

            Vector4 diffuseColor3 = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);
            GL.Light(LightName.Light3, LightParameter.Diffuse, diffuseColor1);*/

            Vector4 materialSpecular = new Vector4(0.0f, 1.0f, 1.0f, 1.0f);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, materialSpecular);
            float materialShininess = 0;
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, materialShininess);
        }

        private void drawSphere(double r, int nx, int ny)
        {
            int ix, iy;
            double x, y, z;
            for (iy = ny-1; iy >= 0; --iy)
            {
                GL.Begin(PrimitiveType.QuadStrip);
                for (ix = nx-1; ix >= 0; --ix)
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

        private void drawStonesTriangle()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[2]);
            GL.Begin(PrimitiveType.Triangles);
            //GL.Color3(Color.Aquamarine);

            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(1.0f, 0.0f, 0.0f);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(1.0f, 1.0f, 0.0f);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        private void drawWaterTriangle()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[4]);
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Aquamarine);

            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(1.0f, 0.0f, 0.0f);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(1.0f, 1.0f, 0.0f);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        private void drawTexturedSphere2(double r, int nx, int ny)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[3]);
            int ix, iy;
            double x, y, z;
            for (iy = ny-1; iy > 0; --iy)
            {
                GL.Begin(PrimitiveType.QuadStrip);
                for (ix = 0; ix <=nx ; ++ix)
                {
                    x = r * Math.Sin(iy * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin(iy * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos(iy * Math.PI / ny);
                    GL.TexCoord2((float)ix / nx, (float)iy / ny);
                    GL.Normal3(x, y, z);
                    GL.Vertex3(x, y, z);

                    x = r * Math.Sin((iy - 1) * (Math.PI / ny)) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin((iy - 1) * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos((iy - 1) * Math.PI / ny);
                    GL.TexCoord2((float)ix / nx, (float)iy / ny - 1.0 / ny);
                    GL.Normal3(x, y, z);
                    GL.Vertex3(x, y, z);
                }
                GL.End();
            }
            GL.Disable(EnableCap.Texture2D);
        }
    }
}
