using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{

    public class SplayTree<T> where T : IComparable<T>
    {
        /// <summary>
        /// inserts a node recursively into the proper position,
        /// sets parent, value, left, and right
        /// 
        /// returns a tuple of the root, and the newly inserted node
        /// </summary>
        /// <param name="root"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public (Node<T>, Node<T>) BSTInsert(Node<T> root, T v)
        {
            Node<T> newNode = root;
            if (root == null)
            {
                root = new Node<T>();
                root.value = v;

                newNode = root;
            }

           
            // insertion logic, if the value (v )is < root, insert to the root.left
            // otherwise it's >=, so insert to the right
            else if (v.CompareTo(root.value) < 0)  //v < root.value
            {
                
                (root.left, newNode) = BSTInsert(root.left, v);
                root.left.parent = root;
            }
            else
            {
                (root.right, newNode) = BSTInsert(root.right, v);
                root.right.parent = root;
            }

            return (root, newNode);
        }

        /// <summary>
        /// Inserts the new node by calling the BSTInsert method.
        /// Following that, splays the tree by the newly inserted node.
        /// then with that now at the root, returns it.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public Node<T> Insert(Node<T> root, T v)
        {
            Node<T> newNode = new Node<T>();

            (root, newNode) = BSTInsert(root, v);

            root = Splay(newNode);

            return root;
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
        /// <summary>
        /// preforms rotations based upon the parent or grandparent of the node
        /// tail recursive
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public Node<T> Splay(Node<T> root)
        {
            Node<T> parent = root.parent;

            if (parent == null)//if the root is the root, has no parents
            {
                //no code needs to be here...
            }
            else if (root.parent.parent == null) //the node has only one parent, it is only one rotation away from the true root
            {
                

                if(parent.left == root)
                    root = Zig(parent);
                else
                    root = Zag(parent);
            }
            else    //the node has two or more parents
            {
                Node<T> grandParent = parent.parent;

                if(grandParent.left == parent)
                {
                    if(parent.left == root) //ZigZig
                    {
                        root = Zig(parent);
                        root = Zig(grandParent);
                    }
                    else    //ZagZig
                    {
                        root = Zag(parent);
                        root = Zig(grandParent);
                    }
                }
                else
                {
                    if (parent.right == root)  //Zagzag
                    {
                        root = Zag(parent);
                        root = Zag(grandParent);
                    }
                    else    //Zigzag
                    {
                        root = Zig(parent);
                        root = Zag(grandParent);
                    }
                }
            }
            //tail recursion if the root is not yet at the top
            if (root.parent != null)
                root = Splay(root);
            return root;
        }
        //rotate left
        public Node<T> Zig(Node<T> oldRoot)
        {
            Node<T> newRoot = oldRoot.left;

            Node<T> oldRootParent = oldRoot.parent;

            newRoot.parent = oldRoot.parent;
            oldRoot.parent = newRoot;


            if (oldRootParent != null && oldRoot == oldRootParent.right)
            {
                oldRootParent.right = newRoot;
            }
            else if (oldRootParent != null)
            {
                oldRootParent.left = newRoot;
            }

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

            return newRoot;
        }
        //rotate right
        public Node<T> Zag(Node<T> oldRoot)
        {
            Node<T> newRoot = oldRoot.right;

            Node<T> oldRootParent = oldRoot.parent;

            newRoot.parent = oldRoot.parent;
            oldRoot.parent = newRoot;

            if(oldRootParent != null && oldRoot == oldRootParent.right)
            {
                oldRootParent.right = newRoot;
            }
            else if (oldRootParent != null)
            {
                oldRootParent.left = newRoot;
            }

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

            return newRoot;
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

        /// <summary>
        /// will return the node asked for, if not found, will return a default/null node
        /// 
        /// uses a standard BST search, and then splays that node to the top
        /// </summary>
        /// <param name="root"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Node<T> Find(Node<T> root, T item)
        {
            root = Search(root, item);
            root = Splay(root);

            return root;
        }
        private Node<T> Search(Node<T> root, T item)
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

            //root = Splay(root);

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

        /// <summary>
        /// searches for the value to be deleted, if not found returns original root.
        /// else, returns the deleted nodes parent, which was now splayed to the root
        /// </summary>
        /// <param name="root"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Node<T> Delete(Node<T> root, T value)
        {
            Node<T> toDelete = Search(root, value);

            if(toDelete == null)
            {
                Console.WriteLine("nothing was deleted");
                return root;
            }
            else
            {
                Node<T> toDeleteParent = BSTDelete(toDelete);
                toDeleteParent = Splay(toDeleteParent);
                return toDeleteParent;  //this is now the root
            } 
        }
        private Node<T> BSTDelete(Node<T> toDelete)
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

                Node<T> successorOldParent = successor.parent;

                if (successor.right != null || successor.left != null)  //if the successor has children we need to worry about
                {
                    BSTDelete(successor);
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

                //remove the successor as its parents child
                if (successorOldParent.left == successor)
                    successorOldParent.left = null;
                if (successorOldParent.right == successor)
                    successorOldParent.right = null;
            }

            return toDeleteParent;
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
