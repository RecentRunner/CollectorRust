using Microsoft.Xna.Framework.Graphics;

namespace Collector.Dimension
{
    public class WorldRenderer : IRestrictions {
        //private Mouse mouse;
        //private InputController inputController;
        //private Player _player;

/*
    public WorldRenderer(InputController inputController, Mouse mouse, Player player) {
        this.inputController = inputController;
        this.mouse = mouse;
        this.player = player;
    }
*/
        public void DrawWorld(SpriteBatch batch, int layer)
        {
            /*
        foreach (var chunkpair in Chunks.LoadedChunks.Keys.Where(chunkpair => chunkpair.Item3 == layer))
        {
            batch.draw(
                //BlockMaterials.Textures[Chunks.LoadedChunks[chunkpair].getName()],
                chunkpair.Item1 << IRestrictions.TileShift,
                chunkpair.Item2 << IRestrictions.TileShift
            );
        }
        */
        }

/*
    private void mouseCrosshair(SpriteBatch batch) {
        Vector3 mousePos = new Vector3(Gdx.input.getX(), Gdx.input.getY(), 0);
        Main.cam.unproject(mousePos);
        int x = mouse.getSelectedX(mousePos);
        int y = mouse.getSelectedY(mousePos);
        batch.draw(mouse.getCrosshair(), x, y);
    }

    public void render(SpriteBatch batch, float timeSinceLastUpdate) {
        //Higher means draws in a lower layer
        drawWorld(batch,0);
        batch.draw(player.getAnimation().getKeyFrame(timeSinceLastUpdate, true), Player.x, Player.y,TILE_SIZE,TILE_SIZE);
        drawWorld(batch,1);
        mouseCrosshair(batch);
        inputController.handleInput();
        batch.setProjectionMatrix(Main.cam.combined);
    }
    */
    }
}
