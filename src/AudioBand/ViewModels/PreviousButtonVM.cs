﻿using System;
using System.Diagnostics;
using System.Drawing;
using AudioBand.Extensions;
using AudioBand.Models;
using AudioBand.Resources;
using AudioBand.Settings;
using Svg;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace AudioBand.ViewModels
{
    /// <summary>
    /// View model for the previous button.
    /// </summary>
    public class PreviousButtonVM : ViewModelBase<PreviousButton>
    {
        private readonly IAppSettings _appsettings;
        private readonly IResourceLoader _resourceLoader;
        private Image _image;

        /// <summary>
        /// Initializes a new instance of the <see cref="PreviousButtonVM"/> class.
        /// </summary>
        /// <param name="appsettings">The app settings.</param>
        /// <param name="resourceLoader">The resource loader.</param>
        /// <param name="dialogService">The dialog service</param>
        public PreviousButtonVM(IAppSettings appsettings, IResourceLoader resourceLoader, IDialogService dialogService)
            : base(appsettings.PreviousButton)
        {
            DialogService = dialogService;
            _appsettings = appsettings;
            _resourceLoader = resourceLoader;
            LoadImage();

            _appsettings.ProfileChanged += AppsettingsOnProfileChanged;
        }

        public Image Image
        {
            get => _image;
            set => SetProperty(ref _image, value, trackChanges: false);
        }

        [PropertyChangeBinding(nameof(PreviousButton.ImagePath))]
        public string ImagePath
        {
            get => Model.ImagePath;
            set
            {
                if (SetProperty(nameof(Model.ImagePath), value))
                {
                    Image?.Dispose();
                    Image = _resourceLoader.TryLoadImageFromPath(value, _resourceLoader.DefaultPreviousImage).Draw(Width, Height);
                }
            }
        }

        [PropertyChangeBinding(nameof(PreviousButton.IsVisible))]
        public bool IsVisible
        {
            get => Model.IsVisible;
            set => SetProperty(nameof(Model.IsVisible), value);
        }

        [PropertyChangeBinding(nameof(PreviousButton.Width))]
        [AlsoNotify(nameof(Size))]
        public int Width
        {
            get => Model.Width;
            set => SetProperty(nameof(Model.Width), value);
        }

        [PropertyChangeBinding(nameof(PreviousButton.Height))]
        [AlsoNotify(nameof(Size))]
        public int Height
        {
            get => Model.Height;
            set => SetProperty(nameof(Model.Height), value);
        }

        [PropertyChangeBinding(nameof(PreviousButton.XPosition))]
        [AlsoNotify(nameof(Location))]
        public int XPosition
        {
            get => Model.XPosition;
            set => SetProperty(nameof(Model.XPosition), value);
        }

        [PropertyChangeBinding(nameof(PreviousButton.YPosition))]
        [AlsoNotify(nameof(Location))]
        public int YPosition
        {
            get => Model.YPosition;
            set => SetProperty(nameof(Model.YPosition), value);
        }

        /// <summary>
        /// Gets the location of the button.
        /// </summary>
        public Point Location => new Point(Model.XPosition, Model.YPosition);

        /// <summary>
        /// Gets the size of the button.
        /// </summary>
        public Size Size => new Size(Width, Height);

        /// <summary>
        /// Gets the dialog service
        /// </summary>
        public IDialogService DialogService { get; }

        /// <inheritdoc/>
        protected override void OnReset()
        {
            base.OnReset();
            LoadImage();
        }

        /// <inheritdoc/>
        protected override void OnCancelEdit()
        {
            base.OnCancelEdit();
            LoadImage();
        }

        private void LoadImage()
        {
            Image = _resourceLoader.TryLoadImageFromPath(ImagePath, _resourceLoader.DefaultPreviousImage).Draw(Width, Height);
        }

        private void AppsettingsOnProfileChanged(object sender, EventArgs e)
        {
            Debug.Assert(IsEditing == false, "Should not be editing");
            ReplaceModel(_appsettings.PreviousButton);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
