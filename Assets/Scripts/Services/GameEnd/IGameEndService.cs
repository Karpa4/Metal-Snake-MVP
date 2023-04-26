namespace Services.GameEnd
{
  public interface IGameEndService : IService
  {
        void FinishGame();
        void LoseGame();
  }
}