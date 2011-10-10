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
            _viewport = new xTile.Dimensions.Rectangle(new Size(Defs.screenWidth, Defs.screenHeight));
            
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

            _viewport.X -= Defs.screenWidth / 2;
        }
        
        public void Draw(SpriteBatch sb)
        {
            Vector2 pos = _spoon._body.Position * Defs.MtrInPix;

            _CameraPosition.X = pos.X;
            _CameraPosition.Y = pos.Y;

            // 2D
            _map.Draw(_mapDisplayDevice, _viewport);
            
            sb.Draw(texture, pos + _spoon._centerOff, null, Color.White, _spoon._body.Rotation, _spoon._centerOff, 1f, SpriteEffects.None, 0);
            // 3D
            Render(pos);
        }

        // Actual position and model position are not on the same scale
        private void Render(Vector2 spoonPos)
        {
            Vector3 pos = Vector3.Zero;
            pos.X = spoonPos.X;
            pos.Y = spoonPos.Y;

            Matrix[] transformation = new Matrix[_spoon._model.Bones.Count];
            _spoon._model.CopyAbsoluteBoneTransformsTo(transformation);

            foreach (ModelMesh mesh in _spoon._model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transformation[mesh.ParentBone.Index] * Matrix.CreateScale(_spoon._scale) * Matrix.CreateRotationY(_spoon._rotAboutY) * Matrix.CreateRotationZ(-_spoon._body.Rotation) * Matrix.CreateTranslation(pos.X, pos.Y, 0f);
                    effect.View = Matrix.CreateLookAt(_CameraPosition, pos, Vector3.Up);
                    effect.Projection = Matrix.CreateOrthographic(Defs.screenWidth, Defs.screenHeight, 1f, 100f);
                }

                mesh.Draw();
            }
        }
    }
}
