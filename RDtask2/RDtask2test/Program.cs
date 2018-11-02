using System;

namespace RDtask2LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqTask.Task1();

            Console.ReadKey();
        }

        /*private static void Add(object sender, BinaryTreeItemAddedEventArgs<int> e)
        {
            var bitree = sender as BinaryTree<int>;
            Console.WriteLine(bitree.Count);
            Console.WriteLine(e.Time);
        }*/
    }
}
