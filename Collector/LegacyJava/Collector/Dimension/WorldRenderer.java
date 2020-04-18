package Collector.Dimension;

import Collector.Character.Player;
import Collector.Main;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.math.Vector3;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.github.czyzby.kiwi.util.tuple.immutable.Triple;
import Collector.Character.InputController;
import Collector.Character.Mouse;
import Collector.Restrictions;

import java.util.Iterator;

public class WorldRenderer implements Restrictions {
    private Mouse mouse;
    private InputController inputController;
    private Player player;

    public WorldRenderer(InputController inputController, Mouse mouse, Player player) {
        this.inputController = inputController;
        this.mouse = mouse;
        this.player = player;
    }

    public void drawWorld(SpriteBatch batch, int layer) {
        for (Triple<Integer, Integer, Integer> chunkpair : Chunks.loadedChunks.keySet()) {
            if (chunkpair.getThird() == layer) {
                batch.draw(
                        BlockMaterials.textures.get(Chunks.loadedChunks.get(chunkpair).getName()),
                        chunkpair.getFirst() << TILE_SHIFT,
                        chunkpair.getSecond() << TILE_SHIFT
                );
            }
        }
    }

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
}
