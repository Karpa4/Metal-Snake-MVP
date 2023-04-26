using UnityEngine;
using Game;
using Services.Pause;

public class RefContainer : SingletonMono<RefContainer>
{
	[SerializeField] private MainPlayer mainPlayer;
	[SerializeField] private Camera mainCamera;
	[SerializeField] private FinishGameTrigger finishTrigger;
	[SerializeField] private PauseManager pauseManager;

	public FinishGameTrigger FinishTrigger { get => finishTrigger; }
	public PauseManager MainPauseManager { get => pauseManager; }
	public MainPlayer MainPlayer { get => mainPlayer; }
	public Camera MainCamera { get => mainCamera; }
}
