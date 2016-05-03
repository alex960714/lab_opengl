using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;

namespace lab_opengl
{
    public partial class Form1 : Form
    {
        GLgraphics glgraphics = new GLgraphics();

        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            glgraphics.Setup(glControl1.Width, glControl1.Height);
            Application.Idle += Application_Idle;
            int texID = glgraphics.LoadTexture("NNMetro.png");
            glgraphics.texturesIDs.Add(texID);
            texID = glgraphics.LoadTexture("waterfall.jpg");
            glgraphics.texturesIDs.Add(texID);
            texID = glgraphics.LoadTexture("stones.jpg");
            glgraphics.texturesIDs.Add(texID);
            texID = glgraphics.LoadTexture("stars_2.png");
            glgraphics.texturesIDs.Add(texID);
            texID = glgraphics.LoadTexture("river.jpg");
            glgraphics.texturesIDs.Add(texID);
            texID = glgraphics.LoadTexture("forest.jpg");
            glgraphics.texturesIDs.Add(texID);
            texID = glgraphics.LoadTexture("sands.jpg");
            glgraphics.texturesIDs.Add(texID);
            texID = glgraphics.LoadTexture("rocks.jpg");
            glgraphics.texturesIDs.Add(texID);
            texID = glgraphics.LoadTexture("grass.jpg");
            glgraphics.texturesIDs.Add(texID);
            texID = glgraphics.LoadTexture("bridge.jpg");
            glgraphics.texturesIDs.Add(texID);
            texID = glgraphics.LoadTexture("forest_2.jpg");
            glgraphics.texturesIDs.Add(texID);

        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            glgraphics.Update();
            glControl1.SwapBuffers();
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            float widthCoef = (e.X - glControl1.Width *
                0.5f) / (float)glControl1.Width;
            float heightCoef = (-e.Y + glControl1.Height *
                0.5f) / (float)glControl1.Height;
            glgraphics.latitude = heightCoef * 180;
            glgraphics.longitude = widthCoef * 360;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            while (glControl1.IsIdle)
                glControl1.Refresh();
        }

        private void glControl1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                /*glgraphics.rotX = true;
                glgraphics.rotY = false;*/
                glgraphics.dx -= 0.1f * (float)Math.Cos(Math.PI / 180.0f * glgraphics.longitude);
                glgraphics.dy -= 0.1f * (float)Math.Sin(Math.PI / 180.0f * glgraphics.longitude);

                glgraphics.rotateAngle = glgraphics.rotateAngle + (float)(Math.Sqrt(Math.Pow(0.1f * (float)Math.Cos(Math.PI / 180.0f * glgraphics.longitude),2) +
                    Math.Pow(0.1f * (float)Math.Sin(Math.PI / 180.0f * glgraphics.longitude),2)) * 180 / (0.5f * Math.PI));
                glgraphics.rotate = new Vector3((float)Math.Sin(Math.PI / 180.0f * glgraphics.longitude), -(float)Math.Cos(Math.PI / 180.0f * glgraphics.longitude), 0);
            }
            if (e.KeyCode == Keys.S)
            {
                //glgraphics.rotX = true;
                //glgraphics.rotY = false;
                glgraphics.dx += 0.1f * (float)Math.Cos(Math.PI / 180.0f * glgraphics.longitude);
                glgraphics.dy += 0.1f * (float)Math.Sin(Math.PI / 180.0f * glgraphics.longitude);

                glgraphics.rotateAngle = glgraphics.rotateAngle - (float)(Math.Sqrt(Math.Pow(0.1f * (float)Math.Cos(Math.PI / 180.0f * glgraphics.longitude), 2) +
                    Math.Pow(0.1f * (float)Math.Sin(Math.PI / 180.0f * glgraphics.longitude), 2)) * 180 / (0.5f * Math.PI));
                glgraphics.rotate = new Vector3((float)Math.Sin(Math.PI / 180.0f * glgraphics.longitude), -(float)Math.Cos(Math.PI / 180.0f * glgraphics.longitude), 0);
            }
            if (e.KeyCode == Keys.A)
            {
                //glgraphics.rotY = true;
                //glgraphics.rotX = false;
                glgraphics.dx -= 0.1f * (float)Math.Cos(90.0f + Math.PI / 180.0f * glgraphics.longitude);
                glgraphics.dy -= 0.1f * (float)Math.Sin(90.0f + Math.PI / 180.0f * glgraphics.longitude);

                glgraphics.rotateAngle = glgraphics.rotateAngle + (float)(Math.Sqrt(Math.Pow(0.1f * (float)Math.Cos(Math.PI / 180.0f * glgraphics.longitude), 2) +
                    Math.Pow(0.1f * (float)Math.Sin(Math.PI / 180.0f * glgraphics.longitude), 2)) * 180 / (0.5f * Math.PI));
                glgraphics.rotate = new Vector3((float)Math.Cos(Math.PI / 180.0f * glgraphics.longitude), (float)Math.Sin(Math.PI / 180.0f * glgraphics.longitude), 0);
            }
            if (e.KeyCode == Keys.D)
            {
               // glgraphics.rotY = true;
                //glgraphics.rotX = false;
                glgraphics.dx += 0.1f * (float)Math.Cos(90.0f + Math.PI / 180.0f * glgraphics.longitude);
                glgraphics.dy += 0.1f * (float)Math.Sin(90.0f + Math.PI / 180.0f * glgraphics.longitude);

                glgraphics.rotateAngle = glgraphics.rotateAngle - (float)(Math.Sqrt(Math.Pow(0.1f * (float)Math.Cos(Math.PI / 180.0f * glgraphics.longitude), 2) +
                    Math.Pow(0.1f * (float)Math.Sin(Math.PI / 180.0f * glgraphics.longitude), 2)) * 180 / (0.5f * Math.PI));
                glgraphics.rotate = new Vector3((float)Math.Cos(Math.PI / 180.0f * glgraphics.longitude), (float)Math.Sin(Math.PI / 180.0f * glgraphics.longitude), 0);
            }
            if (glgraphics.dx > 17.249f)
                glgraphics.dx = 17.249f;
            if ((glgraphics.dx < 2.749f)&&(glgraphics.dx>-2.249f)&&(glgraphics.dy>-7.749f)&&(glgraphics.dy<2.249f))
                glgraphics.dx = 2.749f;
            if ((glgraphics.dx > 7.749f)&& (glgraphics.dy < -2.749f) && (glgraphics.dy > -12.249f))
                glgraphics.dx = 7.749f;
            if ((glgraphics.dy > 2.249f)&&(glgraphics.dx>-12.249f)&&(glgraphics.dy<5.0f))
                glgraphics.dy = 2.249f;
            if ((glgraphics.dy < 7.749f) && (glgraphics.dx > -12.249f)&& (glgraphics.dy > 5.0f))
                glgraphics.dy = 7.749f;
            if ((glgraphics.dy < -2.249f)&&(glgraphics.dx>7.449f))
                glgraphics.dy = -2.249f;
        }
    }

    
}
