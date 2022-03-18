using SkiaSharp;
using Components.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ui.MauiX.Models
{
    /// <summary>
    /// Provides rendering information for a data series.
    /// </summary>
    public class DataSeriesRenderingInfo : NotifyPropertyChanged
    {
        #region Fields
        private string _name;
        private SKColor _color;
        #endregion

        #region Properties
        /// <summary>
        /// The name of the series.
        /// </summary>
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        /// <summary>
        /// The <see cref="SKColor"/> used to render the data series.
        /// </summary>
        public SKColor Color { get => _color; set => SetProperty(ref _color, value); }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new <see cref="DataSeriesRenderingInfo"/>.
        /// </summary>
        public DataSeriesRenderingInfo()
        {

        }

        /// <summary>
        /// Creates a new <see cref="DataSeriesRenderingInfo"/>.
        /// </summary>
        /// <param name="name">The name of the series.</param>
        /// <param name="color">The <see cref="SKColor"/> used to render the data series.</param>
        public DataSeriesRenderingInfo(string name, SKColor color)
        {
            Name = name;
            Color = color;
        }
        #endregion
    }
}
