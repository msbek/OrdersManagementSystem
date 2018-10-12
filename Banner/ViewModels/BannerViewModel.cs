﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Main;
using Infrastructure;
using Prism.Regions;
using Infrastructure.Base;
using Infrastructure.Events;
using MaterialDesignColors;
using Prism.Events;
using MaterialDesignThemes.Wpf;

namespace Banner.ViewModels
{
    public class BannerViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        public BannerViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            Title = "Orders Management System";
            _regionManager = regionManager;
            NavigateHomeCommand = new DelegateCommand(NavigateHome);
            ChangeThemeCommand = new DelegateCommand<string>(ChangeTheme);
            eventAggregator.GetEvent<OnNavigatedToEvent>().Subscribe(title =>
                State = title);
        }
        public DelegateCommand NavigateHomeCommand { get; set; }
        private void NavigateHome()
        {
            var contentRegion = _regionManager.Regions[RegionNames.ContentRegion];
            while (contentRegion.NavigationService.Journal.CanGoBack)
            {
                contentRegion.NavigationService.Journal.GoBack();
            }
        }

        public DelegateCommand<string> ChangeThemeCommand { get; set; }
        private void ChangeTheme(string color)
        {
            var helper = new PaletteHelper();
            var swatches = new SwatchesProvider().Swatches;
            helper.ReplacePrimaryColor(swatches.FirstOrDefault(s => s.Name == color.ToLower()));
        }

        private string _state;
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

    }
}
