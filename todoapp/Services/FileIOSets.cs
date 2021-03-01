using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todoapp.Models;

namespace todoapp.Services
{
    class FileIOSets
    {
        private readonly string PATH;
        
        public FileIOSets(string path)
        {
            PATH = path;
        }
        public BindingList<todoModel> LoadData()
        {
            //проверяем существует ли файл или нет
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<todoModel>();
            }
            using(var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<todoModel>>(fileText);
            }
        }

        public void SaveData(object _todoData)
        {
            using(StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(_todoData);
                writer.Write(output);
            }
            
        }
    }
}
