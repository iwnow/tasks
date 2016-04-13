using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardPath
{
    /// <summary>
    /// Класс путевой карточки
    /// </summary>
    public class CardPath
    {
        /// <summary>
        /// Город отправления
        /// </summary>
        public City From { get; private set; }
        
        /// <summary>
        /// Город назначения
        /// </summary>
        public City To { get; private set; }

        /// <summary>
        /// Уникальный ключ пути
        /// </summary>
        public Guid Id { get; private set; }

        public CardPath(City from, City to)
        {
            if (from == null || 
                to == null)
                throw new NullReferenceException("Передан пустой объект в конструктор класса CardPath!");

            this.From = from;
            this.To = to;
            this.Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", this.From.Name, this.To.Name);
        }

        public override int GetHashCode()
        {
            return string.Concat(this.From.Name, this.To.Name).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var o = obj as CardPath;
            if (o == null)
                return false;
            return ((this.From.Name == o.From.Name) && (this.To.Name == o.To.Name));
        }

        /// <summary>
        /// Генерация карточек
        /// </summary>
        /// <returns></returns>
        public static IList<CardPath> GenCards()
        {
            var res = new List<CardPath>();
            //на всякий случай убираем города с одинаковым именем
            var cities = City.GenCities()
                            .GroupBy(c => c.Name)
                            .Select(g => g.First())
                            .ToArray();
            var count = cities.Length;
            for (int i = 0; i < count; i++)
            {
                if ((i + 1) == count)
                    break;
                var from = new City(cities[i].Name);
                var to = new City(cities[i+1].Name);
                res.Add(new CardPath(from, to));
            }
            res.Reverse();
            res.Reverse(3,3);
            return res;
        }

        /// <summary>
        /// Возвращает упорядоченный список карт
        /// </summary>
        /// <param name="cards">Список карт</param>
        /// <returns>Текущая реализация возвращает LinkedList</returns>
        public static LinkedList<CardPath> LinkCard(IList<CardPath> cards)
        {
            if (cards == null)
                throw new ArgumentNullException("cards");
            
            //простой лист
            var res = new List<CardPath>();
            //связанный список
            var resLinked = new LinkedList<CardPath>();

            //карты у которых одинаковые города отправления убираем - из-за н.у.
            cards = cards.GroupBy(c => c.From.Name)
                        .Select(g => g.First())
                        .ToList();
            //индексируем города отправления
            var index = cards.ToDictionary(c => c.From.Name);
            //находим первую карту - город отправления не должен являться ни в одной карте городом назначения
            var search = cards.Where(c => cards.FirstOrDefault(cc => cc.To.Name == c.From.Name) == null).ToArray();
            if (search.Length == 0 || search.Length > 1)
                throw new ApplicationException("Невозможно однозначно определить начало пути");
            var head = search[0];
            res.Add(head);
            resLinked.AddFirst(head);
            var curr = head;
            while (true)
            {
                if (!index.ContainsKey(curr.To.Name))
                    break;
                var nextCard = index[curr.To.Name];
                res.Add(nextCard);
                resLinked.AddLast(nextCard);
                curr = nextCard;
            }
            //return res;
            return resLinked;
        } 

    }
}
