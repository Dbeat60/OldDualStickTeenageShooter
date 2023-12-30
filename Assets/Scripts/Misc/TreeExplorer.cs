using System;
using System.Collections;
using UnityEngine;

public class TreeExplorer : MonoBehaviour
{

    // Public members

    public Tree parent;

    // Private members

    private int[] values = { 1, 3, 4, 5, 7, 8, 9, 90 };

    // Initialization

    private void Start()
    {
        ArrayList array = new ArrayList();

        Debug.Log("Tree explorer test");
        Debug.Log(string.Format("{0}-{1} {2}", "a", "b", "c"));

        for (int i = 0; i < 15; i++)
        {
            InsertChild(parent, UnityEngine.Random.Range(0, 100));
        }

        //parent.value = -9999;
        InsertChild(parent, -9999);

        Debug.Log(CheckIfBinary(parent));
        Debug.Log("done");

        array.Add(parent);
        array.Add("adf");

        Tree nt = (Tree)array[0];
        Debug.Log(nt.value);
        Debug.Log(array[1]);

        int[] values = GetChildsInTree(parent);
        if (values != null)
        {
            Debug.Log("Tree has " + values.Length + " values:");
            string vstring = "";
            for (int i = 0; i < values.Length; ++i)
            {
                vstring += values[i] + (i < values.Length - 1 ? " " : "");
            }
            Debug.Log(vstring);
        }
        else
        {
            Debug.Log("Tree has no values");
        }
    }

    // Public methods
    
    public int []GetChildsInTree_old(Tree tree)
    {
        int[] values = null;
        int count = GetTreeCount(tree);

        if (count > 0)
        {
            values = new int[count];
            Tree []leaves = new Tree[count];
            
            leaves[0] = tree;
            values[0] = tree.value;
            for (int current = 0, next = 1; current < count; ++current)
            {
                Tree leaf = leaves[current];
                values[current] = leaf.value;
                
                if (leaf.left != null)
                {
                    leaves[next] = leaf.left;
                    next++;
                }

                if (leaf.right != null)
                {
                    leaves[next] = leaf.right;
                    next++;
                }
            }
            
            // Sort
            for (int i = 0; i < count - 1; ++i)
            {
                int minValue = values[i];
                int min = i;

                for (int j = i + 1; j < count; ++j)
                {
                    if (values[j] < minValue)
                    {
                        minValue = values[j];
                        min = j;
                    }
                }

                if (min != i)
                {
                    int tmp = values[i];
                    values[i] = minValue;
                    values[min] = tmp;
                }
            }
        }

        return values;
    }

    public int[] GetChildsInTree(Tree tree)
    {
        int[] values = null;
        int count = GetTreeCount(tree);

        if (count > 0)
        {
            values = new int[count];
            Tree[] leaves = new Tree[count];

            leaves[0] = tree;
            int next = 0;

            int j = 0;
            while (j >= 0)
            {
                while (leaves[j].left != null)
                {
                    leaves[j + 1] = leaves[j].left;
                    j++;
                }

                values[next] = leaves[j].value;
                next++;
                
                if (leaves[j].right != null)
                {
                    leaves[j] = leaves[j].right;
                }
                else
                {
                    while (j >= 0
                        && leaves[j].right == null)
                    {
                        --j;

                        if (j >= 0)
                        {
                            values[next] = leaves[j].value;
                            next++;
                        }
                    }

                    if (j >= 0)
                    {
                        leaves[j] = leaves[j].right;
                    }
                }
            }
        }

        return values;
    }

    public bool IsValueInTree(Tree tree, int value)
    {
        bool found = false;

        Tree search = tree;
        while (search != null)
        {
            if (value < search.value)
            {
                search = search.left;
            }
            else if (value > search.value)
            {
                search = search.right;
            }
            else
            {
                found = true;
                break;
            }
        }

        return found;
    }
    
    // Private methods

    private void InsertChild(Tree tree, int value)
    {
        if (parent == null)
        {
            parent = new Tree(value);
            tree = parent;
        }
        else
        {
            Tree search = tree;
            while (search != null)
            {
                if (value < search.value)
                {
                    if (search.left != null)
                    {
                        search = search.left;
                    }
                    else
                    {
                        search.left = new Tree(value);
                        break;
                    }
                }
                else if (value > search.value)
                {
                    if (search.right != null)
                    {
                        search = search.right;
                    }
                    else
                    {
                        search.right = new Tree(value);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }

    private bool CheckIfBinary(Tree tree)
    {
        if (tree.left != null)
        {
            if (tree.left.value > tree.value)
            {
                return false;
            }
            else
            {
                CheckIfBinary(tree.left);
            }
        }

        if (tree.right != null)
        {
            if (tree.right.value < tree.value)
            {
                return false;
            }
            else
            {
                CheckIfBinary(tree.right);
            }
        }

        return true;
    }

    private int GetTreeCount(Tree tree)
    {
        int count = 0;

        if (tree != null)
        {
            count = CountChild(tree);
        }

        return count;
    }

    private int CountChild(Tree tree)
    {
        int count = 1;

        if (tree.left != null)
        {
            count += CountChild(tree.left);
        }
        if (tree.right != null)
        {
            count += CountChild(tree.right);
        }

        return count;
    }

    // Utils
    
    private static void Swap<T>(T val1, T val2)
    {
        T temp;
        temp = val1;
        val1 = val2;
        val2 = temp;
    }

}
