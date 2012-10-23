using System;

namespace AppKataCsvViewer
{
    public class CsvViewer
    {
        private readonly Display display;
        private readonly UserCommandReceiver userCommandReceiver;

        public CsvViewer(UserCommandReceiver userCommandReceiver, Display display)
        {
            this.userCommandReceiver = userCommandReceiver;
            this.display = display;
        }

        public void Show(Browsable browsable)
        {
            display.Show(browsable.NextPage());
            display.PrintUserOptionsFor(browsable.PageCount);
            ExecuteCommandEnteredByUser(browsable);
        }

        private void ExecuteCommandEnteredByUser(Browsable browsable)
        {
            string command;

            while ( ! string.IsNullOrEmpty(command = userCommandReceiver.ReceiveUserCommand()))
            {
                if (command.ToLower() == "x" || command.ToLower() == "exit")
                {
                    break;
                }

                if (command.ToLower() == "n" || command.ToLower() == "next")
                {
                    display.Show(browsable.NextPage());
                    display.PrintUserOptionsFor(browsable.PageCount);
                }

                if (command.ToLower() == "p" || command.ToLower() == "previous")
                {
                    display.Show(browsable.PreviousPage());
                    display.PrintUserOptionsFor(browsable.PageCount);
                }

                if (command.ToLower() == "l" || command.ToLower() == "last")
                {
                    display.Show(browsable.LastPage());
                    display.PrintUserOptionsFor(browsable.PageCount);
                }

                // TODO: Execute entered command.
            }

            if (command == null || command.ToLower() != "x" && command.ToLower() != "exit")
            {
                throw new Exception("No or wrong command was entered by user: " + command);
            }
        }
    }
}