using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardPath
{
    /// <summary>
    /// Абстракция - город
    /// </summary>
    public class City {
        
        /// <summary>
        /// Имя города
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Уникальный ключ объекта, однозначно определяющий город
        /// (в реальной ситуации уникальбные ключи скорее всего обеспечены на уровне БД)
        /// для менее связного кода лучшим решением было бы определить свобю абстракцию ключа
        /// Guid - примелемый вариант в рамках текущей задачи
        /// </summary>
        public Guid Id { get; private set; }

        public City(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ApplicationException("Имя города не должно быть пустым!", new ArgumentException("", "name"));
            this.Name = name;
            this.Id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            var o = obj as City;
            if (o == null)
                return false;
            return this.Name == o.Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// Генерация списка городов
        /// </summary>
        /// <returns></returns>
        public static IList<City> GenCities()
        {
            return new List<City>
            {
                new City("Москва"),
                new City("Питер"),
                new City("Пермь"),
                new City("Пекин"),
                new City("Сингапур"),
                new City("Коломбо"),
                new City("Кельн"),
                new City("Париж"),
                new City("Мадрид"),
                new City("Лос-Анджелес"),
                new City("Сан-Франциско"),
                new City("Вашингтон")
            };
        }
    }
}
