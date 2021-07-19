using System;

namespace Assignment5
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> avlTree = new AVLTree<int>();
            Node<int> avlroot = null;

            for(int i = 0; i < 100; i++)
            {
                avlroot = avlTree.Insert(avlroot, i);
            }
            avlTree.printLevelOrder(avlroot);
            //avlroot = avlTree.Insert(avlroot, 20);
            //avlroot = avlTree.Insert(avlroot, 30);
            //Console.WriteLine("Before Right Right");
            //avlTree.printLevelOrder(avlroot);
            //avlroot = avlTree.Insert(avlroot, 40);
            //Console.WriteLine("\n\n After Right Right, and before Right Left \n ");
            //avlTree.printLevelOrder(avlroot);
            //avlroot = avlTree.Insert(avlroot, 50);
            //avlroot = avlTree.Insert(avlroot, 45);
            //Console.WriteLine("\n\n After Right Left ");
            //avlTree.printLevelOrder(avlroot);
            //Console.WriteLine("\n\n After Left Left, and before Left Right \n ");
            ////print post insert, proving the tree moved
            //avlTree.printLevelOrder(avlroot);
            //Console.WriteLine("\n \n After Left Right");
            //avlroot = avlTree.Insert(avlroot, 8);
            //avlTree.printLevelOrder(avlroot);

        }
    }
}
