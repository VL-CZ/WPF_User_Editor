using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_MVVM_2_.Models;

namespace WPF_MVVM_2_.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Event Handler

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion

        #region Properties

        // vlastnosti
        public ObservableCollection<User> Users { get; set; }
        public User BestUser { get; set; }
        public User SelectedUser { get; set; }

        public bool EnabledName { get; set; }
        public bool EnabledAge { get; set; }
        public bool EnabledScore { get; set; }

        /// <summary>
        /// Command, reaguje na stisknutí tlačítka
        /// </summary>
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// konstuktor
        /// </summary>
        public MainWindowViewModel()
        {
            Users = new ObservableCollection<User>()
            {
                new User("Karel Novák", 18, 100),
                new User("Jan Nový", 25, 250)
            };
            FindBest();

            // při stisknutí zavolá UpdateUsers()
            SaveCommand = new RelayCommand(o => UpdateUsers());
            DeleteCommand = new RelayCommand(user => RemoveUser(user));
            AddCommand = new RelayCommand(user => AddUser(user));
            EditCommand = new RelayCommand(o => EditUser());

            EnabledAge = false;
            EnabledName = false;
            EnabledScore = false;
        }

        #endregion

        #region Help Methods

        /// <summary>
        /// najde nejlepšího uživatele
        /// </summary>
        public void FindBest()
        {
            BestUser = Users.OrderByDescending(x => x.Score).FirstOrDefault();
            OnPropertyChanged("BestUser");
        }

        /// <summary>
        /// nastaví vybraného uživatele na user
        /// </summary>
        /// <param name="user"></param>
        public void SetSelectedUser(User user)
        {
            SelectedUser = user;
            OnPropertyChanged("SelectedUser");
        }

        public void SetVisibilityName(bool val)
        {
            EnabledName = val;
            OnPropertyChanged("EnabledName");
        }

        public void SetVisibilityAge(bool val)
        {
            EnabledAge = val;
            OnPropertyChanged("EnabledAge");
        }

        public void SetVisibilityScore(bool val)
        {
            EnabledScore = val;
            OnPropertyChanged("EnabledScore");
        }

        #endregion

        #region Users Methods

        /// <summary>
        /// zavolá FindBest() při stisknutí tlačítka
        /// </summary>
        private void UpdateUsers()
        {
            FindBest();
            SetVisibilityAge(false);
            SetVisibilityName(false);
            SetVisibilityScore(false);
        }

        public void AddUser(object o)
        {
            User user = new User();
            Users.Add(user);
            SetSelectedUser(user);

            FindBest();
            SetVisibilityAge(true);
            SetVisibilityName(true);
            SetVisibilityScore(true);
        }

        public void RemoveUser(object user)
        {
            Users.Remove(user as User);
            FindBest();
        }

        public void EditUser()
        {
            SetVisibilityAge(true);
            SetVisibilityName(true);
            SetVisibilityScore(true);
        }

        #endregion

    }
}