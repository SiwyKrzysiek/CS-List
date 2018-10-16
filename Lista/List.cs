using System;
using System.Text;

namespace Lista
{
    /// <summary>
    /// Klasa listy jednokierunkowej.
    /// Zawiera podstawowe operacje na listach
    /// </summary>
    public class List<T>
    {
        private Node<T> head;
        private Node<T> tale;

        public int Lenght { get; private set; }

        public List()
        {
            //Pola sa automatycznie inicjalizowane poprawnymi wartosciami
        }

        public void PushBack(T element)
        {
            Lenght++;

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

        public void PushFront(T element)
        {
            Lenght++;

            Node<T> newNode = new Node<T>(element);
            if (head == null) //Lista pusta
            {
                head = newNode;
                tale = newNode;
            }
            else
            {
                newNode.Next = head;
                head = newNode;
            }
        }

        public void PopFront()
        {
            head = head.Next;
            Lenght--;
        }

        public void PopBack()
        {
            if (Lenght <= 0)
                throw new InvalidOperationException("Can't pop from empty list");
            if (Lenght == 1)
            {
                head = null;
                tale = null;
                Lenght--;

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
            Lenght--;
        }

        public void RemoveAtIndex(int index)
        {
			if (index >= Lenght)
					throw new IndexOutOfRangeException("Element with given index does not exist");
            if (index == Lenght-1)
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
                    Lenght--;
                    return;
                }

                previous = i;
                currentIndex++;
			}
        }

        public void InsertAfterIndex(int index, T value)
        {
			if (index >= Lenght)
				throw new IndexOutOfRangeException("Element with given index does not exist");

            Node<T> temp = head;
            for (int i = 0; i < index; i++)
            {
                temp = temp.Next;
            }

            Node<T> newNode = new Node<T>(value);
            newNode.Next = temp.Next;
            temp.Next = newNode;

            Lenght++;
        }

        public T FindAtIndex(int index)
        {
            if (index >= Lenght)
				throw new IndexOutOfRangeException("Element with given index does not exist");

			int currentIndex = 0;
            for (Node<T> i = head; i != null; i = i.Next)
            {
                if (index == currentIndex++)
                    return i.Value;
            }

            throw new IndexOutOfRangeException("Element with given index does not exist");
        }

        public T RecurentFindAtIndex(int index)
        {
            return RecurentHidenFindAtIndex(index, head);
        }

        private T RecurentHidenFindAtIndex(int index, Node<T> myHead)
        {
            if (index > 0)
                return RecurentHidenFindAtIndex(index - 1, myHead.Next);

            return myHead.Value;
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
