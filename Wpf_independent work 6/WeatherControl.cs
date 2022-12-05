using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_independent_work_6
{
    enum Precipitation//создаем перечисление параллельно классу, чтобы можно было обращаться из любого класса в данном пространстве имен
    {
        sunny,
        cloudy,
        rain,
        snow
    }

    class WeatherControl : DependencyObject//класс делаем наследником от DependencyObject
    {
        //задаем поля
        private string wind_direction;
        private int wind_speed;
        private Precipitation precipitation;
        //создаем обычные свойства
        public string WindDirection { get; set; }
        public int WindSpeed { get; set; }

        public WeatherControl (string wind_dir, int wind_sp, Precipitation precipitation)//создаем конструктор
        {
            this.WindDirection = wind_dir;
            this.WindSpeed = wind_sp;
            this.precipitation = precipitation;
        }

        public static readonly DependencyProperty TempProperty;//создаем свойство зависимости

        public double Temp//создаем свойство класса
        {
            get => (double)GetValue(TempProperty);//GetValue возвращает тип Object, поэтому преобразовываем в double
            set => SetValue(TempProperty, value);
        }

        static WeatherControl()//создаем статический конструктор для инициализации TempProperty
        {
            //иницализируем через метод Register
            TempProperty = DependencyProperty.Register(
                nameof(Temp),//прописываем имя
                typeof(double),//прописываем тип
                typeof(WeatherControl),//прописываем владельца
                new FrameworkPropertyMetadata(
                    0,//значение по умолчанию
                    FrameworkPropertyMetadataOptions.AffectsMeasure |//флаги
                    FrameworkPropertyMetadataOptions.Journal,
                    null,//метод который будет использоваться при изменении свойства (в данном случае отсутствует)
                    new CoerceValueCallback(CoerceTemp))//экземпляр делегата для вызова метода проверки введенных данных (метод производит корректировку значений)
                );
        }

        private static object CoerceTemp(DependencyObject d, object baseValue)//создаем метод для проверки данных
        {
            double v = (double)baseValue;//приводим значение, которое пытаются ввести (object baseValue) к типу double
            if (v >= -50 && v <= 50)
                return v;
            else
                return 0;
        }
    }
}
