using System.Collections.Generic;
using UnityEngine;
using Game.PlayerConstructor;
using System.Threading.Tasks;
using System;
using UI.Windows.Garage;

namespace Services.PlayerData
{
    public class PlayerPartsData : IPlayerPartsData
    {
        private List<List<Detail>> allDetails = new List<List<Detail>>();
        private List<List<Detail>> playerDetails = new List<List<Detail>>();
        private List<int> currentDetailsIndexes = new List<int>();
        private bool isPainted = true;
        private ConstructCar menuConstructCar;
        private Body currentBody;
        private List<DetailsList> allDetailsStorage;

        public Body CurrentBody { get => currentBody; }
        public List<List<Detail>> AllDetails { get => allDetails; }
        public List<List<Detail>> PlayerDetails { get => playerDetails; }
        public List<int> CurrentDetailsIndexes { get => currentDetailsIndexes; }

        private const string FileNameForPlayerParts = "/PlayerDetails.dat";
        private const string FileNameForCurrentParts = "/CurrentDetails.dat";

        private List<int> StartDetailsIndexes = new List<int> { 0, 0, 0, 0, 0, 0, 0 };

        private List<List<int>> StartDetailIDs = new List<List<int>>
        {   new List<int>(){0},
            new List<int>(){100},
            new List<int>(){200},
            new List<int>(){300},
            new List<int>(){400},
            new List<int>(){500},
            new List<int>(){600}
        };

        public PlayerPartsData()
        {
            allDetailsStorage = Resources.Load<AllDetails>(ConstantVariables.AllDetailsPath).AllDetailsLists;
            for (int i = 0; i < allDetailsStorage.Count; i++)
            {
                allDetails.Add(new List<Detail>());
                for (int j = 0; j < allDetailsStorage[i].Details.Count; j++)
                {
                    allDetails[i].Add(allDetailsStorage[i].Details[j]);
                }
            }
            LoadPlayerParts();
            LoadCurrentParts();
            currentBody = (Body)playerDetails[0][currentDetailsIndexes[0]];
        }

        public void SetNewCurrentDetail(DetailType type, int newDetailIndex)
        {

            // при замене кузова меняется currentBody


            if (type == DetailType.Color)
            {
                Color newColor = currentBody.Colors[newDetailIndex];
                currentBody.ChangeColor(newDetailIndex);
                menuConstructCar.SetColor(newColor);
            }
            else
            {
                int categoryIndex = (int)type;
                currentDetailsIndexes[categoryIndex] = newDetailIndex;
                menuConstructCar.ChangeDetailInMainMenu(playerDetails[categoryIndex][newDetailIndex], type);
            }
        }

        public void SetCurrentDetails(ConstructCar constructCar)
        {
            List<Detail> currentDetails = GetCurrentDetails();
            constructCar.NewConstruct(currentDetails);
            if (isPainted)
            {
                Color currentColor = currentBody.GetCurrentColor();
                constructCar.SetColor(currentColor);
            }
        }

        /// <summary>
        /// Установка выбранных деталей в конструктор игрока в главном меню при запуске главного меню
        /// </summary>
        /// <param name="menuConstructCar">Конструтор игрока в главном меню</param>
        public void SetupMenuConstructCar(ConstructCar menuConstructCar)
        {
            this.menuConstructCar = menuConstructCar;
            SetCurrentDetails(this.menuConstructCar);
        }

        /// <summary>
        /// Установка выброанных деталей в конструктор гаража при его открытии
        /// </summary>
        public void SetupGarageConstructCar()
        {
            UIGarageConstructCar.instance.StartConstruct(GetCurrentUISprites(), GetCurrentOffsets());
            UIGarageConstructCar.instance.ChangeColor(CurrentBody.GetCurrentColor());
            SecondWeapon secondWeapon = (SecondWeapon)playerDetails[5][currentDetailsIndexes[5]];
            UIGarageConstructCar.instance.ChangeSecondStandSprite(secondWeapon.SecondStandUISprite);
        }

        private List<Sprite> GetCurrentUISprites()
        {
            List<Sprite> currentSprites = new List<Sprite>();
            for (int i = 0; i < currentDetailsIndexes.Count; i++)
            {
                currentSprites.Add(playerDetails[i][currentDetailsIndexes[i]].GarageDetailSprite);
            }
            return currentSprites;
        }

        private List<Vector2> GetCurrentOffsets()
        {
            List<Vector2> result = new List<Vector2>();
            for (int i = 0; i < currentDetailsIndexes.Count; i++)
            {
                result.Add(playerDetails[i][currentDetailsIndexes[i]].GarageOffset);
            }
            return result;
        }

        private void LoadCurrentParts()
        {
            if (SerializeBinaryService.CheckFileExists(FileNameForCurrentParts))
            {
                currentDetailsIndexes = SerializeBinaryService.Load(ref currentDetailsIndexes, FileNameForCurrentParts);
            }
            else
            {
                currentDetailsIndexes = StartDetailsIndexes;
            }
        }

        private void LoadPlayerParts()
        {
            List<List<int>> LoadListID = new List<List<int>>();
            if (SerializeBinaryService.CheckFileExists(FileNameForPlayerParts))
            {
                LoadListID = SerializeBinaryService.Load(ref LoadListID, FileNameForPlayerParts);
            }
            else
            {
                LoadListID = StartDetailIDs;
            }

            ConstructPlayerParts(LoadListID);
        }

        private void ConstructPlayerParts(List<List<int>> LoadListID)
        {
            for (int i = 0; i < LoadListID.Count; i++)
            {
                playerDetails.Add(new List<Detail>());
                for (int j = 0; j < LoadListID[i].Count; j++)
                {
                    int index = allDetails[i].FindIndex(x => x.ID == LoadListID[i][j]);
                    playerDetails[i].Add(allDetails[i][index]);
                    allDetails[i].RemoveAt(index);
                }
            }
        }

        public void SavePlayerParts()
        {
            List<List<int>> SaveListID = new List<List<int>>();
            for (int i = 0; i < playerDetails.Count; i++)
            {
                SaveListID.Add(new List<int>());
                for (int j = 0; j < playerDetails[i].Count; j++)
                {
                    SaveListID[i].Add(playerDetails[i][j].ID);
                }
            }

            SerializeBinaryService.Save(SaveListID, FileNameForPlayerParts);
        }

        public void SaveCurrentParts()
        {
            SerializeBinaryService.Save(currentDetailsIndexes, FileNameForCurrentParts);
        }

        public void Cleanup()
        {
            SavePlayerParts();
            SaveCurrentParts();
        }

        public void Reset()
        {
            currentDetailsIndexes = new List<int> { 0, 0, 0, 0, 0, 0, 0 }; ;
            playerDetails.Clear();
            ResetDetails();
        }

        private void ResetDetails()
        {
            allDetails = new List<List<Detail>>();
            for (int i = 0; i < allDetailsStorage.Count; i++)
            {
                allDetails.Add(new List<Detail>());
                for (int j = 0; j < allDetailsStorage[i].Details.Count; j++)
                {
                    allDetails[i].Add(allDetailsStorage[i].Details[j]);
                }
            }
            ConstructPlayerParts(StartDetailIDs);
            if (menuConstructCar != null)
            {
                SetCurrentDetails(menuConstructCar);
            }
            SaveCurrentParts();
            SavePlayerParts();
        }

        private List<Detail> GetCurrentDetails()
        {
            List<Detail> currentDetails = new List<Detail>();
            for (int i = 0; i < currentDetailsIndexes.Count; i++)
            {
                currentDetails.Add(playerDetails[i][currentDetailsIndexes[i]]);
            }
            return currentDetails;
        }
    }
}
