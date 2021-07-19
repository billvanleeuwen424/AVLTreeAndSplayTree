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
        public int balancefactor = 0;
    }

    public class AVLTree<T> where T : IComparable<T>
    {
        /// <summary>
        /// inserts a node recursively into the proper position,
        /// sets parent, value, left, and right, balances tree
        /// </summary>
        /// <param name="root"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public Node<T> Insert(Node<T> root, T v)
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
                
                root.left = Insert(root.left, v);
                root.left.parent = root;
                root = balance_tree(root);
            }
            else
            {

                root.right = Insert(root.right, v);
                root.right.parent = root;
                root = balance_tree(root);
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

        public int getHeight(Node<T> current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = Math.Max(l, r);
                height = m + 1;
            }
            return height;
        }
        public int balance_factor(Node<T> current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }

        // This is the code which actually balances the tree, it calls the different cases
        // but RotateLeftLeft, RotateLeftRight, and RotateRightLeft are to be filled in by the student.  
        private Node<T> balance_tree(Node<T> root)
        {
            root.balancefactor = balance_factor(root);// balance factor is left_height - right_height
            if (root.balancefactor > 1)
            {
                //we have to rotate right
                if (root.left.balancefactor > 0)
                {
                    //simple right rotation
                    root = RotateLeftLeft(root);
                }
                else
                {
                    //straighten the bend first, and rotate right.
                    root = RotateLeftRight(root);
                }
            }
            else if (root.balancefactor < -1)
            {
                //we have to rotate left
                if (root.right.balancefactor > 0)
                {
                    //straighten the bend and then rotate left.
                    root = RotateRightLeft(root);
                }
                else
                {
                    //use this as a template
                    //This is a simple left rotation
                    root = RotateRightRight(root);
                }
            }
            return root;
        }//end balance_tree

        /// <summary>
        /// Steps:
        /// 1. set the new roots parent to the old roots, and set the old root parent to the new root
        /// 2. if the new root has a right child, deal with it so it doesnt get lost
        ///     - set the right child as the left child of the oldRoot, and set its parent to the old root.
        ///       this movement will keep the tree a valid BST
        /// 3. finally, set the old root as the child of the new root. 
        /// return the new root. tree is now balanced.
        /// </summary>
        /// <param name="oldRoot"></param>
        /// <returns></returns>
        public Node<T> RotateLeftLeft(Node<T> oldRoot)
        {
            Node<T> newRoot = oldRoot.left;

            newRoot.parent = oldRoot.parent;
            oldRoot.parent = newRoot;


            //if the new root has both left and right children, deal with the right child
            if (newRoot.right != null)
            {
                Node<T> newRootRight = newRoot.right;

                oldRoot.left = newRootRight;
                newRootRight.parent = oldRoot;
            }
            else
            {
                oldRoot.left = null;
            }
            

            newRoot.right = oldRoot;

            //refactor balance in new position
            //oldRoot.balancefactor = balance_factor(oldRoot);

            return newRoot;
        }

        public Node<T> RotateLeftRight(Node<T> oldRoot)
        {
            Node<T> newLeft = oldRoot.left;

            Node<T> newRoot = newLeft.right;
            //step one
            newRoot.parent = oldRoot;
            newLeft.parent = newRoot;

            newRoot.left = newLeft;

            oldRoot.left = newRoot;
            newLeft.right = null;

            //step two
            newRoot = RotateLeftLeft(oldRoot);

            return newRoot;
        }

        public Node<T> RotateRightRight(Node<T> oldRoot)
        {
            Node<T> newRoot = oldRoot.right;

            newRoot.parent = oldRoot.parent;
            oldRoot.parent = newRoot;


            //if the new root has both right and right children, deal with the right child
            if (newRoot.left != null)
            {
                Node<T> newRootRight = newRoot.left;

                oldRoot.right = newRootRight;
                newRootRight.parent = oldRoot;
            }
            else
            {
                oldRoot.right = null;
            }


            newRoot.left = oldRoot;

            //refactor balance in new position
            //oldRoot.balancefactor = balance_factor(oldRoot);

            return newRoot;
        }

        public Node<T> RotateRightLeft(Node<T> oldRoot)
        {
            Node<T> newRight = oldRoot.right;

            Node<T> newRoot = newRight.left;
            //step one
            newRoot.parent = oldRoot;
            newRight.parent = newRoot;

            newRoot.right = newRight;

            oldRoot.right = newRoot;
            newRight.left = null;

            //step two
            newRoot = RotateRightRight(oldRoot);

            return newRoot;
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

        //Level order traversal repurposed from http://www.geeksforgeeks.org/level-order-tree-traversal/
        public void printLevelOrder(Node<T> root)
        {
            int treeHeight = getHeight(root);
            int i;
            string temp;//formatting 
            for (i = 1; i <= treeHeight; i++)
            {
                Console.WriteLine();
                temp = new string(' ', 4 * (treeHeight - i));//formatting
                Console.Write(temp);//the formatting being printed
                printGivenLevel(root, i);
            }
        }
        public void printGivenLevel(Node<T> root, int level)
        {
            //The formatting should probably happen here not in printLevelOrder
            //

            if (root == null)
            {
                Console.Write(" ni ");
                return;
            }
            if (level == 1)
                Console.Write(" " + root.value + " ");
            else if (level > 1)
            {
                printGivenLevel(root.left, level - 1);
                printGivenLevel(root.right, level - 1);

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
