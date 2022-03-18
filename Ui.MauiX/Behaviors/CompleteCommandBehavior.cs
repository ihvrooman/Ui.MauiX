using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ui.MauiX.Behaviors
{
    public class CompleteCommandBehavior : Behavior<Entry>
    {
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                propertyName: nameof(Command),
                returnType: typeof(ICommand),
                declaringType: typeof(ContentView),
                defaultValue: null);

        protected override void OnAttachedTo(Entry bindTo)
        {
            bindTo.Completed += OnCompleted;

            base.OnAttachedTo(bindTo);
        }

        protected override void OnDetachingFrom(Entry bindTo)
        {
            bindTo.Completed -= OnCompleted;

            base.OnDetachingFrom(bindTo);
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            string value = ((Entry)sender).Text;
            if (Command != null)
                Command.Execute(value);
        }

    }
}
