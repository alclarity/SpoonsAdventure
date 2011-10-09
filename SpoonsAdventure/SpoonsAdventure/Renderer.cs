using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using xTile.Display;
using xTile.Dimensions;
using xTile;

namespace SpoonsAdventure
{
    class Renderer
    {
        Model _modelSpoon;
        Model _modelBox;
        float _aspectRatio;
        public Vector3 _modelPosition;
        public float _modelRotationX;
        public float _modelRotationY;

        Vector3 _CameraPosition;

        // Map Viewer
        Map _map;
        IDisplayDevice _mapDisplayDevice;
        xTile.Dimensions.Rectangle _viewport;

        public Renderer()
        {
            // 2D
            _viewport = new xTile.Dimensions.Rectangle(new Size(800, 600));
            
            // 3D
            _modelPosition  = Vector3.Zero;
            _modelRotationX = 0.0f;
            _modelRotationY = MathHelper.PiOver2;
            _CameraPosition = new Vector3(0f, 0f, 100f);
        }

        public void Init()
        {

        }

        public void LoadContent(ContentManager cm, GraphicsDevice gm, Map map)
        {
            _map = map;
            _mapDisplayDevice = new XnaDisplayDevice(cm, gm);
            _map.LoadTileSheets(_mapDisplayDevice);
            _aspectRatio = gm.Viewport.AspectRatio;

            _modelSpoon = cm.Load<Model>("Models/Spoon");
            _modelBox   = cm.Load<Model>("Models/Box");
        }
        
        public void Draw()
        {
            // 2D
            _map.Draw(_mapDisplayDevice, _viewport);

            // 3D
            Render(_modelSpoon);
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
                    effect.World = transformation[mesh.ParentBone.Index] * Matrix.CreateRotationX(_modelRotationX) * Matrix.CreateRotationY(_modelRotationY) * Matrix.CreateTranslation(_modelPosition);
                    effect.View = Matrix.CreateLookAt(_CameraPosition, Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90f), _aspectRatio, 1f, 10000f);
                }

                mesh.Draw();
            }
        }
    }
}
