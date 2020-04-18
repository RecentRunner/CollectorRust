package Collector;

import Collector.Dimension.*;
import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.GL30;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.utils.viewport.ExtendViewport;
import Collector.Character.InputController;
import Collector.Character.Mouse;
import Collector.Character.Player;
import Collector.UI.GUI;

import static Collector.Restrictions.*;

public class Main extends ApplicationAdapter {
	private Stage stage;
	private SpriteBatch batch;
	private WorldRenderer worldRenderer;
	private GUI gui;
	private ExtendViewport extendViewport;
	public static float chunkLoadingTime = 0;
	float playerAnimationTime = 0f; //accumulator
	public static OrthographicCamera cam;
	private InputController inputController;
	private Mouse mouse;
	private Player player;


	@Override
	public void create () {
		Gdx.graphics.setWindowedMode(1024, 576);

		//Declaring all objects needed for the game
		cam = new OrthographicCamera(VIEWPORT_WIDTH, VIEWPORT_HEIGHT);
		extendViewport = new ExtendViewport(VIEWPORT_WIDTH, VIEWPORT_HEIGHT, cam);
		mouse = new Mouse();
		player = new Player(0, 0);
		inputController = new InputController(player,mouse);
		worldRenderer = new WorldRenderer(inputController,mouse,player);
		batch = new SpriteBatch();
		stage = new Stage();
		gui = new GUI(stage);

		Gdx.input.setInputProcessor(stage);

		BlockMaterials.create();
		Chunks.createStructures();

		//cam.translate(TILE_SIZE, TILE_SIZE);
		cam.zoom = 25f;
	}

	@Override
	public void resize(int width, int height) {
		stage.getViewport().update(width, height, true);
	}

	@Override
	public void render () {
		float deltaTime = Gdx.graphics.getDeltaTime();
		Gdx.gl.glClear(GL30.GL_COLOR_BUFFER_BIT);

		playerAnimationTime += deltaTime;
		chunkLoadingTime += deltaTime;

		gui.debugInfo(stage,mouse);

		stage.act(deltaTime);
		batch.begin();
		worldRenderer.render(batch,playerAnimationTime);

		if (chunkLoadingTime > RENDER_TIME) {
			chunkLoadingTime -= RENDER_TIME;
			World.loadChunks();
			World.unloadChunks();
		}

		cam.update();
		batch.end();
		stage.draw();
	}


	@Override
	public void dispose () {
		gui.dispose();
		player.dispose();
		stage.dispose();
		batch.dispose();
	}
}
