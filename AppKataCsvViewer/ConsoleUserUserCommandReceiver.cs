using System;

namespace AppKataCsvViewer
{
    public class ConsoleUserUserCommandReceiver : UserCommandReceiver
    {
        private UserCommandReceiverListener userCommandReceiverListener;

        public ConsoleUserUserCommandReceiver(UserCommandReceiverListener userCommandReceiverListener)
        {
            this.userCommandReceiverListener = userCommandReceiverListener;
        }

        public string ReceiveUserCommand()
        {
            userCommandReceiverListener.NotifyNewCommand();
            return Console.ReadLine();
        }
    }
}