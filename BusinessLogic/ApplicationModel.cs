using ModelLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ApplicationModel : INotifyPropertyChanged
    {
        public void Init()
        {
            State = ApplicationState.Login;
        }

        public void Login()
        {
            State = ApplicationState.Home;

        }

        public void Logout()
        {
            State = ApplicationState.Login;
        }

        public void OpenFile()
        {
            CurrentFile = new FileItem();
            CurrentFile.Name = "Example File";
            CurrentFile.Properties.Add(new PropertyItem() { Name = "Property one", Value = 0 });
            CurrentFile.Properties.Add(new PropertyItem() { Name = "Property two", Value = 0 });
            CurrentFile.Properties.Add(new PropertyItem() { Name = "Property three", Value = 0 });
            CurrentFile.Properties.Add(new PropertyItem() { Name = "Property four", Value = 0 });

            State = ApplicationState.Edit;
        }

        public void CloseFile()
        {
            State = ApplicationState.Home;
        }

        public void CalculateValue(string propertyName)
        {
            if (CurrentFile != null)
            {
                var pItem = CurrentFile.Properties.FirstOrDefault(p => p.Name == propertyName);

                State = ApplicationState.Edit_Progress;

                Thread.Sleep(5000);

                Random rand = new Random();

                pItem.Value = rand.Next(1000);

                State = ApplicationState.Edit;
            }
        }

        public FileItem CurrentFile { get; set; }

        public bool UnsavedChanges { get; set; }

        #region ApplicationState State
        private ApplicationState _state;
        public ApplicationState State
        {
            get { return _state; }
            set
            {
                if (value != _state)
                {
                    _state = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("State"));
                    }
                }
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
