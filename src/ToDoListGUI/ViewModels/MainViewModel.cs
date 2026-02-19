using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ToDoListGUI.Models;

namespace ToDoListGUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ToDoItem> Tasks { get; } = new();
        public ICollectionView TasksView { get; }

        private readonly string _filePath = "tasks.txt";

        private string _newTaskName = string.Empty;
        public string NewTaskName
        {
            get => _newTaskName;
            set { _newTaskName = value; OnPropertyChanged(nameof(NewTaskName)); }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory)); }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); ApplyFilters(); }
        }

        private object? _filterCategory;
        public object? FilterCategory
        {
            get => _filterCategory;
            set { _filterCategory = value; OnPropertyChanged(nameof(FilterCategory)); ApplyFilters(); }
        }

        private ToDoItem? _selectedTask;
        public ToDoItem? SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(nameof(SelectedTask)); }
        }

        public IEnumerable<Category> Categories { get; } = (Category[])Enum.GetValues(typeof(Category));
        public List<object> FilterOptions { get; } = BuildFilterOptions();

        public ICommand AddTaskCommand    { get; }
        public ICommand RemoveTaskCommand { get; }

        public MainViewModel()
        {
            TasksView = CollectionViewSource.GetDefaultView(Tasks);
            TasksView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(ToDoItem.Category)));

            AddTaskCommand = new RelayCommand(
                execute:    AddTask,
                canExecute: () => !string.IsNullOrWhiteSpace(NewTaskName));

            RemoveTaskCommand = new RelayCommand(
                execute:    RemoveTask,
                canExecute: () => SelectedTask != null);

            LoadData();

            _filterCategory = FilterOptions[0];
        }

        private void AddTask()
        {
            Tasks.Add(new ToDoItem(NewTaskName.Trim(), SelectedCategory));
            SaveData();
            NewTaskName = string.Empty;
        }

        private void RemoveTask()
        {
            if (SelectedTask == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to remove: {SelectedTask.Name}?",
                "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Tasks.Remove(SelectedTask);
                SaveData();
            }
        }

        public void SaveData()
        {
            var lines = Tasks.Select(t => $"{t.Name}|{t.Category}|{t.IsDone}");
            File.WriteAllLines(_filePath, lines);
        }

        private void LoadData()
        {
            if (!File.Exists(_filePath)) return;

            foreach (var line in File.ReadAllLines(_filePath))
            {
                var parts = line.Split('|');
                if (parts.Length >= 2 && Enum.TryParse(parts[1], out Category cat))
                {
                    bool done = parts.Length == 3 && bool.TryParse(parts[2], out var d) && d;
                    Tasks.Add(new ToDoItem(parts[0], cat, done));
                }
            }
        }

        private void ApplyFilters()
        {
            TasksView.Filter = obj =>
            {
                if (obj is not ToDoItem item) return false;

                bool categoryMatch = FilterCategory == null
                    || FilterCategory.ToString() == "All"
                    || item.Category.ToString() == FilterCategory.ToString();

                bool searchMatch = string.IsNullOrWhiteSpace(SearchText)
                    || item.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);

                return categoryMatch && searchMatch;
            };
        }

        private static List<object> BuildFilterOptions()
        {
            var list = new List<object> { "All" };
            foreach (var cat in Enum.GetValues(typeof(Category))) list.Add(cat);
            return list;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
