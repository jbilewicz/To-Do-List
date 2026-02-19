# 📝 To-Do List Application

## 📝 Project Overview
A modern desktop **To-Do List** application designed for Windows. The application offers intelligent task filtering, deadline management, and a dynamic visual alert system for overdue tasks.

## 💻 Tech Stack
<p align="left">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" />
  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=.net&logoColor=white" />
  <img src="https://img.shields.io/badge/WPF-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/XAML-0C549B?style=for-the-badge&logo=xaml&logoColor=white" />
</p>

---

## 🛠️ Core Features
- **Task Management:** Full CRUD (Create, Read, Update, Delete) cycle with the ability to edit task names directly within the list (Inline Editing).
- **Filtering System:** Advanced filtering engine combining real-time text search with category-based filtering.
- **Deadline Monitoring:** Due date system with visual alerts (red font) for overdue tasks.
- **Visual Feedback:** Task completion status support (CheckBox) with a text strikethrough effect.
- **Theme Engine:** Built-in theme switching system (Light/Dark Mode) affecting the entire UI.

---

## 📐 Data Architecture
The system is based on a reactive data flow, ensuring seamless synchronization between the logic and the user interface.



| Component | Technology | Responsibility |
| :--- | :--- | :--- |
| **Data Source** | Local File (`tasks.txt`) | Persistent data storage in text format. |
| **Collection** | `ObservableCollection` | Dynamic view updates upon data changes. |
| **Filtering** | `ICollectionView` | Managing element visibility without removing them from the collection. |
| **Notifications** | `INotifyPropertyChanged` | Real-time UI updates upon task status changes. |

---

## 🚀 Key Algorithms
1. **Hybrid Filtering:** The filtering algorithm simultaneously verifies two parameters: whether the task category matches the one selected in `FilterSelector` and if the `Name` field contains the string entered in `SearchInput`.
2. **Overdue Detection:** The calculated `IsOverdue` property monitors the relationship between `DateTime.Now` and the `Deadline`. If a task is not completed and the deadline has passed, the system changes the row's visual state to red.
3. **Inline Persistence:** Every change (name edit, status toggle, or task addition) automatically triggers a data synchronization process to the local file.

