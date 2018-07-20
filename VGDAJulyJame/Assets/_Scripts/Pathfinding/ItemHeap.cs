using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeap<T> where T : IHeapItem<T>
{
    T[] items;
    int currentItemCount;

    public ItemHeap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Swap(T itemA, T itemB)
    {
        // swap item location
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;

        // update swaped index numbers
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }

    public void PercolateUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        // compare specified item with values of parent
        while(true)
        {
            T parentItem = items[parentIndex];

            if (item.CompareTo(parentItem) > 0)
                Swap(item, parentItem);
            else
                break;

            // compare to next parent (traverses up tree)
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    // insert item at top of heap, swap with children if item is larger than child node
    public void PercolateDown(T item)
    {
        while (true)
        {
            int leftChildIndex = 2 * item.HeapIndex + 1;
            int rightChildIndex = 2 * item.HeapIndex + 2;

            int swapIndex = 0;

            if (leftChildIndex < currentItemCount)
            {
                swapIndex = leftChildIndex;

                if (rightChildIndex < currentItemCount)
                {
                    if (items[leftChildIndex].CompareTo(items[rightChildIndex]) < 0)
                    {
                        swapIndex = rightChildIndex;
                    }
                }

                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                    return;
            }
            else
                return;
        }
    }

    // add item into heap
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;

        PercolateUp(item);
        currentItemCount++;
    }

    // return value of top of heap, removes top of heap
    public T Pop()
    {
        T firstItem = items[0];

        currentItemCount--;

        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        PercolateDown(items[0]);
        
        return firstItem;
    }

    // check if item is within heap
    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    // change priority of an item
    public void UpdateItem(T item)
    {
        PercolateUp(item);
    }

    public int Count { get { return currentItemCount; } }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}
