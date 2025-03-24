using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

//Performed by Slipushkina Oleksandra

namespace Zodiac
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DateTime _birthDate = DateTime.Today;
        private string _ageText;
        private string _westernZodiac;
        private string _chineseZodiac;

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (_birthDate != value)
                {
                    _birthDate = value;
                    OnPropertyChanged();
                    CalculateAgeAndZodiac();
                }
            }
        }

        public string AgeText
        {
            get => _ageText;
            private set { _ageText = value; OnPropertyChanged(); }
        }

        public string WesternZodiac
        {
            get => _westernZodiac;
            private set { _westernZodiac = value; OnPropertyChanged(); }
        }

        public string ChineseZodiac
        {
            get => _chineseZodiac;
            private set { _chineseZodiac = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CalculateAgeAndZodiac()
        {
            int age = DateTime.Today.Year - BirthDate.Year;
            if (BirthDate.Date > DateTime.Today.AddYears(-age)) age--;

            if (age < 0 || age > 135)
            {
                MessageBox.Show("Некоректний вік!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                AgeText = "Невірна дата";
                WesternZodiac = "-";
                ChineseZodiac = "-";
                return;
            }

            AgeText = $"Вік: {age}";
            WesternZodiac = GetWesternZodiac(BirthDate);
            ChineseZodiac = GetChineseZodiac(BirthDate);

            if (BirthDate.Day == DateTime.Today.Day && BirthDate.Month == DateTime.Today.Month)
            {
                MessageBox.Show("З Днем народження!", "Святкуємо", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private string GetWesternZodiac(DateTime date)
        {
            int day = date.Day, month = date.Month;
            switch (month)
            {
                case 1: return day < 20 ? "Козеріг" : "Водолій";
                case 2: return day < 19 ? "Водолій" : "Риби";
                case 3: return day < 21 ? "Риби" : "Овен";
                case 4: return day < 20 ? "Овен" : "Телець";
                case 5: return day < 21 ? "Телець" : "Близнюки";
                case 6: return day < 21 ? "Близнюки" : "Рак";
                case 7: return day < 23 ? "Рак" : "Лев";
                case 8: return day < 23 ? "Лев" : "Діва";
                case 9: return day < 23 ? "Діва" : "Терези";
                case 10: return day < 23 ? "Терези" : "Скорпіон";
                case 11: return day < 22 ? "Скорпіон" : "Стрілець";
                case 12: return day < 22 ? "Стрілець" : "Козеріг";
                default: return "Невідомо";
            }
        }

        private string GetChineseZodiac(DateTime date)
        {
            string[] signs = { "Мавпа", "Півень", "Собака", "Свиня", "Щур", "Бик", "Тигр", "Кролик", "Дракон", "Змія", "Кінь", "Коза" };
            return signs[date.Year % 12];
        }
    }
}