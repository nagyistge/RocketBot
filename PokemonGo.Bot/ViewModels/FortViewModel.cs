﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using POGOProtos.Map.Fort;
using System.Threading.Tasks;

namespace PokemonGo.Bot.ViewModels
{
    public class FortViewModel : ViewModelBase
    {
        protected readonly SessionViewModel session;

        public Position2DViewModel Position { get; }

        public string Id { get; }
        #region Details

        AsyncRelayCommand details;

        public AsyncRelayCommand Details
        {
            get
            {
                if (details == null)
                    details = new AsyncRelayCommand(ExecuteDetails, CanExecuteDetails);

                return details;
            }
        }

        async Task ExecuteDetails()
        {
            var fortInfo = await session.GetFortDetails(Id, Position.Latitude, Position.Longitude);
        }

        bool CanExecuteDetails() => true;

        #endregion Details


        public FortViewModel(FortData fort, SessionViewModel session)
        {
            Position = new Position2DViewModel(fort.Latitude, fort.Longitude);
            Id = fort.Id;
            this.session = session;
        }

        public override int GetHashCode() => Id.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as FortViewModel);
        public bool Equals(FortViewModel other) => Id == other?.Id;
    }
}