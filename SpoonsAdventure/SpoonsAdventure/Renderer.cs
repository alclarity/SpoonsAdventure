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
        Character _spoon;
        float _aspectRatio;
        Vector3 _CameraPosition;

        // Map Viewer
        Map _map;
        IDisplayDevice _mapDisplayDevice;
        xTile.Dimensions.Rectangle _viewport;

        // Test
        Texture2D texture;

        public Renderer()
        {
            // 2D
            _viewport = new xTile.Dimensions.Rectangle(new Size(1000, 800));
            
            // 3D
            _CameraPosition = new Vector3(0f, 0f, 100f);
        }

        public void Init()
        {

        }

        public void LoadContent(ContentManager cm, GraphicsDevice gm, Map map, Character spoon)
        {
            _map = map;
            _mapDisplayDevice = new XnaDisplayDevice(cm, gm);
            _map.LoadTileSheets(_mapDisplayDevice);
            _aspectRatio = gm.Viewport.AspectRatio;

            _spoon = spoon;
            _spoon._model = cm.Load<Model>("Models/Spoon");

            // Test
            texture = cm.Load<Texture2D>("Models/SpoonTexture");
        }
        
        public void Draw()
        {
            // 2D
            _map.Draw(_mapDisplayDevice, _viewport);

            // 3D
            Render();
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, _spoon._body.Position, null, Color.White, 0, _spoon._center, 1, SpriteEffects.None, 1);
        }

        private void Render()
        {
            Matrix[] transformation = new Matrix[_spoon._model.Bones.Count];
            _spoon._model.CopyAbsoluteBoneTransformsTo(transformation);

            foreach (ModelMesh mesh in _spoon._model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    float xPos = _spoon._body.Position.X;
                    float yPos = _spoon._body.Position.Y;
                    effect.World = transformation[mesh.ParentBone.Index] * Matrix.CreateScale(_spoon._scale)  * Matrix.CreateRotationY(_spoon._rotAboutY) * Matrix.CreateTranslation(xPos, yPos, 0f);
                    effect.View = Matrix.CreateLookAt(_CameraPosition, Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90f), _aspectRatio, 1f, 10000f);
                }

                mesh.Draw();
            }
        }
    }
}
