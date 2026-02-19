using System.ComponentModel;

namespace ToDoListGUI.Models
{
    public class ToDoItem : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private bool _isDone;
        private bool _isEditing;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public Category Category { get; set; }

        public bool IsDone
        {
            get => _isDone;
            set { _isDone = value; OnPropertyChanged(nameof(IsDone)); }
        }

        public bool IsEditing
        {
            get => _isEditing;
            set { _isEditing = value; OnPropertyChanged(nameof(IsEditing)); }
        }

        public ToDoItem(string name, Category category, bool isDone = false)
        {
            Name = name;
            Category = category;
            IsDone = isDone;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
