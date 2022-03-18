using System;
using Ui.Models;
using System.Collections;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ui.MauiX.Resources;
using Ui.MauiX.Helpers;
using System.Linq;
using System.Collections.Generic;
using DataCollections.Models;
using Ui.MauiX.Models;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A popup that displays a collection of items for user selection.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectionPopup : BaseInputControl
    {
        #region Fields
        private IEnumerable _itemsSource;
        private DataTemplate _selectionItemTemplate;
        private SelectionSettings _settings = new SelectionSettings()
        {
            ButtonSize = 30,
            BackgroundColor = CustomColors.BackgroundColor,
        };
        #endregion

        #region Properties
        /// <summary>
        /// The source of the items to template and display.
        /// </summary>
        public IEnumerable ItemsSource { get => _itemsSource; private set => SetProperty(ref _itemsSource, value); }

        /// <summary>
        /// The <see cref="DataTemplate"/> used to render the items on the <see cref="CustomControls.SelectionControl"/>.
        /// </summary>
        public DataTemplate SelectionItemTemplate { get => _selectionItemTemplate; private set => SetProperty(ref _selectionItemTemplate, value); }

        public override object Value
        {
            get
            {
                object selectedItems = SelectionControl.SelectedItems;
                if (Settings.SelectionMode == SelectionMode.Single)
                {
                    selectedItems = ((IEnumerable<object>)selectedItems).FirstOrDefault();
                }
                return selectedItems;
            }
            set => throw new InvalidOperationException();
        }

        internal override string ClearedValue => string.Empty;

        public static Thickness DefaultPadding { get; set; } = new Thickness(225, 30);

        public new Thickness Padding => DefaultPadding;

        public override CornerRadius CornerRadius => new CornerRadius(5);

        public SelectionSettings Settings { get => _settings; private set => SetProperty(ref _settings, value); }

        public bool AutoCloseOnItemSelected { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="SelectionPopup"/>.
        /// </summary>
        /// <param name="itemsSource">The source of the items to template and display.</param>
        internal SelectionPopup(IEnumerable itemsSource)
        {
            ItemsSource = itemsSource;
            ContentMargin = new Thickness(0, 0, 0, 10);
            IsInvalidTipVisible = false;
            InitializeComponent();
            DoneCommand.ChangeCanExecute();
            SetDefaultSelectionItemTemplate();
        }
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Shows an instance of the <see cref="SelectionPopup"/>.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which the <see cref="SelectionPopup"/> is to be shown.</param>
        /// <param name="itemsSource">The source of the items to template and display.</param>
        /// <param name="iconResourceId">The icon resource id.</param>
        /// <param name="label">The label which describes the value that is being collected from the user.</param>
        /// <param name="valueSuffix">The suffix of the value.
        /// <para>The unit of measure would be a common value for this property.</para></param>
        /// <param name="itemTemplate">The <see cref="DataTemplate"/> used to render items.</param>
        /// <param name="settingsOverride">The <see cref="SelectionSettings"/> override.</param>
        /// <param name="autoCloseOnItemSelected">Indicates whether or not the popup will automatically close as soon as the user selects an item.
        /// <para>Note: This only applies if the <see cref="Xamarin.Forms.SelectionMode"/> is set to <see cref="Xamarin.Forms.SelectionMode.Single"/>.</para></param>
        /// <returns></returns>
        public static async Task<PopupResult> Show(NavigableElement navigableElement, IEnumerable itemsSource, string iconResourceId = null, string label = null, string valueSuffix = null, DataTemplate itemTemplate = null, SelectionSettings settingsOverride = null, bool autoCloseOnItemSelected = false)
        {
            return await Device.InvokeOnMainThreadAsync(async () =>
            {
                var popupPushed = false;
                SelectionPopup popup = null;
                try
                {
                    popup = new SelectionPopup(itemsSource)
                    {
                        Label = label,
                        IconResourceId = iconResourceId,
                        ValueSuffix = valueSuffix,
                        AutoCloseOnItemSelected = autoCloseOnItemSelected,
                    };
                    if (itemTemplate != null)
                    {
                        popup.SelectionItemTemplate = itemTemplate;
                        popup.ItemTemplate = itemTemplate;
                    }
                    if (settingsOverride != null)
                    {
                        popup.Settings = settingsOverride;
                    }
                    await navigableElement.OpenModalPopup(popup);
                    popupPushed = true;

                    await popup.WaitForResultToBeSet();

                    return popup.Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (popupPushed)
                    {
                        await navigableElement.CloseModalPopup(popup);
                    }
                }
            });
        }
        #endregion

        #region Protected
        protected override void SetResult(bool userCancelled)
        {
            Result = new PopupResult(userCancelled, Value);
        }

        protected override bool CanExecuteDoneCommand(object arg)
        {
            return SelectionControl.SelectedItems.Count() > 0;
        }
        #endregion

        #region Private
        private void SelectionControl_SelectedItemsChanged(object sender, IEnumerable<object> e)
        {
            DoneCommand.ChangeCanExecute();
            OnPropertyChanged(nameof(Value));

            if (Settings.SelectionMode == SelectionMode.Single && AutoCloseOnItemSelected && DoneCommand.CanExecute(null))
            {
                DoneCommand.Execute(null);
            }
        }

        private void SetDefaultSelectionItemTemplate()
        {
            if (SelectionItemTemplate != null)
            {
                return;
            }

            SelectionItemTemplate = (DataTemplate)Resources["SelectionControlRadioButtonDataTemplate"];
        }
        #endregion
        #endregion
    }
}