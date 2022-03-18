using Ui.MauiX.CustomControls.ViewModels;
using Ui.MauiX.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DynamicWorkflow : ContentView
    {
        #region Fields
        private ScrollView _scrollView;
        #endregion

        #region Properties
        /// <summary>
        /// The viewmodel used as the binding context for the control.
        /// <para>Note: The viewmodel provided must inherit from <see cref="BaseWorkflowViewModel"/>.</para>
        /// </summary>
        public BaseWorkflowViewModel ViewModel
        {
            private get => WorkflowView.BindingContext == null ? null : (BaseWorkflowViewModel)WorkflowView.BindingContext;
            set
            {
                if (WorkflowView.BindingContext is BaseWorkflowViewModel oldViewModel)
                {
                    oldViewModel.PropertyChanged -= ViewModel_PropertyChanged;

                    if (oldViewModel.Steps is INotifyCollectionChanged steps)
                    {
                        steps.CollectionChanged -= ScrollableItems_CollectionChanged;
                    }

                    if (oldViewModel is BaseChatWorkflowViewModel c && c.Messages is INotifyCollectionChanged m)
                    {
                        m.CollectionChanged -= ScrollableItems_CollectionChanged;
                    }
                }

                WorkflowView.BindingContext = value;

                if (WorkflowView.BindingContext is BaseWorkflowViewModel newViewModel)
                {
                    newViewModel.PropertyChanged += ViewModel_PropertyChanged;

                    if (newViewModel.Steps is INotifyCollectionChanged steps)
                    {
                        steps.CollectionChanged += ScrollableItems_CollectionChanged;
                    }

                    SetWorkflowView();

                    if (newViewModel is BaseChatWorkflowViewModel c && c.Messages is INotifyCollectionChanged m)
                    {
                        m.CollectionChanged += ScrollableItems_CollectionChanged;
                    }
                }
            }
        }

        /// <summary>
        /// The control's micro font size.
        /// </summary>
        public double MicroFontSize
        {
            get => (double)GetValue(MicroFontSizeProperty);
            set => SetValue(MicroFontSizeProperty, value);
        }

        /// <summary>
        /// The micro font size property.
        /// </summary>
        public static readonly BindableProperty MicroFontSizeProperty = BindableProperty.Create(nameof(MicroFontSize), typeof(double), typeof(DynamicWorkflow), 10.0);

        /// <summary>
        /// The control's small font size.
        /// </summary>
        public double SmallFontSize
        {
            get => (double)GetValue(SmallFontSizeProperty);
            set => SetValue(SmallFontSizeProperty, value);
        }

        /// <summary>
        /// The small font size property.
        /// </summary>
        public static readonly BindableProperty SmallFontSizeProperty = BindableProperty.Create(nameof(SmallFontSize), typeof(double), typeof(DynamicWorkflow), 12.0);

        /// <summary>
        /// The control's medium font size.
        /// </summary>
        public double MediumFontSize
        {
            get => (double)GetValue(MediumFontSizeProperty);
            set => SetValue(MediumFontSizeProperty, value);
        }

        /// <summary>
        /// The medium font size property.
        /// </summary>
        public static readonly BindableProperty MediumFontSizeProperty = BindableProperty.Create(nameof(MediumFontSize), typeof(double), typeof(DynamicWorkflow), 14.0);
        #endregion

        #region Constructor
        public DynamicWorkflow()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BaseWorkflowViewModel.WorkflowType))
            {
                SetWorkflowView();
            }
            else if (e.PropertyName == nameof(BaseWorkflowViewModel.CurrentSubStep))
            {
                ScrollLastItemIntoView();
            }
        }

        private void SetWorkflowView()
        {
            var key = string.Empty;
            switch (ViewModel.WorkflowType)
            {
                case WorkflowType.Menu:
                    key = "MenuWorkflowDataTemplate";
                    break;
                case WorkflowType.Process:
                    key = "ProcessWorkflowDataTemplate";
                    break;
                case WorkflowType.Chat:
                    key = "ChatWorkflowDataTemplate";
                    break;
                default:
                    break;
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }

            var dataTemplate = (DataTemplate)Resources[key];
            WorkflowView.Content = (View)dataTemplate.CreateContent();

            _scrollView = WorkflowView.Content is ScrollView s ? s : null;
            if (WorkflowView.Content is ScrollView s1)
            {
                _scrollView = s1;
            }
            else if (WorkflowView.Content is Grid g)
            {
                var view = g.Children.FirstOrDefault(c => c is ScrollView);
                _scrollView = view != null ? (ScrollView)view : null;
            }
        }

        private  void ScrollableItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ScrollLastItemIntoView();
        }

        private void ScrollLastItemIntoView()
        {
            if (_scrollView == null || ViewModel == null || !ViewModel.AutoScrollLastItemIntoView)
            {
                return;
            }

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(500);
                await _scrollView.ScrollToAsync(_scrollView.Content, ScrollToPosition.End, true);

                if (Device.RuntimePlatform == Device.GTK)
                {
                    await Task.Delay(1000);
                    await _scrollView.ScrollToAsync(_scrollView.Content, ScrollToPosition.End, true);
                }
            });
        }
        #endregion
    }
}