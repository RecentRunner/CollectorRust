using System;
using System.Collections.Generic;
using System.Linq;
using Collector.Character;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Collector.Dimension
{
    public class WorldRenderer : IRestrictions
    {
        private readonly PlayerMouse _playerMouse;
        private readonly InputController _inputController;
        private readonly SpriteBatch _spriteBatch;
        private readonly World _world;
        private const int TileSize = 32;

        private readonly Dictionary<int, Rectangle> _wangTileset = new Dictionary<int, Rectangle>
        {
            [0] = new Rectangle(TileSize, 0, TileSize, TileSize),
            [1] = new Rectangle(0, 3 * TileSize, TileSize, TileSize),
            [2] = new Rectangle(0, TileSize, TileSize, TileSize),
            [3] = new Rectangle(0, 2 * TileSize, TileSize, TileSize),
            [4] = new Rectangle(2 * TileSize, TileSize, TileSize, TileSize),
            [5] = new Rectangle(3 * TileSize, 3 * TileSize, TileSize, TileSize),
            [6] = new Rectangle(TileSize,  TileSize, TileSize, TileSize),
            [7] = new Rectangle(4 * TileSize, 2 * TileSize, TileSize, TileSize),
            [8] = new Rectangle(2 * TileSize, 3 * TileSize, TileSize, TileSize),
            [9] = new Rectangle(TileSize, 3 * TileSize, TileSize, TileSize),
            [10] = new Rectangle(4 * TileSize, 3 * TileSize, TileSize, TileSize),
            [11] = new Rectangle(4 * TileSize, TileSize, TileSize, TileSize),
            [12] = new Rectangle(2 * TileSize, 2 * TileSize, TileSize, TileSize),
            [13] = new Rectangle(3 * TileSize, TileSize, TileSize, TileSize),
            [14] = new Rectangle(3 * TileSize, 2 * TileSize, TileSize, TileSize),
            [15] = new Rectangle(TileSize, 2 * TileSize, TileSize, TileSize),
        };


        public WorldRenderer(PlayerMouse playerMouse, InputController inputController, SpriteBatch spriteBatch,
            World world)
        {
            _playerMouse = playerMouse;
            _inputController = inputController;
            _spriteBatch = spriteBatch;
            _world = world;

            _world.Impassable.Add(Blocks.BlockWater);
            _world.Impassable.Add(Blocks.BlockRoof);
            _world.Impassable.Add(Blocks.BlockWall);
        }

        private void DrawWorld(SpriteBatch batch, int layer)
        {
            foreach (var chunkpair in _world.LoadedChunks.Keys.Where(chunkpair => layer == chunkpair.Item3))
            {
                if (!Main.Wang.Contains(_world.SavedChunks[chunkpair]))
                {
                    DrawTile(batch, chunkpair);
                }
                else
                {
                    DrawWang(batch, chunkpair);
                }
            }
        }

        private void DrawTile(SpriteBatch batch, Tuple<int, int, int> chunkpair)
        {
            var (tempX, tempY, _) = chunkpair;
            batch.Draw(
                Main.Materials[_world.LoadedChunks[chunkpair].ToString()],
                new Rectangle(
                    tempX,
                    tempY,
                    IRestrictions.TileSize, IRestrictions.TileSize
                ),
                Color.White
            );
        }

        private void DrawWang(SpriteBatch batch, Tuple<int, int, int> chunkpair)
        {
            var sum = 0;
            var (tempX, tempY, _) = chunkpair;
            var wangBlock = _world.LoadedChunks[chunkpair];

            if (_world.GetTerrain(tempX , tempY - 1) != wangBlock) sum += 1;
            if (_world.GetTerrain(tempX + 1, tempY) != wangBlock) sum += 2;
            if (_world.GetTerrain(tempX, tempY + 1) != wangBlock) sum += 4;
            if (_world.GetTerrain(tempX - 1, tempY) != wangBlock) sum += 8;

            batch.Draw(
                Main.Materials[wangBlock.ToString()],
                new Rectangle(
                    tempX,
                    tempY,
                    IRestrictions.TileSize, IRestrictions.TileSize
                ),
                _wangTileset[sum],
                Color.White
            );
        }

        public void Draw(GameTime gameTime)
        {
            //Higher means draws in a lower layer
            DrawWorld(_spriteBatch, 0);
            _playerMouse.Draw();
            DrawWorld(_spriteBatch, 1);
            _inputController.Draw();
            _inputController.PlayerInput(gameTime);
            DrawWorld(_spriteBatch, 2);
        }
    }
}