using System.Linq;
using Collector.Character;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Collector.Dimension
{
    public class WorldRenderer : IRestrictions
    {
        private Mouse _mouse;
        private InputController _inputController;
        private Player _player;
        private SpriteBatch _spriteBatch;
        private Main _main;
        private OrthographicCamera _orthographicCamera;

        public WorldRenderer(Mouse mouse, InputController inputController, Player player, SpriteBatch spriteBatch, Main main, OrthographicCamera orthographicCamera)
        {
            _mouse = mouse;
            _inputController = inputController;
            _player = player;
            _spriteBatch = spriteBatch;
            _main = main;
            _orthographicCamera = orthographicCamera;
        }

        private static void DrawWorld(SpriteBatch batch, int layer)
        {
            foreach (var chunkpair in Chunks.LoadedChunks.Keys.Where(chunkpair => chunkpair.Item3 == layer))
            {
                batch.Draw(
                    BlockMaterials.Textures[Chunks.LoadedChunks[chunkpair].Name],
                    new Rectangle(
                        chunkpair.Item1 << IRestrictions.TileShift,
                        chunkpair.Item2 << IRestrictions.TileShift,
                        IRestrictions.TileSize, IRestrictions.TileSize
                    ),
                    Color.White
                );
            }
        }

        /*
        private void mouseCrosshair(SpriteBatch batch)
        {
            Vector3 mousePos = new Vector3(Gdx.input.getX(), Gdx.input.getY(), 0);
            Main.cam.unproject(mousePos);
            int x = mouse.getSelectedX(mousePos);
            int y = mouse.getSelectedY(mousePos);
            batch.draw(mouse.getCrosshair(), x, y);
        }
        */

        public void Draw()
        {
            //Higher means draws in a lower layer
            DrawWorld(_spriteBatch, 0);
            _inputController.Draw();
            DrawWorld(_spriteBatch, 1);
            _mouse.Draw();
            _inputController.PlayerInput(_main,_spriteBatch);
        }
    }
}