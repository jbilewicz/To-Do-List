# 📝 To-Do List Application

## 📝 Project Overview
Nowoczesna aplikacja desktopowa typu **To-Do List** zaprojektowana dla systemu Windows. Aplikacja oferuje inteligentne filtrowanie zadań, zarządzanie terminami (deadlines) oraz dynamiczny system powiadomień wizualnych o zaległych zadaniach.

## 💻 Tech Stack
<p align="left">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" />
  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=.net&logoColor=white" />
  <img src="https://img.shields.io/badge/WPF-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/XAML-0C549B?style=for-the-badge&logo=xaml&logoColor=white" />
</p>

---

## 🛠️ Core Features
- **Task Management:** Pełny cykl CRUD (Create, Read, Update, Delete) z możliwością edycji nazw zadań bezpośrednio na liście.
- **Filtering System:** Zaawansowany silnik filtrowania łączący wyszukiwanie tekstowe "na żywo" z filtrowaniem według kategorii.
- **Deadline Monitoring:** System dat końcowych z wizualnym alertem (czerwona czcionka) dla zadań zaległych.
- **Visual Feedback:** Obsługa statusu wykonania zadania (CheckBox) z efektem przekreślenia tekstu (Strikethrough).
- **Theme Engine:** Wbudowany system zmiany motywów (Light/Dark Mode) wpływający na całe UI.

---

## 📐 Data Architecture
System opiera się na reaktywnym przepływie danych, zapewniając płynną synchronizację między logiką a interfejsem użytkownika.



| Component | Technology | Responsibility |
| :--- | :--- | :--- |
| **Data Source** | Local File (`tasks.txt`) | Trwałe przechowywanie danych w formacie tekstowym. |
| **Collection** | `ObservableCollection` | Dynamiczne odświeżanie widoku po zmianie danych. |
| **Filtering** | `ICollectionView` | Zarządzanie widocznością elementów bez ich usuwania. |
| **Notifications** | `INotifyPropertyChanged` | Aktualizacja UI w czasie rzeczywistym po zmianie statusu zadania. |

---

## 🚀 Key Algorithms
1. **Hybrid Filtering:** Algorytm filtrujący weryfikuje jednocześnie dwa parametry: czy kategoria zadania odpowiada wybranej w `FilterSelector` oraz czy pole `Name` zawiera frazę wpisaną w `SearchInput`.
2. **Overdue Detection:** Właściwość obliczana `IsOverdue` sprawdza relację między `DateTime.Now` a `Deadline`. Jeśli zadanie nie jest gotowe i termin minął, system zmienia stan wizualny wiersza na kolor czerwony.
3. **Inline Persistence:** Każda zmiana (edycja nazwy, zmiana statusu lub dodanie zadania) automatycznie wyzwala proces zapisu danych do pliku lokalnego.


