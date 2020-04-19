package Collector.Character;

import Collector.Restrictions;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.math.Vector3;

public class Mouse implements Restrictions {
    Texture crosshair;

    public Mouse() {
        this.crosshair = new Texture("assets/crosshair.png");
    }

    public Texture getCrosshair() {
        return crosshair;
    }

    public int getSelectedX(Vector3 mousePos) {
        return ((int)(mousePos.x)>>4)<<4;
    }

    public int getSelectedY(Vector3 mousePos) {
        return ((int)(mousePos.y)>>4)<<4;
    }
}
