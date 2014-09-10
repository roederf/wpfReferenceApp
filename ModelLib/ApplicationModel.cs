using BusinessLogicInterface;
using ModelInterfaces;
using ModelLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        public bool SaveFile()
        {
            foreach (var item in CurrentFile.Properties)
            {
                item.HasChanged = false;
            }

            using (FileStream fs = new FileStream("data.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, CurrentFile);
                fs.Close();
            }
            return true;
        }

        public bool OpenFile()
        {
            //CurrentFile = new FileItem();
            //CurrentFile.Name = "Example File";
            //CurrentFile.Properties.Add(new PropertyItem() { Name = "Property one", Value = 0 });
            //CurrentFile.Properties.Add(new PropertyItem() { Name = "Property two", Value = 0 });
            //CurrentFile.Properties.Add(new PropertyItem() { Name = "Property three", Value = 0 });
            //CurrentFile.Properties.Add(new PropertyItem() { Name = "Property four", Value = 0 });

            using (FileStream fs = new FileStream("data.dat", FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                CurrentFile = bf.Deserialize(fs) as FileItem;
                fs.Close();
            }

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
                
                Thread.Sleep(2000);

                Random rand = new Random();

                pItem.Value = rand.Next(1000);
                pItem.HasChanged = true;
            }
        }

        public IFileItem CurrentFile { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
