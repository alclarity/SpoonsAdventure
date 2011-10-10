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
            _viewport = new xTile.Dimensions.Rectangle(new Size(1000, 768));
            
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
        
        public void Draw(SpriteBatch sb)
        {
            // 2D
            _map.Draw(_mapDisplayDevice, _viewport);

            //spriteBatch.Draw(tex, pos, null, Color.White, body.Rotation, origin, 1f, SpriteEffects.None, 0);
            Vector2 pos = _spoon._body.Position * Defs.MtrInPix + new Vector2(16,16);
            sb.Draw(texture, pos, null, Color.White, _spoon._body.Rotation, new Vector2(16,16), 1f, SpriteEffects.None, 0);
            // 3D
            Render();
        }

        // 0,0 in the world is center on the Camera
        // Actual position and model position are not on the same scale
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
                    effect.World = transformation[mesh.ParentBone.Index] * Matrix.CreateScale(_spoon._scale) * Matrix.CreateRotationY(_spoon._rotAboutY) * Matrix.CreateTranslation(xPos, yPos, 0f);
                    effect.View = Matrix.CreateLookAt(_CameraPosition, Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90f), _aspectRatio, 1f, 10000f);
                }

                mesh.Draw();
            }
        }
    }
}
