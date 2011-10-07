using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace SpoonsAdventure
{
    class Renderer
    {
        Model modelSpoon;
        Model modelBox;
        float aspectRatio;
        Vector3 modelPosition;
        float modelRotationX;
        float modelRotationY;

        Vector3 cameraPosition = new Vector3(0f, 0f, 100f);

        public Renderer(GraphicsDeviceManager graphics)
        {
            modelPosition  = Vector3.Zero;
            modelRotationX = 0.0f;
            modelRotationY = MathHelper.PiOver2;
            cameraPosition = new Vector3(0f, 0f, 100f);

            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
        }

        public void LoadContent(ContentManager Content)
        {
            modelSpoon = Content.Load<Model>("Models/Spoon");
            modelBox   = Content.Load<Model>("Models/Box");
        }

        public void Draw()
        {
            Render(modelSpoon);
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
                    effect.World = transformation[mesh.ParentBone.Index] * Matrix.CreateRotationX(modelRotationX) * Matrix.CreateRotationY(modelRotationY) * Matrix.CreateTranslation(modelPosition);
                    effect.View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90f), aspectRatio, 1f, 10000f);
                }

                mesh.Draw();
            }
        }

        // Just playing with the model for now
        public void Controller(KeyboardState key)
        {
            if (key.IsKeyDown(Keys.W))
            {
                modelPosition.Y += 5f;
            }
            else if (key.IsKeyDown(Keys.S))
            {
                modelPosition.Y -= 5f;
            }

            if (key.IsKeyDown(Keys.A))
            {
                modelPosition.X -= 0.1f;

            }
            else if (key.IsKeyDown(Keys.D))
            {
                modelPosition.X += 0.1f;
            }

            if (key.IsKeyDown(Keys.Z))
            {
                modelPosition.Z -= 5f;

            }
            else if (key.IsKeyDown(Keys.X))
            {
                modelPosition.Z += 5f;
            }

            if (key.IsKeyDown(Keys.Up))
            {
                modelRotationX += 0.1f;
            }
            if (key.IsKeyDown(Keys.Down))
            {
                modelRotationX -= 0.1f;
            }
            if (key.IsKeyDown(Keys.Left))
            {
                modelRotationY -= 0.1f;
            }
            if (key.IsKeyDown(Keys.Right))
            {
                modelRotationY += 0.1f;
            }
        }
    }
}
