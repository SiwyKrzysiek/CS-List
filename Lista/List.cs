using System;
using System.Text;

namespace Lista
{
    public class List<T>
    {
        private Node<T> head;
        private Node<T> tale;
        private int size;

        public int Lenght
        {
            get => size;
        }

        public List()
        {
            //Pola sa automatycznie inicjalizowane poprawnymi wartosciami
        }

        public void PushBack(T element)
        {
            size++;

            Node<T> newNode = new Node<T>(element);
            if (head == null) //Lista pusta
            {
                head = newNode;
                tale = newNode;
            }
            else
            {
                tale.Next = newNode;
                tale = newNode;
            }
        }

        public void PopFront()
        {
            head = head.Next;
            size--;
        }

        public void PopBack()
        {
            if (size <= 0)
                throw new InvalidOperationException("Can't pop from empty list");
            if (size == 1)
            {
                head = null;
                tale = null;
                size--;

                return;
            }
            
            Node<T> previous=head, i = head;

            while(i.Next != null)
            {
                previous = i;
                i = i.Next;
            }

            previous.Next = null;
            tale = previous;
            size--;
        }

        public void RemoveAtIndex(int index)
        {
			if (index >= size)
					throw new IndexOutOfRangeException("Element with given index does not exist");
            if (index == size-1)
            {
                PopBack();
                return;
            }
            if (index == 0)
            {
                PopFront();
                return;
            }

            int currentIndex = 0;
            Node<T> previous = head;
			for (Node<T> i = head; i != null; i = i.Next)
			{
                if (currentIndex == index)
                {
                    previous.Next = i.Next;
                    size--;
                    return;
                }

                previous = i;
                currentIndex++;
			}
        }

        public T FindAtIndex(int index)
        {
            if (index >= size)
				throw new IndexOutOfRangeException("Element with given index does not exist");

			int currentIndex = 0;
            for (Node<T> i = head; i != null; i = i.Next)
            {
                if (index == currentIndex++)
                    return i.Value;
            }

            throw new IndexOutOfRangeException("Element with given index does not exist");
        }

        public T FindElement(T element, Func<T, T, bool> howToCompare)
        {
			for (Node<T> i = head; i != null; i = i.Next)
			{
                if (howToCompare(i.Value, element))
                    return i.Value;
			}

            throw new Exception("No element found");
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (Node<T> i = head; i != null; i = i.Next)
            {
                stringBuilder.Append($"{i.Value.ToString()}->");
            }

            stringBuilder.Remove(stringBuilder.Length-2, 2);

            return stringBuilder.ToString();
        }
    }
}
