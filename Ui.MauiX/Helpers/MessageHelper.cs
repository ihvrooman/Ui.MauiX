using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ui.MauiX.Helpers
{
    /// <summary>
    /// Provides extended methods for sending messages via the <see cref="MessagingCenter"/>.
    /// </summary>
    public static class MessageHelper
    {
        #region Methods
        /// <summary>
        /// Sends a named message with the specified arguments asynchronously.
        /// </summary>
        /// <typeparam name="TSender">The type of object that sends the message.</typeparam>
        /// <typeparam name="TArgs">The type of the objects that are used as message arguments for the message.</typeparam>
        /// <param name="sender">The instance that is sending the message. Typically, this is specified with the this keyword used within the sending object.</param>
        /// <param name="message">The message that will be sent to objects that are listening for the message from instances of type TSender.</param>
        /// <param name="args">The arguments that will be passed to the listener's callback.</param>
        public static void SendAsync<TSender, TArgs>(TSender sender, string message, TArgs args) where TSender : class
        {
            Task.Run(() =>
            {
                MessagingCenter.Send(sender, message, args);
            });
        }

        /// <summary>
        /// Sends a named message that has no arguments asynchronously.
        /// </summary>
        /// <typeparam name="TSender">The type of object that sends the message.</typeparam>
        /// <param name="sender">The instance that is sending the message. Typically, this is specified with the this keyword used within the sending object.</param>
        /// <param name="message">The message that will be sent to objects that are listening for the message from instances of type TSender.</param>
        public static void SendAsync<TSender>(TSender sender, string message) where TSender : class
        {
            Task.Run(() =>
            {
                MessagingCenter.Send(sender, message);
            });
        }

        /// <summary>
        /// Subscribes to messages that match the provided message and are sent by an instance of type <see cref="Application"/>.
        /// </summary>
        /// <param name="subscriber">The object that is subscribing to the messages. Typically, this is specified with the this keyword used within the subscribing object.</param>
        /// <param name="message">The message that will be sent to objects that are listening for the message from instances of type <see cref="Application"/>.</param>
        /// <param name="callback">A callback, which takes the sending <see cref="Application"/> as a parameter, that is run when the message is received by the subscriber.</param>
        public static void SubscribeToApplicationMessages(this object subscriber, string message, Action<Application> callback)
        {
            MessagingCenter.Subscribe(subscriber, message, callback);
        }

        /// <summary>
        /// Unsubscribes from messages that match the provided message and are sent by an instance of type <see cref="Application"/>.
        /// </summary>
        /// <param name="subscriber">The object that is subscribing to the messages. Typically, this is specified with the this keyword used within the subscribing object.</param>
        /// <param name="message">The message that will be sent to objects that are listening for the message from instances of type <see cref="Application"/>.</param>
        public static void UnsubscribeFromApplicationMessages(this object subscriber, string message)
        {
            MessagingCenter.Unsubscribe<Application>(subscriber, message);
        }

        /// <summary>
        /// Sends a named message that has no arguments with the current <see cref="Application"/> as the sender.
        /// </summary>
        /// <param name="message">The message that will be sent to objects that are listening for the message from instances of type <see cref="Application"/>.</param>
        public static void SendMessageFromCurrentApplication(string message)
        {
            MessagingCenter.Send(Application.Current, message);
        }

        /// <summary>
        /// Sends a named message that has no arguments asynchronously with the current <see cref="Application"/> as the sender.
        /// </summary>
        /// <param name="message">The message that will be sent to objects that are listening for the message from instances of type <see cref="Application"/>.</param>
        public static void SendMessageFromCurrentApplicationAsync(string message)
        {
            SendAsync(Application.Current, message);
        }
        #endregion
    }
}
