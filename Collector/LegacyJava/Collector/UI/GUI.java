package Collector.UI;

import Collector.Character.Mouse;
import Collector.Main;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.math.Vector3;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.Label;
import com.badlogic.gdx.scenes.scene2d.ui.Table;
import com.badlogic.gdx.utils.Align;
import com.badlogic.gdx.utils.viewport.ExtendViewport;
import Collector.Restrictions;

public class GUI implements Restrictions {
    private final BitmapFont font;
    private ExtendViewport guiViewport;
    private final Table table;
    private final Label label;

    public GUI(Stage stage) {
        font = new BitmapFont();
        table = new Table();
        table.setDebug(true);
        guiViewport = new ExtendViewport(Gdx.graphics.getWidth(),Gdx.graphics.getHeight(),stage.getCamera());
        stage.setViewport(guiViewport);

        //Creating block selection debug menu

        table.top().left();
        label = new Label("x=N/A y=N/A", new Label.LabelStyle(font,Color.WHITE));
        table.add(label);

        //Creating Minimap


    }

    public void debugInfo(Stage stage, Mouse mouse){
        table.setFillParent(true);
        stage.addActor(table);

        //Mouse position
        Vector3 mousePos = new Vector3(Gdx.input.getX(), Gdx.input.getY(), 0);
        Main.cam.unproject(mousePos);
        int x = mouse.getSelectedX(mousePos) >> TILE_SHIFT;
        int y = mouse.getSelectedY(mousePos) >> TILE_SHIFT;
        label.setText("x="+ x + " y=" + y);
    }

    public void dispose(){
        font.dispose();
    }
}
