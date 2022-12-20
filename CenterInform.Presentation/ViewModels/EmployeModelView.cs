using CenterInfor.Domain.Models;
using CenterInform.Application.Interfaces;
using System.Runtime.CompilerServices;
using MvvmHelpers.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Controls;
using CenterInform.Serializator;
using CenterInform.Presentation.Models;
using Microsoft.Win32;
using System.Collections.Generic;

namespace CenterInform.Presentation.ViewModels
{
    public class EmployeModelView : INotifyPropertyChanged
    {
        public ObservableCollection<Employe> Employes { get; private set; }
        public ObservableCollection<ComboBoxModel> ComboBox { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand SaveAllCommand { get; set; }

        private readonly IRepository _repository;
        private readonly SerializationManager _serializationManager;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] String propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string _searchField;
        public string SearchField
        {
            get
            {
                return _searchField;
            }
            set
            {
                _searchField = value;
                OnPropertyChanged();
            }
        }

        private Employe _selectedEmploye;
        public Employe SelectedEmploye
        {
            get
            {
                return _selectedEmploye;
            }
            set
            {
                _selectedEmploye = value;
                OnPropertyChanged("IsEnabledSaveAndDelete");
                OnPropertyChanged("Current");
                OnPropertyChanged();
            }
        }

        private ComboBoxModel _selectedComboBoxItem;
        public ComboBoxModel SelectedComboBoxItem
        {
            get
            {
                return _selectedComboBoxItem;
            }
            set
            {
                _selectedComboBoxItem = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// текстовый лабел показывающий выделенный обьект в таблице
        /// </summary>
        public string Current
        {
            get
            {
                return _selectedEmploye == null ? "null" : $"{_selectedEmploye.FirstName}:{_selectedEmploye.SecondName}";
            }
        }

        public bool IsEnabledSaveAndDelete
        {
            get
            {
                return _selectedEmploye != null;
            }
        }

        private bool _isEnablenImportButton;
        public bool IsEnabledImportButton
        {
            get
            {
                return _isEnablenImportButton;
            }
            set
            {
                _isEnablenImportButton = value;
                OnPropertyChanged();
            }
        }

        public EmployeModelView(IRepository repository, SerializationManager manager)
        {
            _repository = repository;
            _serializationManager = manager;
            Employes = new ObservableCollection<Employe>();
            ComboBox = new ObservableCollection<ComboBoxModel>()
            {
                new ComboBoxModel
                {
                    Id = 1,
                    Value = "XML"
                },
                new ComboBoxModel
                {
                    Id = 2,
                    Value = "JSON"
                }
            };
            _selectedComboBoxItem = ComboBox[1];
            IsEnabledImportButton = true;

            SaveCommand = new AsyncCommand(Save);
            RefreshCommand = new Command(()=> ShowTable());
            SearchCommand = new Command(Search);
            DeleteCommand = new Command(Delete);
            ImportCommand = new Command(Serialize);
            ExportCommand = new Command(OpenFileDialogAndReadFile);
            SaveAllCommand = new Command(SaveAll);
            ShowTable();
        }

        /// <summary>
        /// добавляем данные из бд
        /// </summary>
        public void ShowTable(List<Employe> employes = null)
        {
            Employes.Clear();
            if(employes == null)
            {
                var list = _repository.Get();
                foreach (var emp in list)
                {
                    Employes.Add(emp);
                }
            }
            else
            {
                foreach (var emp in employes)
                {
                    Employes.Add(emp);
                }
            }
            
        }
        
        /// <summary>
        /// я сделал простой поиск на совпадение по имени
        /// если нужно можно это усложнять и улучшать
        /// </summary>
        private void Search()
        {
            if (String.IsNullOrEmpty(SearchField))
            {
                ShowTable();
                return;
            }
            var result = _repository.Get().Where(x => x.FirstName.ToLower().Contains(SearchField.ToLower())).ToList();
            
            if (result != null && result.Count > 0)
            {
                Employes.Clear();
                foreach (var item in result)
                {
                    Employes.Add(item);
                }
            }
        }

        /// <summary>
        /// Сохраняем по одной сущности
        /// </summary>
        /// <returns></returns>
        private async Task Save()
        {
            if (_selectedEmploye == null)
            {
                return;
            }
            await _repository.Update(_selectedEmploye);
            await _repository.CommitChanges();
            ShowTable();
        }

        /// <summary>
        /// перезаписываем таблицу основываясь на листе
        /// </summary>
        private async void SaveAll()
        {
            if (Employes == null || Employes.Count == 0)
                return;
            //удаляем расхождения из таблицы
            await _repository.ExcludeEntityFromDbTable(Employes);
            //оставшиеся записи считаем истинными
            //по идее сюда надо вынести логику из репозитория
            //но в таком случае сюда надо передавать контекст 
            //чтобы менять стейт, ибо иначе из-за области 
            //происходят проблемы с тракингом сущностей
            foreach (var item in Employes)
            {
                await _repository.Update(item);
            }

            await _repository.CommitChanges();
            ShowTable();
        }

        /// <summary>
        /// тоже самое что и с сохранением, удаляем персонально по одному
        /// </summary>
        private async void Delete()
        {
            if (_selectedEmploye == null)
                return;
            var result = await _repository.FindById(_selectedEmploye.Id);
            if (result == null)
            {
                return;
            }
            _repository.Remove(result);
            await _repository.CommitChanges();
            ShowTable();
        }
        
        private async void Serialize()
        {
            IsEnabledImportButton = false;
            switch (SelectedComboBoxItem.Value)
            {
                case "XML":
                    await _serializationManager.ImportToXml(Employes);
                    break;
                case "JSON":
                    await _serializationManager.ImportToJson(Employes);
                    break;
                default:
                    break;
            }
            IsEnabledImportButton = true;
        }

        /// <summary>
        /// не уверен что это правильно для MVVM 
        /// но вроде как работает
        /// </summary>
        private async void OpenFileDialogAndReadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                List<Employe> result = null;
                switch (openFileDialog.FileName.Split(".").Last())
                {
                    case "xml":
                        result = await _serializationManager.ExportFromXml<List<Employe>>(openFileDialog.FileName);
                        break;
                    case "json":
                        result = await _serializationManager.ExportFromJson<List<Employe>>(openFileDialog.FileName);
                        break;
                    default:
                        break;
                }
                ShowTable(result);
            }
        }
    }
}
