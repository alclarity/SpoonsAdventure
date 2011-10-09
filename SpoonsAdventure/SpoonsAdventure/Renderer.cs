using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SpoonsAdventure
{
    class Renderer
    {
        Model _ModelSpoon;
        Model _ModelBox;
        float _AspectRatio;
        public Vector3 _ModelPosition;
        public float _ModelRotationX;
        public float _ModelRotationY;

        Vector3 cameraPosition = new Vector3(0f, 0f, 100f);

        public Renderer(GraphicsDeviceManager gdm)
        {
            _ModelPosition  = Vector3.Zero;
            _ModelRotationX = 0.0f;
            _ModelRotationY = MathHelper.PiOver2;
            cameraPosition = new Vector3(0f, 0f, 100f);

            _AspectRatio = gdm.GraphicsDevice.Viewport.AspectRatio;
        }

        public void LoadContent(ContentManager cm)
        {
            _ModelSpoon = cm.Load<Model>("Models/Spoon");
            _ModelBox   = cm.Load<Model>("Models/Box");
        }
        
        public void Draw()
        {
            Render(_ModelSpoon);
            //Render(modelBox);
        }

        private void Render(Model model)
        {
            Matrix[] transformation = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transformation);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transformation[mesh.ParentBone.Index] * Matrix.CreateRotationX(_ModelRotationX) * Matrix.CreateRotationY(_ModelRotationY) * Matrix.CreateTranslation(_ModelPosition);
                    effect.View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90f), _AspectRatio, 1f, 10000f);
                }

                mesh.Draw();
            }
        }
    }
}
