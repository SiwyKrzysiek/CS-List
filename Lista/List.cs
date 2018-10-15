﻿using System;
using System.Text;

namespace Lista
{
    public class List<T>
    {
        private Node<T> head;
        private Node<T> tale;
        private int size;

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
