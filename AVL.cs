using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    public class Node<T>
    {
        public T value;
        public Node<T> left;
        public Node<T> right;
        public Node<T> parent;
        public int balance=0;
    }

    public class AVLTree<T> where T : IComparable<T>
    {
        /// <summary>
        /// inserts a node recursively into the proper position,
        /// sets parent, value, left, and right
        /// </summary>
        /// <param name="root"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public Node<T> insert(Node<T> root, T v)
        {
            if (root == null)
            {
                root = new Node<T>();
                root.value = v;
            }

           
            // insertion logic, if the value (v )is < root, insert to the root.left
            // otherwise it's >=, so insert to the right
            else if (v.CompareTo(root.value) < 0)  //v < root.value
            {
                
                root.left = insert(root.left, v);
                root.left.parent = root;
            }
            else
            {

                root.right = insert(root.right, v);
                root.right.parent = root;
            }

            return root;
        }
        // Lab:  Take the code from here, and implement 3 different traversals  as strings
        // public string traverse (Node root)

        public void traverse(Node<T> root)
        {
            if (root == null)
            {
                return;
            }
            Console.WriteLine(root.value.ToString());
            traverse(root.left);
            traverse(root.right);


        }
        

        public string inOrder(Node<T> root)
        {
            if (root == null)
            {
                return "";
            }

            string returnString = "";

            returnString += inOrder(root.left);

            returnString += (root.value.ToString() + ", ");

            returnString += inOrder(root.right);

            return returnString;
        }

        public string preOrder(Node<T> root)
        {
            if (root == null)
            {
                return "";
            }

            string returnString = "";

            returnString += (root.value.ToString() + ", ");

            returnString += preOrder(root.left);

            returnString += preOrder(root.right);

            return returnString;
        }

        public string postOrder(Node<T> root)
        {
            if (root == null)
            {
                return "";
            }

            string returnString = "";

            returnString += postOrder(root.left);

            returnString += postOrder(root.right);

            returnString += (root.value.ToString() + ", ");


            return returnString;
        }

        public void breadthFirst(Node<T> root)
        {
            //build queue
            Queue<Node<T>> queue = new Queue<Node<T>>();

            //put the root into it
            queue.Enqueue(root);

            int i = 0;
            //continue until no more nodes
            while (queue.Count > 0)
            {
                i++;
                root = queue.Dequeue();

                if (root != null)
                {
                    queue.Enqueue(root.left);
                    queue.Enqueue(root.right);

                    Console.WriteLine(root.value.ToString());
                }
            }
        }

        public Node<T> findSmallest(Node<T> root)
        {
            Node<T> returnNode = new Node<T>();

            if (root.left == null)
            {
                returnNode = root;
            }
            else
                returnNode = findSmallest(root.left);

            return returnNode;
        }


        public Node<T> Search(Node<T> root, T item)
        {
            Node<T> returnNode = new Node<T>();

            if (root.value.CompareTo(item) == 0) // root.value == item
            {
                return root;
            }
            else if (root.left != null && item.CompareTo(root.value) < 0)    //root.left != null && item < root.value
            {
                root = Search(root.left, item); 
            }
            else if (root.right != null && item.CompareTo(root.value) > 0)   //root.right != null && item > root.value
            {
                    root = Search(root.right, item);
            }
            else
                root = returnNode;  //null node

            return root;
        }

        public Node<T> GetSibling(Node<T> root)
        {
            Node<T> sibling = new Node<T>();

            Node<T> parent = root.parent;

            if (parent.left == root && parent.right != null)
                sibling = parent.right;
            else if (parent.right == root && parent.left != null)
                sibling = parent.left;

            return sibling;
        }

        public Node<T> GetAunt(Node<T> root)
        {
            Node<T> aunt = new Node<T>();

            Node<T> parent = root.parent;
            Node<T> grandParent = parent.parent;

            if (grandParent.left == parent && grandParent.right != null)
                aunt = grandParent.right;
            else if (grandParent.right == parent && grandParent.left != null)
                aunt = grandParent.left;

            return aunt;
        }

        public void Delete(Node<T> toDelete)
        {
            Node<T> toDeleteParent = toDelete.parent;

            //no children
            if (toDelete.left == null && toDelete.right == null) //node is a leaf
            {
                //sever it
                if (toDeleteParent.left == toDelete)
                    toDeleteParent.left = null;
                else
                    toDeleteParent.right = null;
            }
            //only one child
            else if (toDelete.left != null && toDelete.right == null)   //if only has one child, and it is left child
            {
                Node<T> toDeleteOnlyChild = toDelete.left;

                if (toDeleteParent.left == toDelete)
                    toDeleteParent.left = toDeleteOnlyChild;
                else
                    toDeleteParent.right = toDeleteOnlyChild;

                toDeleteOnlyChild.parent = toDeleteParent;
            }
            else if (toDelete.left == null && toDelete.right != null)   //if only has one child, and it is right child
            {
                Node<T> toDeleteOnlyChild = toDelete.right;

                if (toDeleteParent.right == toDelete)
                    toDeleteParent.right = toDeleteOnlyChild;
                else
                    toDeleteParent.left = toDeleteOnlyChild;

                toDeleteOnlyChild.parent = toDeleteParent;
            }
            //two children
            else
            {
                //find in order successor
                Node<T> successor = FindInOrderSuccessor(toDelete);

                if (successor.right != null || successor.left != null)  //if the successor has children we need to worry about
                {
                    Delete(successor);
                }

                //transfer toDeletes attributes to successor
                if (toDelete.right != successor) //incase the successor was only one level down, dont make itself its own child
                    successor.right = toDelete.right;
                else    //if the succesor is only one step to the right, remove its reference
                    toDelete.right = null;

                successor.left = toDelete.left;
                successor.parent = toDelete.parent;

                //change toDeletes childrens attributes
                if(toDelete.left != null)
                    toDelete.left.parent = successor;
                if(toDelete.right != null)
                    toDelete.right.parent = successor;

                //change toDelete parents attributes
                if (toDeleteParent.left == toDelete)
                    toDeleteParent.left = successor;
                else
                    toDeleteParent.right = successor;

                //toDelete is now fully cut off!!
            }


        }

        private Node<T> FindInOrderSuccessor(Node<T> root)
        {
            Node<T> rootRight = root.right;

            Node<T> inOrderSuccessor = findSmallest(rootRight);

            return inOrderSuccessor;
        }

        public bool ValidateWholeTree(Node<T> root)
        {
            bool treeValid = true;

            Queue<Node<T>> queue = new Queue<Node<T>>();

            //put the root into it
            queue.Enqueue(root);

            int i = 0;
            //continue until no more nodes
            while (queue.Count > 0)
            {
                i++;
                root = queue.Dequeue();

                if (root != null)
                {
                    queue.Enqueue(root.left);
                    queue.Enqueue(root.right);

                    Console.WriteLine(root.value.ToString());

                    bool subValid = ValidateSubTree(root);

                    Console.WriteLine("SubTree Valid??: " + subValid);

                    if (!subValid)
                        treeValid = false;

                }
            }

            return treeValid;
        }

        private bool ValidateSubTree(Node<T> root)
        {
            bool valid = true;

            if (root.right != null)
            {
                if(root.right.value.CompareTo(root.value) < 0)   //root.right.value < root.value
                {
                    valid = false;
                }
            }
            if (root.left != null)
            {
                if(root.left.value.CompareTo(root.value) > 0)    //root.left.value > root.value
                {
                    valid = false;
                }
            }

            return valid;
        }

        public int CompareTo(T? other)
        {
            throw new NotImplementedException();
        }
    }
}
