using Ui.MauiX.Enums;
using Ui.MauiX.Models;
using Ui.MauiX.Test.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ui.MauiX.Test.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<ChatMessage> _messages = new ObservableCollection<ChatMessage>();
        private ChatSettings _settings = new ChatSettings();
        #endregion

        #region Properties
        public ObservableCollection<ChatMessage> Messages { get => _messages; set => SetProperty(ref _messages, value); }

        public ChatSettings Settings { get => _settings; set => SetProperty(ref _settings, value); }
        #endregion

        #region Constructor
        public ChatViewModel()
        {
            SupplementalPageControlTypes.Add(typeof(ChatSupplementalPageControls));

            BackgroundColor = Settings.BackgroundColor;
            CornerRadius = Settings.MessageCornerRadius;
            SetIndividualCornerRadiusProperties();
            FontSize = (int)Settings.MessageFontSize;

            base.PropertyChanged += BaseViewModel_PropertyChanged;

            Task.Run(async () =>
            {
                var extraString = " Monologues are similar to poems, epiphanies, and others, in that, they involve one 'voice' speaking but there are differences between them. For example, a soliloquy involves a character relating their thoughts and feelings to themself and to the audience without addressing any of the other characters. A monologue is the thoughts of a person spoken out loud.[3] Monologues are also distinct from apostrophes, in which the speaker or writer addresses an imaginary person, inanimate object, or idea.[4] Asides differ from each of these not only in length (asides are shorter) but also in that asides are not heard by other characters even in situations where they logically should be (e.g. two characters engaging in a dialogue interrupted by one of them delivering an aside).";
                for (int i = 0; i < 10; i++)
                {
                    var prompt = new ChatMessage() { Message = $"Prompt {i}", Type = ChatMessageType.NonUserSourced };
                    if (i == 1)
                    {
                        prompt.Message = prompt.Message.Insert(6, extraString);
                    }
                    Device.BeginInvokeOnMainThread(() => Messages.Add(prompt));
                    await Task.Delay(2500);

                    var reply = new ChatMessage() { Message = $"Reply {i}", Type = ChatMessageType.UserSourced };
                    if (i == 1)
                    {
                        reply.Message = prompt.Message.Insert(5, extraString);
                    }
                    Device.BeginInvokeOnMainThread(() => Messages.Add(reply));
                    await Task.Delay(1500);
                }
            });
        }
        #endregion

        #region Methods
        private void BaseViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BackgroundColor))
            {
                Settings.BackgroundColor = BackgroundColor;
            }
            else if (e.PropertyName == nameof(CornerRadius))
            {
                Settings.MessageCornerRadius = CornerRadius;
            }
            else if (e.PropertyName == nameof(FontSize))
            {
                Settings.MessageFontSize = FontSize;
            }
        }
        #endregion
    }
}
