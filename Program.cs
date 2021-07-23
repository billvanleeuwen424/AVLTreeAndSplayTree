using System;

namespace Assignment5
{
    class Program
    {
        static void Main(string[] args)
        {

            SplayTree<int> splayInt = new SplayTree<int>();
            Node<int> root = null;

            root = splayInt.Insert(root, 10);
            root = splayInt.Insert(root, 7);
            root = splayInt.Insert(root, 20);
            root = splayInt.Insert(root, 1);
            root = splayInt.Insert(root, 9);
            root = splayInt.Insert(root, 30);

            Random rand = new Random(5);
            for(int i = 0; i < 25; i++)
            {
                root = splayInt.Insert(root, rand.Next(50));
            }
            Node<int> seven = splayInt.Search(root, 7);
            //before splaying seven
            splayInt.printLevelOrder(root);
            root = splayInt.Splay(seven);
            //after splaying seven
            splayInt.printLevelOrder(root);

            Node<int> thirty = splayInt.Search(root, 30);
            root = splayInt.Splay(thirty);  
            splayInt.printLevelOrder(root);

            Node<int> ten = splayInt.Search(root, 10);
            root = splayInt.Splay(ten);
            splayInt.printLevelOrder(root);

            Node<int> fortyeight = splayInt.Search(root, 48);
            root = splayInt.Splay(fortyeight);
            splayInt.printLevelOrder(root);

            splayInt.ValidateWholeTree(root);

            //splayInt.printLevelOrder(root);
            //AVLTree<int> avlTree = new AVLTree<int>();
            //Node<int> avlroot = null;

            //avlroot = avlTree.Insert(avlroot, 20);
            //avlroot = avlTree.Insert(avlroot, 15);
            //avlroot = avlTree.Insert(avlroot, 30);
            //avlroot = avlTree.Insert(avlroot, 25);
            //avlroot = avlTree.Insert(avlroot, 40);
            //avlroot = avlTree.Insert(avlroot, 17);
            //avlroot = avlTree.Insert(avlroot, 10);
            //avlroot = avlTree.Insert(avlroot, 12);
            //avlroot = avlTree.Insert(avlroot, 16);
            //avlroot = avlTree.Insert(avlroot, 50);
            //avlroot = avlTree.Insert(avlroot, 20);
            //avlroot = avlTree.Insert(avlroot, 19);
            //avlroot = avlTree.Insert(avlroot, 19);
            //avlTree.printLevelOrder(avlroot);

            //Node<int> fifteen = avlTree.Search(avlroot,15);

            //avlTree.Delete(fifteen);

            //avlTree.printLevelOrder(avlroot);

            //string[] birdNames = new string[10] { "Tweety", "Zazu", "Iago", "Hula", "Manu", "Couscous", "Roo", "Tookie", "Plucky", "Jay" };
            //int numbirds = 0;

            ////create an animal tree
            //AVLTree<Animal> AnimalTree = new AVLTree<Animal>();
            //Node<Animal> root = null;

            ////fill the tree with 15 birds
            //for(int i = 0; i < 15; i++)
            //{
            //    Bird tempBird = RandBird(birdNames, ref numbirds);
            //    root = AnimalTree.Insert(root, tempBird);
            //}

            ////print breadth first
            //AnimalTree.breadthFirst(root);

            //validate tree
            //Console.WriteLine("\n\n Validate Tree.");
            //bool treeValid = AnimalTree.ValidateWholeTree(root);
            //Console.WriteLine("\nIs this a valid BST?:" + treeValid);
        }



        /// <summary>
        /// generates a random bird. Just needs the name array passed from main. and the number of birds for the ID
        /// </summary>
        /// <returns></returns>
        static Bird RandBird(string[] birdNames, ref int numbirds)
        {
            Random rnd = new Random();

            Bird randomBird = new Bird(birdNames[rnd.Next(10)], rnd.Next(100), numbirds, true);

            numbirds++;

            return randomBird;
        }
    }
}
