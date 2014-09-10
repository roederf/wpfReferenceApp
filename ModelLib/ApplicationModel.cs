using BusinessLogicInterface;
using ModelInterfaces;
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
    public class ApplicationModel : IApplicationModel
    {
        public bool Login()
        {
            return true;
        }

        public bool Logout()
        {
            return true;
        }

        public bool OpenFile()
        {
            CurrentFile = new FileItem();
            CurrentFile.Name = "Example File";
            CurrentFile.Properties.Add(new PropertyItem() { Name = "Property one", Value = 0 });
            CurrentFile.Properties.Add(new PropertyItem() { Name = "Property two", Value = 0 });
            CurrentFile.Properties.Add(new PropertyItem() { Name = "Property three", Value = 0 });
            CurrentFile.Properties.Add(new PropertyItem() { Name = "Property four", Value = 0 });

            return true;
        }

        public bool CloseFile()
        {
            return true;
        }

        public void CalculateValue(string propertyName)
        {
            if (CurrentFile != null)
            {
                var pItem = CurrentFile.Properties.FirstOrDefault(p => p.Name == propertyName);
                
                Thread.Sleep(5000);

                Random rand = new Random();

                pItem.Value = rand.Next(1000);
            }
        }

        public IFileItem CurrentFile { get; set; }

        public bool UnsavedChanges { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
